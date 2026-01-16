namespace Cliente_Ejercicio1
{
    partial class Formulario_cliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formulario_cliente));
            this.btnTime = new System.Windows.Forms.Button();
            this.btnDate = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.lblResultado = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.btnDialogo = new System.Windows.Forms.Button();
            this.lblIp = new System.Windows.Forms.Label();
            this.lblPuerto = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTime
            // 
            this.btnTime.Location = new System.Drawing.Point(13, 13);
            this.btnTime.Name = "btnTime";
            this.btnTime.Size = new System.Drawing.Size(75, 23);
            this.btnTime.TabIndex = 0;
            this.btnTime.Tag = "time";
            this.btnTime.Text = "time";
            this.btnTime.UseVisualStyleBackColor = true;
            this.btnTime.Click += new System.EventHandler(this.btnes_ClickAsync);
            // 
            // btnDate
            // 
            this.btnDate.Location = new System.Drawing.Point(94, 13);
            this.btnDate.Name = "btnDate";
            this.btnDate.Size = new System.Drawing.Size(75, 23);
            this.btnDate.TabIndex = 1;
            this.btnDate.Tag = "date";
            this.btnDate.Text = "date";
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.btnes_ClickAsync);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(175, 13);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 2;
            this.btnAll.Tag = "all";
            this.btnAll.Text = "all";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnes_ClickAsync);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(256, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Tag = "close";
            this.btnClose.Text = "close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(13, 55);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(318, 20);
            this.txtPass.TabIndex = 4;
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(12, 87);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(58, 13);
            this.lblResultado.TabIndex = 5;
            this.lblResultado.Text = "Resultado:";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(53, 39);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(247, 13);
            this.lblPass.TabIndex = 6;
            this.lblPass.Text = "Introduce aqui la contraseña para cerrar el servidor";
            // 
            // btnDialogo
            // 
            this.btnDialogo.Location = new System.Drawing.Point(378, 13);
            this.btnDialogo.Name = "btnDialogo";
            this.btnDialogo.Size = new System.Drawing.Size(75, 23);
            this.btnDialogo.TabIndex = 7;
            this.btnDialogo.Text = "Ip / Puerto";
            this.btnDialogo.UseVisualStyleBackColor = true;
            this.btnDialogo.Click += new System.EventHandler(this.btnDialogo_Click);
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(375, 55);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(19, 13);
            this.lblIp.TabIndex = 8;
            this.lblIp.Text = "Ip:";
            // 
            // lblPuerto
            // 
            this.lblPuerto.AutoSize = true;
            this.lblPuerto.Location = new System.Drawing.Point(375, 87);
            this.lblPuerto.Name = "lblPuerto";
            this.lblPuerto.Size = new System.Drawing.Size(41, 13);
            this.lblPuerto.TabIndex = 9;
            this.lblPuerto.Text = "Puerto:";
            // 
            // Formulario_cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 125);
            this.Controls.Add(this.lblPuerto);
            this.Controls.Add(this.lblIp);
            this.Controls.Add(this.btnDialogo);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnDate);
            this.Controls.Add(this.btnTime);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Formulario_cliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servicio de Fecha y Hora";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTime;
        private System.Windows.Forms.Button btnDate;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Button btnDialogo;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.Label lblPuerto;
    }
}

