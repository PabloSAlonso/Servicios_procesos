namespace Ejercicio4
{
    partial class Tasks_Archivos
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnBusqueda = new System.Windows.Forms.Button();
            this.btnPosicion = new System.Windows.Forms.Button();
            this.btnHttp = new System.Windows.Forms.Button();
            this.txtBoxComun = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listaResultados = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(150, 83);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(484, 22);
            this.txtUrl.TabIndex = 0;
            // 
            // btnBusqueda
            // 
            this.btnBusqueda.Location = new System.Drawing.Point(12, 199);
            this.btnBusqueda.Name = "btnBusqueda";
            this.btnBusqueda.Size = new System.Drawing.Size(115, 23);
            this.btnBusqueda.TabIndex = 1;
            this.btnBusqueda.Text = "Busqueda";
            this.btnBusqueda.UseVisualStyleBackColor = true;
            this.btnBusqueda.Click += new System.EventHandler(this.btnBusqueda_click);
            // 
            // btnPosicion
            // 
            this.btnPosicion.Location = new System.Drawing.Point(133, 199);
            this.btnPosicion.Name = "btnPosicion";
            this.btnPosicion.Size = new System.Drawing.Size(115, 23);
            this.btnPosicion.TabIndex = 2;
            this.btnPosicion.Text = "Posicion";
            this.btnPosicion.UseVisualStyleBackColor = true;
            this.btnPosicion.Click += new System.EventHandler(this.btnPosicion_Click);
            // 
            // btnHttp
            // 
            this.btnHttp.Location = new System.Drawing.Point(254, 199);
            this.btnHttp.Name = "btnHttp";
            this.btnHttp.Size = new System.Drawing.Size(115, 23);
            this.btnHttp.TabIndex = 3;
            this.btnHttp.Text = "HTTP";
            this.btnHttp.UseVisualStyleBackColor = true;
            // 
            // txtBoxComun
            // 
            this.txtBoxComun.Location = new System.Drawing.Point(56, 228);
            this.txtBoxComun.Name = "txtBoxComun";
            this.txtBoxComun.Size = new System.Drawing.Size(274, 22);
            this.txtBoxComun.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(376, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Introduce:Busqueda / Posicion / HTTP";
            // 
            // listaResultados
            // 
            this.listaResultados.FormattingEnabled = true;
            this.listaResultados.ItemHeight = 16;
            this.listaResultados.Location = new System.Drawing.Point(379, 199);
            this.listaResultados.Name = "listaResultados";
            this.listaResultados.ScrollAlwaysVisible = true;
            this.listaResultados.Size = new System.Drawing.Size(485, 244);
            this.listaResultados.TabIndex = 7;
            // 
            // Tasks_Busqueda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 535);
            this.Controls.Add(this.listaResultados);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxComun);
            this.Controls.Add(this.btnHttp);
            this.Controls.Add(this.btnPosicion);
            this.Controls.Add(this.btnBusqueda);
            this.Controls.Add(this.txtUrl);
            this.Name = "Tasks_Busqueda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario Busqueda";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnBusqueda;
        private System.Windows.Forms.Button btnPosicion;
        private System.Windows.Forms.Button btnHttp;
        private System.Windows.Forms.TextBox txtBoxComun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listaResultados;
    }
}

