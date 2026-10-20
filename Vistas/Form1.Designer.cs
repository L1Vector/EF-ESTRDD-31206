namespace proyectofinal
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lblTitulo = new Label();
            btnRegistrarDebito = new Button();
            btnExtornoAuto = new Button();
            btnEmitirBoleta = new Button();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            txtLog = new TextBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.DarkRed;
            lblTitulo.Location = new Point(26, 23);
            lblTitulo.Margin = new Padding(5, 0, 5, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(561, 47);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "OXXO - Módulo POS (Salidas, Notificaciones y Extorno)";
            // 
            // btnRegistrarDebito
            // 
            btnRegistrarDebito.BackColor = Color.LightSkyBlue;
            btnRegistrarDebito.Location = new Point(26, 84);
            btnRegistrarDebito.Margin = new Padding(5, 4, 5, 4);
            btnRegistrarDebito.Name = "btnRegistrarDebito";
            btnRegistrarDebito.Size = new Size(374, 53);
            btnRegistrarDebito.TabIndex = 1;
            btnRegistrarDebito.Text = "1. Registrar Débito POS (Push Pila)";
            btnRegistrarDebito.UseVisualStyleBackColor = false;
            btnRegistrarDebito.Click += btnRegistrarDebitoPOS_Click;
            // 
            // btnExtornoAuto
            // 
            btnExtornoAuto.BackColor = Color.Coral;
            btnExtornoAuto.Location = new Point(414, 84);
            btnExtornoAuto.Margin = new Padding(5, 4, 5, 4);
            btnExtornoAuto.Name = "btnExtornoAuto";
            btnExtornoAuto.Size = new Size(386, 53);
            btnExtornoAuto.TabIndex = 2;
            btnExtornoAuto.Text = "2. Extorno Automático (Pop Rollback)";
            btnExtornoAuto.UseVisualStyleBackColor = false;
            btnExtornoAuto.Click += btnExtornoAutomatico_Click;
            // 
            // btnEmitirBoleta
            // 
            btnEmitirBoleta.BackColor = Color.LightGreen;
            btnEmitirBoleta.Location = new Point(26, 153);
            btnEmitirBoleta.Margin = new Padding(5, 4, 5, 4);
            btnEmitirBoleta.Name = "btnEmitirBoleta";
            btnEmitirBoleta.Size = new Size(374, 53);
            btnEmitirBoleta.TabIndex = 3;
            btnEmitirBoleta.Text = "3. Emitir Boleta B155 (Flush Pila)";
            btnEmitirBoleta.UseVisualStyleBackColor = false;
            btnEmitirBoleta.Click += btnEmitirBoleta_Click;
            // 
            // btnAnterior
            // 
            btnAnterior.Location = new Point(414, 153);
            btnAnterior.Margin = new Padding(5, 4, 5, 4);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(186, 53);
            btnAnterior.TabIndex = 4;
            btnAnterior.Text = "◄ Voucher Prev";
            btnAnterior.Click += btnVoucherAnterior_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Location = new Point(614, 153);
            btnSiguiente.Margin = new Padding(5, 4, 5, 4);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(186, 53);
            btnSiguiente.TabIndex = 5;
            btnSiguiente.Text = "Voucher Next ►";
            btnSiguiente.Click += btnVoucherSiguiente_Click;
            // 
            // txtLog
            // 
            txtLog.BackColor = Color.Black;
            txtLog.Font = new Font("Consolas", 10F);
            txtLog.ForeColor = Color.Lime;
            txtLog.Location = new Point(26, 231);
            txtLog.Margin = new Padding(5, 4, 5, 4);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(772, 397);
            txtLog.TabIndex = 6;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(707, -1);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(93, 77);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 16;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(832, 663);
            Controls.Add(pictureBox1);
            Controls.Add(txtLog);
            Controls.Add(btnSiguiente);
            Controls.Add(btnAnterior);
            Controls.Add(btnEmitirBoleta);
            Controls.Add(btnExtornoAuto);
            Controls.Add(btnRegistrarDebito);
            Controls.Add(lblTitulo);
            Margin = new Padding(5, 4, 5, 4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Caja OXXO - Módulo POS";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnRegistrarDebito;
        private System.Windows.Forms.Button btnExtornoAuto;
        private System.Windows.Forms.Button btnEmitirBoleta;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.TextBox txtLog;
        private PictureBox pictureBox1;
    }
}
