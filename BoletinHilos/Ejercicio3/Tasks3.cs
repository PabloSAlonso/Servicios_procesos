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
        async Task<string> DownloadFileAsync(string fileName, int delayMs) //devolvemos el string
        {
            await Task.Delay(delayMs); //hacemos que la tarea tarde en ejecutarse ese numero de ms
            return $"File {fileName} downloaded in {delayMs} ms";
        }

        Random random = new Random();
        public int numAleatorio(int maximo)
        {
            return random.Next(maximo);
        }

        private async void btnDescargar_Click(object sender, EventArgs e)
        {
            //texto es un string que al hacer el await de la tarea lo estamos igualando al resultado de ejecutar dicha tarea, si no hicieramos await no devolveria el string ya que devolveria la propia tarea (Task) sin completar pq el programa seguiria ejecutandose, de esta manera le mandamos esperar a acabar la tarea y obtenemos su resultado
            int aleatorio = numAleatorio(1000);
            string texto = await DownloadFileAsync(txtFileName.Text, aleatorio); 
            txtResultados.Text += texto + Environment.NewLine;
        }
    }
}
