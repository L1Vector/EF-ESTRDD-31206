namespace semana7Stack
{
    partial class FormModuloVouchers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormModuloVouchers));
            btnRegistrar = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtCodigo = new TextBox();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            lstVoucher = new ListBox();
            lstHistorial = new ListBox();
            lblActual = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnRegistrar
            // 
            btnRegistrar.BackColor = Color.Orange;
            btnRegistrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRegistrar.ForeColor = Color.White;
            btnRegistrar.Location = new Point(116, 219);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(137, 31);
            btnRegistrar.TabIndex = 0;
            btnRegistrar.Text = "Registrar Voucher";
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(884, 121);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(37, 156);
            label1.Name = "label1";
            label1.Size = new Size(139, 20);
            label1.TabIndex = 3;
            label1.Text = "Codigo de voucher";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Red;
            label4.Location = new Point(37, 313);
            label4.Name = "label4";
            label4.Size = new Size(151, 20);
            label4.TabIndex = 6;
            label4.Text = "Comprobante actual";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(525, 156);
            label5.Name = "label5";
            label5.Size = new Size(156, 20);
            label5.TabIndex = 7;
            label5.Text = "Historial de vouchers";
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(42, 190);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(363, 23);
            txtCodigo.TabIndex = 8;
            // 
            // btnAnterior
            // 
            btnAnterior.BackColor = Color.Orange;
            btnAnterior.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAnterior.ForeColor = Color.White;
            btnAnterior.Location = new Point(42, 256);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(108, 31);
            btnAnterior.TabIndex = 9;
            btnAnterior.Text = "Anterior";
            btnAnterior.UseVisualStyleBackColor = false;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.BackColor = Color.Orange;
            btnSiguiente.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSiguiente.ForeColor = Color.White;
            btnSiguiente.Location = new Point(220, 256);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(108, 31);
            btnSiguiente.TabIndex = 10;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.UseVisualStyleBackColor = false;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // lstVoucher
            // 
            lstVoucher.FormattingEnabled = true;
            lstVoucher.ItemHeight = 15;
            lstVoucher.Location = new Point(42, 336);
            lstVoucher.Name = "lstVoucher";
            lstVoucher.Size = new Size(363, 139);
            lstVoucher.TabIndex = 11;
            // 
            // lstHistorial
            // 
            lstHistorial.FormattingEnabled = true;
            lstHistorial.ItemHeight = 15;
            lstHistorial.Location = new Point(525, 190);
            lstHistorial.Name = "lstHistorial";
            lstHistorial.Size = new Size(347, 289);
            lstHistorial.TabIndex = 12;
            // 
            // lblActual
            // 
            lblActual.AutoSize = true;
            lblActual.BackColor = Color.White;
            lblActual.Location = new Point(58, 349);
            lblActual.Name = "lblActual";
            lblActual.Size = new Size(60, 15);
            lblActual.TabIndex = 13;
            lblActual.Text = "(ninguno)";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(884, 502);
            Controls.Add(lblActual);
            Controls.Add(lstHistorial);
            Controls.Add(lstVoucher);
            Controls.Add(btnSiguiente);
            Controls.Add(btnAnterior);
            Controls.Add(txtCodigo);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(btnRegistrar);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRegistrar;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label4;
        private Label label5;
        private TextBox txtCodigo;
        private Button btnAnterior;
        private Button btnSiguiente;
        private ListBox lstVoucher;
        private ListBox lstHistorial;
        private Label lblActual;
    }
}
