using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio3
{
    public partial class Tasks3 : Form
    {
        public Tasks3()
        {
            InitializeComponent();
        }
        async Task<string> DownloadFileAsync(string fileName, int delayMs)
        {
            return $"File {fileName} downloaded in {delayMs} ms";
        }

        Random random = new Random();
        public int numAleatorio(int maximo)
        {
            return random.Next(maximo);
        }

        private async Task btnDescargar_Click(object sender, EventArgs e)
        {
            int aleatorio = numAleatorio(10);
            Task<string> texto = DownloadFileAsync(txtFileName.Text, aleatorio);
            string resultado = await texto;
            await Task.Delay(aleatorio * 100);
            txtResultados.Text += texto.Result + Environment.NewLine;
        }
    }
}
