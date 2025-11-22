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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        async Task<string> DownloadFileAsync(string fileName, int delayMs)
        {
            return $"File {fileName} downloaded in {delayMs} ms";
        }

        Random random = new Random();
        private int numAleatorio(int maximo)
        {
            return random.Next(maximo);
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            Task<string> a = Task.Run(() =>DownloadFileAsync(txtFileName.Text, numAleatorio(10)));

            txtResultados.Text += a.Result + Environment.NewLine;
        }
    }
}
