using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente_Ejercicio1
{
    public partial class Formulario_cliente : Form
    {
        public Formulario_cliente()
        {
            InitializeComponent();
        }

        IPAddress ip = IPAddress.Parse("127.0.0.1");
        int puerto = 31416;

        private async Task<String> EnvioYRecepcionAsync(string comando)
        {
            try
            {
                using (Socket conexion = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp))
                {
                    IPEndPoint ep = new IPEndPoint(ip, puerto);
                    // Trata de conectarse a la IP y Puerto configurados.
                    await conexion.ConnectAsync(ep);
                    // Si hay exito (si no salta excepción), se procede al protocolo:
                    // - Recepción de mensaje de bienvenida.
                    // - Envío de texto desde txtEnviar
                    // - Recpción de mensaje de servidor
                    Encoding codificacion = Console.OutputEncoding;
                    using (NetworkStream ns = new NetworkStream(conexion))
                    using (StreamReader sr = new StreamReader(ns, codificacion))
                    using (StreamWriter sw = new StreamWriter(ns, codificacion))
                    {
                        sw.AutoFlush = true;
                        // Leemos mensaje de bienvenida y lo deshechamos
                        // (No hacemos nada con él).
                        string msg = await sr.ReadLineAsync();
                        // Se escribe el comando que depende del boton pulsado
                        await sw.WriteLineAsync(comando);
                        msg = await sr.ReadLineAsync();
                        return msg;
                    }
                }
            }
            catch (Exception ex) when (ex is SocketException || ex is IOException)
            {
                // Problema de comunicación:
                // no existe servidor, no responde, red caida,...
                return ex.Message;
            }
            catch (Exception ex)
            {
                return $"Error inesperado: {ex.GetType().Name}. Contacte con soporte.";
            }
        }

        private void btnes_Click(object sender, EventArgs e)
        {
            EnvioYRecepcionAsync(((Button)sender).Text);
        }

        private void btnDialogo_Click(object sender, EventArgs e)
        {
            bool flag = true;
            Form form = new Modal();
            form.tbIp.Text = ip.ToString();
            DialogResult result;
            result = form.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                MessageBox.Show("No se han guardado la Ip y Puerto nuevo", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result == DialogResult.OK)
            {
                int puertoMaximo = IPEndPoint.MaxPort;

                if(!IPAddress.TryParse(form.tbIp.Text, out IPAddress ipValidada))
                {

                }
                if (flag)
                {
                    ip = ipValidada;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                EnvioYRecepcionAsync("close");
            }
            else
            {
                EnvioYRecepcionAsync($"close {txtPass.Text}");
            }
        }
    }
}
