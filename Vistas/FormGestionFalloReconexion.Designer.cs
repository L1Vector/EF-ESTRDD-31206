namespace semana7Stack
{
    partial class FormGestionFalloReconexion
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGestionFalloReconexion));
            cmbTipo = new ComboBox();
            numImporte = new NumericUpDown();
            btnRegistrar = new Button();
            btnFallarRed = new Button();
            btnFallarServidor = new Button();
            btnRecuperarRed = new Button();
            btnRecuperarServidor = new Button();
            lstPendientes = new ListBox();
            btnProcesar = new Button();
            lstHistorial = new ListBox();
            lblEstadoRed = new Label();
            lblEstadoServidor = new Label();
            lblEstadoProceso = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            lblTotalPendientes = new Label();
            ((System.ComponentModel.ISupportInitialize)numImporte).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // cmbTipo
            // 
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Items.AddRange(new object[] { "Cobro", "Reversion", "Extorno" });
            cmbTipo.Location = new Point(32, 205);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(178, 23);
            cmbTipo.TabIndex = 0;
            // 
            // numImporte
            // 
            numImporte.DecimalPlaces = 2;
            numImporte.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numImporte.Location = new Point(32, 272);
            numImporte.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numImporte.Name = "numImporte";
            numImporte.Size = new Size(178, 23);
            numImporte.TabIndex = 1;
            numImporte.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // btnRegistrar
            // 
            btnRegistrar.BackColor = Color.RoyalBlue;
            btnRegistrar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRegistrar.ForeColor = Color.White;
            btnRegistrar.Location = new Point(32, 316);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(183, 33);
            btnRegistrar.TabIndex = 2;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnFallarRed
            // 
            btnFallarRed.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFallarRed.Location = new Point(32, 438);
            btnFallarRed.Name = "btnFallarRed";
            btnFallarRed.Size = new Size(183, 33);
            btnFallarRed.TabIndex = 3;
            btnFallarRed.Text = "Desconectar red";
            btnFallarRed.UseVisualStyleBackColor = true;
            btnFallarRed.Click += btnFallarRed_Click;
            // 
            // btnFallarServidor
            // 
            btnFallarServidor.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFallarServidor.Location = new Point(29, 519);
            btnFallarServidor.Name = "btnFallarServidor";
            btnFallarServidor.Size = new Size(183, 33);
            btnFallarServidor.TabIndex = 4;
            btnFallarServidor.Text = "Fallar servidor";
            btnFallarServidor.UseVisualStyleBackColor = true;
            btnFallarServidor.Click += btnFallarServidor_Click;
            // 
            // btnRecuperarRed
            // 
            btnRecuperarRed.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRecuperarRed.Location = new Point(32, 480);
            btnRecuperarRed.Name = "btnRecuperarRed";
            btnRecuperarRed.Size = new Size(183, 33);
            btnRecuperarRed.TabIndex = 5;
            btnRecuperarRed.Text = "Recuperar red";
            btnRecuperarRed.UseVisualStyleBackColor = true;
            btnRecuperarRed.Click += btnRecuperarRed_Click;
            // 
            // btnRecuperarServidor
            // 
            btnRecuperarServidor.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRecuperarServidor.Location = new Point(32, 557);
            btnRecuperarServidor.Name = "btnRecuperarServidor";
            btnRecuperarServidor.Size = new Size(183, 33);
            btnRecuperarServidor.TabIndex = 6;
            btnRecuperarServidor.Text = "Recuperar servidor";
            btnRecuperarServidor.UseVisualStyleBackColor = true;
            btnRecuperarServidor.Click += btnRecuperarServidor_Click;
            // 
            // lstPendientes
            // 
            lstPendientes.FormattingEnabled = true;
            lstPendientes.ItemHeight = 15;
            lstPendientes.Location = new Point(454, 150);
            lstPendientes.Name = "lstPendientes";
            lstPendientes.Size = new Size(471, 199);
            lstPendientes.TabIndex = 7;
            // 
            // btnProcesar
            // 
            btnProcesar.BackColor = Color.FromArgb(0, 192, 0);
            btnProcesar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProcesar.ForeColor = Color.White;
            btnProcesar.Location = new Point(454, 373);
            btnProcesar.Name = "btnProcesar";
            btnProcesar.Size = new Size(183, 45);
            btnProcesar.TabIndex = 9;
            btnProcesar.Text = "Procesar pendientes";
            btnProcesar.UseVisualStyleBackColor = false;
            btnProcesar.Click += btnProcesar_Click;
            // 
            // lstHistorial
            // 
            lstHistorial.FormattingEnabled = true;
            lstHistorial.ItemHeight = 15;
            lstHistorial.Location = new Point(454, 455);
            lstHistorial.Name = "lstHistorial";
            lstHistorial.Size = new Size(471, 199);
            lstHistorial.TabIndex = 10;
            // 
            // lblEstadoRed
            // 
            lblEstadoRed.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEstadoRed.ForeColor = Color.Green;
            lblEstadoRed.Location = new Point(25, 71);
            lblEstadoRed.Name = "lblEstadoRed";
            lblEstadoRed.Size = new Size(200, 23);
            lblEstadoRed.TabIndex = 11;
            lblEstadoRed.Text = "Red: Conectada";
            // 
            // lblEstadoServidor
            // 
            lblEstadoServidor.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEstadoServidor.ForeColor = Color.Red;
            lblEstadoServidor.Location = new Point(251, 71);
            lblEstadoServidor.Name = "lblEstadoServidor";
            lblEstadoServidor.Size = new Size(206, 23);
            lblEstadoServidor.TabIndex = 12;
            lblEstadoServidor.Text = "Servidor: Activo";
            // 
            // lblEstadoProceso
            // 
            lblEstadoProceso.Location = new Point(513, 76);
            lblEstadoProceso.Name = "lblEstadoProceso";
            lblEstadoProceso.Size = new Size(412, 18);
            lblEstadoProceso.TabIndex = 13;
            lblEstadoProceso.Text = "Estado: ";
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(255, 128, 0);
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(984, 58);
            label1.TabIndex = 14;
            label1.Text = "GESTIÓN DE FALLOS DE RED Y RECONEXIÓN DE POS";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(903, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(81, 58);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 15;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(445, 116);
            label2.Name = "label2";
            label2.Size = new Size(496, 31);
            label2.TabIndex = 16;
            label2.Text = "OPERACIONES PENDIENTES";
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(454, 421);
            label3.Name = "label3";
            label3.Size = new Size(496, 31);
            label3.TabIndex = 17;
            label3.Text = "HISTORIAL DE ATENCIONES";
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(25, 116);
            label4.Name = "label4";
            label4.Size = new Size(185, 31);
            label4.TabIndex = 18;
            label4.Text = "NUEVO REGISTRO";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(29, 176);
            label5.Name = "label5";
            label5.Size = new Size(106, 15);
            label5.TabIndex = 19;
            label5.Text = "Tipo de operación:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(29, 246);
            label6.Name = "label6";
            label6.Size = new Size(74, 15);
            label6.TabIndex = 20;
            label6.Text = "Importe (S/.)";
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(32, 388);
            label7.Name = "label7";
            label7.Size = new Size(284, 31);
            label7.TabIndex = 21;
            label7.Text = "SIMULACIÓN DE CONEXIÓN";
            // 
            // lblTotalPendientes
            // 
            lblTotalPendientes.Location = new Point(454, 352);
            lblTotalPendientes.Name = "lblTotalPendientes";
            lblTotalPendientes.Size = new Size(143, 18);
            lblTotalPendientes.TabIndex = 22;
            lblTotalPendientes.Text = "Elementos en la cola: 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 661);
            Controls.Add(lblTotalPendientes);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(lblEstadoProceso);
            Controls.Add(lblEstadoServidor);
            Controls.Add(lblEstadoRed);
            Controls.Add(lstHistorial);
            Controls.Add(btnProcesar);
            Controls.Add(lstPendientes);
            Controls.Add(btnRecuperarServidor);
            Controls.Add(btnRecuperarRed);
            Controls.Add(btnFallarServidor);
            Controls.Add(btnFallarRed);
            Controls.Add(btnRegistrar);
            Controls.Add(numImporte);
            Controls.Add(cmbTipo);
            Name = "Form1";
            StartPosition = FormStartPosition.Manual;
            Text = "SISTEMA OXXO POS";
            ((System.ComponentModel.ISupportInitialize)numImporte).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbTipo;
        private NumericUpDown numImporte;
        private Button btnRegistrar;
        private Button btnFallarRed;
        private Button btnFallarServidor;
        private Button btnRecuperarRed;
        private Button btnRecuperarServidor;
        private ListBox lstPendientes;
        private Button btnProcesar;
        private ListBox lstHistorial;
        private Label lblEstadoRed;
        private Label lblEstadoServidor;
        private Label lblEstadoProceso;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lblTotalPendientes;
    }
}
