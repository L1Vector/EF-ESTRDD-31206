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
            cmbTipo.Location = new Point(37, 273);
            cmbTipo.Margin = new Padding(3, 4, 3, 4);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(203, 28);
            cmbTipo.TabIndex = 0;
            // 
            // numImporte
            // 
            numImporte.DecimalPlaces = 2;
            numImporte.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numImporte.Location = new Point(37, 363);
            numImporte.Margin = new Padding(3, 4, 3, 4);
            numImporte.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numImporte.Name = "numImporte";
            numImporte.Size = new Size(203, 27);
            numImporte.TabIndex = 1;
            numImporte.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // btnRegistrar
            // 
            btnRegistrar.BackColor = Color.RoyalBlue;
            btnRegistrar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRegistrar.ForeColor = Color.White;
            btnRegistrar.Location = new Point(37, 421);
            btnRegistrar.Margin = new Padding(3, 4, 3, 4);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(209, 44);
            btnRegistrar.TabIndex = 2;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnFallarRed
            // 
            btnFallarRed.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFallarRed.Location = new Point(37, 584);
            btnFallarRed.Margin = new Padding(3, 4, 3, 4);
            btnFallarRed.Name = "btnFallarRed";
            btnFallarRed.Size = new Size(209, 44);
            btnFallarRed.TabIndex = 3;
            btnFallarRed.Text = "Desconectar red";
            btnFallarRed.UseVisualStyleBackColor = true;
            btnFallarRed.Click += btnFallarRed_Click;
            // 
            // btnFallarServidor
            // 
            btnFallarServidor.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFallarServidor.Location = new Point(33, 692);
            btnFallarServidor.Margin = new Padding(3, 4, 3, 4);
            btnFallarServidor.Name = "btnFallarServidor";
            btnFallarServidor.Size = new Size(209, 44);
            btnFallarServidor.TabIndex = 4;
            btnFallarServidor.Text = "Fallar servidor";
            btnFallarServidor.UseVisualStyleBackColor = true;
            btnFallarServidor.Click += btnFallarServidor_Click;
            // 
            // btnRecuperarRed
            // 
            btnRecuperarRed.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRecuperarRed.Location = new Point(37, 640);
            btnRecuperarRed.Margin = new Padding(3, 4, 3, 4);
            btnRecuperarRed.Name = "btnRecuperarRed";
            btnRecuperarRed.Size = new Size(209, 44);
            btnRecuperarRed.TabIndex = 5;
            btnRecuperarRed.Text = "Recuperar red";
            btnRecuperarRed.UseVisualStyleBackColor = true;
            btnRecuperarRed.Click += btnRecuperarRed_Click;
            // 
            // btnRecuperarServidor
            // 
            btnRecuperarServidor.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRecuperarServidor.Location = new Point(37, 743);
            btnRecuperarServidor.Margin = new Padding(3, 4, 3, 4);
            btnRecuperarServidor.Name = "btnRecuperarServidor";
            btnRecuperarServidor.Size = new Size(209, 44);
            btnRecuperarServidor.TabIndex = 6;
            btnRecuperarServidor.Text = "Recuperar servidor";
            btnRecuperarServidor.UseVisualStyleBackColor = true;
            btnRecuperarServidor.Click += btnRecuperarServidor_Click;
            // 
            // lstPendientes
            // 
            lstPendientes.FormattingEnabled = true;
            lstPendientes.Location = new Point(519, 200);
            lstPendientes.Margin = new Padding(3, 4, 3, 4);
            lstPendientes.Name = "lstPendientes";
            lstPendientes.Size = new Size(538, 264);
            lstPendientes.TabIndex = 7;
            // 
            // btnProcesar
            // 
            btnProcesar.BackColor = Color.FromArgb(0, 192, 0);
            btnProcesar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProcesar.ForeColor = Color.White;
            btnProcesar.Location = new Point(519, 497);
            btnProcesar.Margin = new Padding(3, 4, 3, 4);
            btnProcesar.Name = "btnProcesar";
            btnProcesar.Size = new Size(209, 60);
            btnProcesar.TabIndex = 9;
            btnProcesar.Text = "Procesar pendientes";
            btnProcesar.UseVisualStyleBackColor = false;
            btnProcesar.Click += btnProcesar_Click;
            // 
            // lstHistorial
            // 
            lstHistorial.FormattingEnabled = true;
            lstHistorial.Location = new Point(519, 607);
            lstHistorial.Margin = new Padding(3, 4, 3, 4);
            lstHistorial.Name = "lstHistorial";
            lstHistorial.Size = new Size(538, 264);
            lstHistorial.TabIndex = 10;
            // 
            // lblEstadoRed
            // 
            lblEstadoRed.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEstadoRed.ForeColor = Color.Green;
            lblEstadoRed.Location = new Point(29, 95);
            lblEstadoRed.Name = "lblEstadoRed";
            lblEstadoRed.Size = new Size(229, 31);
            lblEstadoRed.TabIndex = 11;
            lblEstadoRed.Text = "Red: Conectada";
            // 
            // lblEstadoServidor
            // 
            lblEstadoServidor.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEstadoServidor.ForeColor = Color.Red;
            lblEstadoServidor.Location = new Point(287, 95);
            lblEstadoServidor.Name = "lblEstadoServidor";
            lblEstadoServidor.Size = new Size(235, 31);
            lblEstadoServidor.TabIndex = 12;
            lblEstadoServidor.Text = "Servidor: Activo";
            // 
            // lblEstadoProceso
            // 
            lblEstadoProceso.Location = new Point(586, 101);
            lblEstadoProceso.Name = "lblEstadoProceso";
            lblEstadoProceso.Size = new Size(471, 24);
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
            label1.Size = new Size(1125, 77);
            label1.TabIndex = 14;
            label1.Text = "GESTIÓN DE FALLOS DE RED Y RECONEXIÓN DE POS";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1032, 0);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(93, 77);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 15;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(509, 155);
            label2.Name = "label2";
            label2.Size = new Size(567, 41);
            label2.TabIndex = 16;
            label2.Text = "OPERACIONES PENDIENTES";
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(519, 561);
            label3.Name = "label3";
            label3.Size = new Size(567, 41);
            label3.TabIndex = 17;
            label3.Text = "HISTORIAL DE ATENCIONES";
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(29, 155);
            label4.Name = "label4";
            label4.Size = new Size(211, 41);
            label4.TabIndex = 18;
            label4.Text = "NUEVO REGISTRO";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(33, 235);
            label5.Name = "label5";
            label5.Size = new Size(134, 20);
            label5.TabIndex = 19;
            label5.Text = "Tipo de operación:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(33, 328);
            label6.Name = "label6";
            label6.Size = new Size(93, 20);
            label6.TabIndex = 20;
            label6.Text = "Importe (S/.)";
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(37, 517);
            label7.Name = "label7";
            label7.Size = new Size(325, 41);
            label7.TabIndex = 21;
            label7.Text = "SIMULACIÓN DE CONEXIÓN";
            // 
            // lblTotalPendientes
            // 
            lblTotalPendientes.Location = new Point(519, 469);
            lblTotalPendientes.Name = "lblTotalPendientes";
            lblTotalPendientes.Size = new Size(163, 24);
            lblTotalPendientes.TabIndex = 22;
            lblTotalPendientes.Text = "Elementos en la cola: 0";
            // 
            // FormGestionFalloReconexion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1125, 918);
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
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormGestionFalloReconexion";
            StartPosition = FormStartPosition.Manual;
            Text = "SISTEMA OXXO POS";
            Load += FormGestionFalloReconexion_Load;
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
