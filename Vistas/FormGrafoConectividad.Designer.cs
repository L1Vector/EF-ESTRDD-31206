namespace semana7Stack.Vistas
{
    partial class FormGrafoConectividad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelGrafo = new Panel();
            lblTitulo = new Label();
            lblDetalleOperacion = new Label();
            btnCerrar = new Button();
            pictureBox3 = new PictureBox();
            lblEstadoGeneral = new Label();
            btnRecuperarRed = new Button();
            btnDesconectarRed = new Button();
            btnEliminarPasarela = new Button();
            groupBox2 = new GroupBox();
            cmbPasarela = new ComboBox();
            label4 = new Label();
            groupBox1 = new GroupBox();
            btnAgregarConexion = new Button();
            numLatencia = new NumericUpDown();
            label3 = new Label();
            cboDestinoArista = new ComboBox();
            cboOrigenArista = new ComboBox();
            label2 = new Label();
            btnAgregarPasarela = new Button();
            txtPasarela = new TextBox();
            label1 = new Label();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            btnRutaOptima = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numLatencia).BeginInit();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // panelGrafo
            // 
            panelGrafo.BackColor = Color.FromArgb(255, 252, 239);
            panelGrafo.Location = new Point(12, 326);
            panelGrafo.Name = "panelGrafo";
            panelGrafo.Size = new Size(776, 265);
            panelGrafo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = Color.FromArgb(255, 252, 239);
            lblTitulo.Font = new Font("Segoe UI", 29F, FontStyle.Bold);
            lblTitulo.Location = new Point(244, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(302, 52);
            lblTitulo.TabIndex = 41;
            lblTitulo.Text = "Grafo de Pagos";
            // 
            // lblDetalleOperacion
            // 
            lblDetalleOperacion.AutoSize = true;
            lblDetalleOperacion.Location = new Point(12, 609);
            lblDetalleOperacion.Name = "lblDetalleOperacion";
            lblDetalleOperacion.Size = new Size(38, 15);
            lblDetalleOperacion.TabIndex = 42;
            lblDetalleOperacion.Text = "label1";
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.FromArgb(252, 196, 9);
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Microsoft Sans Serif", 10.8F);
            btnCerrar.ForeColor = Color.Black;
            btnCerrar.Location = new Point(693, 599);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(95, 33);
            btnCerrar.TabIndex = 43;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = EF_Estructu_III.Properties.Resources.OXXO_Logo;
            pictureBox3.Location = new Point(679, -3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(109, 49);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 44;
            pictureBox3.TabStop = false;
            // 
            // lblEstadoGeneral
            // 
            lblEstadoGeneral.AutoSize = true;
            lblEstadoGeneral.Location = new Point(12, 83);
            lblEstadoGeneral.Name = "lblEstadoGeneral";
            lblEstadoGeneral.Size = new Size(38, 15);
            lblEstadoGeneral.TabIndex = 45;
            lblEstadoGeneral.Text = "label1";
            // 
            // btnRecuperarRed
            // 
            btnRecuperarRed.Location = new Point(359, 28);
            btnRecuperarRed.Name = "btnRecuperarRed";
            btnRecuperarRed.Size = new Size(92, 39);
            btnRecuperarRed.TabIndex = 55;
            btnRecuperarRed.Text = "Recuperar red";
            btnRecuperarRed.UseVisualStyleBackColor = true;
            btnRecuperarRed.Click += btnRecuperarRed_Click;
            // 
            // btnDesconectarRed
            // 
            btnDesconectarRed.Location = new Point(252, 28);
            btnDesconectarRed.Name = "btnDesconectarRed";
            btnDesconectarRed.Size = new Size(103, 39);
            btnDesconectarRed.TabIndex = 54;
            btnDesconectarRed.Text = "Desconectar red";
            btnDesconectarRed.UseVisualStyleBackColor = true;
            btnDesconectarRed.Click += btnDesconectarRed_Click;
            // 
            // btnEliminarPasarela
            // 
            btnEliminarPasarela.Location = new Point(149, 58);
            btnEliminarPasarela.Name = "btnEliminarPasarela";
            btnEliminarPasarela.Size = new Size(111, 39);
            btnEliminarPasarela.TabIndex = 53;
            btnEliminarPasarela.Text = "Eliminar Pasarela";
            btnEliminarPasarela.UseVisualStyleBackColor = true;
            btnEliminarPasarela.Click += btnEliminarPasarela_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cmbPasarela);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(btnDesconectarRed);
            groupBox2.Controls.Add(btnRecuperarRed);
            groupBox2.Location = new Point(10, 232);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(457, 88);
            groupBox2.TabIndex = 52;
            groupBox2.TabStop = false;
            groupBox2.Text = "Agregar Estado";
            // 
            // cmbPasarela
            // 
            cmbPasarela.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPasarela.FormattingEnabled = true;
            cmbPasarela.Location = new Point(83, 37);
            cmbPasarela.Name = "cmbPasarela";
            cmbPasarela.Size = new Size(163, 23);
            cmbPasarela.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(5, 40);
            label4.Name = "label4";
            label4.Size = new Size(50, 15);
            label4.TabIndex = 56;
            label4.Text = "Pasarela";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnAgregarConexion);
            groupBox1.Controls.Add(numLatencia);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(cboDestinoArista);
            groupBox1.Controls.Add(cboOrigenArista);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(10, 110);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(457, 106);
            groupBox1.TabIndex = 51;
            groupBox1.TabStop = false;
            groupBox1.Text = "Conectar Pasarelas";
            // 
            // btnAgregarConexion
            // 
            btnAgregarConexion.Location = new Point(268, 59);
            btnAgregarConexion.Name = "btnAgregarConexion";
            btnAgregarConexion.Size = new Size(164, 39);
            btnAgregarConexion.TabIndex = 8;
            btnAgregarConexion.Text = "Agregar Conexión";
            btnAgregarConexion.UseVisualStyleBackColor = true;
            btnAgregarConexion.Click += btnAgregarConexion_Click;
            // 
            // numLatencia
            // 
            numLatencia.Location = new Point(106, 70);
            numLatencia.Name = "numLatencia";
            numLatencia.Size = new Size(140, 23);
            numLatencia.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 71);
            label3.Name = "label3";
            label3.Size = new Size(78, 15);
            label3.TabIndex = 6;
            label3.Text = "Latencia (ms)";
            // 
            // cboDestinoArista
            // 
            cboDestinoArista.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDestinoArista.FormattingEnabled = true;
            cboDestinoArista.Location = new Point(268, 30);
            cboDestinoArista.Name = "cboDestinoArista";
            cboDestinoArista.Size = new Size(163, 23);
            cboDestinoArista.TabIndex = 5;
            // 
            // cboOrigenArista
            // 
            cboOrigenArista.DropDownStyle = ComboBoxStyle.DropDownList;
            cboOrigenArista.FormattingEnabled = true;
            cboOrigenArista.Location = new Point(83, 30);
            cboOrigenArista.Name = "cboOrigenArista";
            cboOrigenArista.Size = new Size(163, 23);
            cboOrigenArista.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 32);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 3;
            label2.Text = "Conectar:";
            // 
            // btnAgregarPasarela
            // 
            btnAgregarPasarela.Location = new Point(32, 59);
            btnAgregarPasarela.Name = "btnAgregarPasarela";
            btnAgregarPasarela.Size = new Size(111, 39);
            btnAgregarPasarela.TabIndex = 49;
            btnAgregarPasarela.Text = "Agregar Pasarela";
            btnAgregarPasarela.UseVisualStyleBackColor = true;
            btnAgregarPasarela.Click += btnAgregarPasarela_Click;
            // 
            // txtPasarela
            // 
            txtPasarela.Location = new Point(97, 26);
            txtPasarela.Name = "txtPasarela";
            txtPasarela.Size = new Size(163, 23);
            txtPasarela.TabIndex = 48;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 28);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 47;
            label1.Text = "Pasarela";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtPasarela);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(btnAgregarPasarela);
            groupBox3.Controls.Add(btnEliminarPasarela);
            groupBox3.Location = new Point(508, 110);
            groupBox3.Margin = new Padding(3, 2, 3, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 2, 3, 2);
            groupBox3.Size = new Size(281, 106);
            groupBox3.TabIndex = 59;
            groupBox3.TabStop = false;
            groupBox3.Text = "Gestionar Pasarelas";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btnRutaOptima);
            groupBox4.Location = new Point(508, 220);
            groupBox4.Margin = new Padding(3, 2, 3, 2);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(3, 2, 3, 2);
            groupBox4.Size = new Size(281, 106);
            groupBox4.TabIndex = 60;
            groupBox4.TabStop = false;
            groupBox4.Text = "Ruta Óptima";
            // 
            // btnRutaOptima
            // 
            btnRutaOptima.Location = new Point(68, 40);
            btnRutaOptima.Name = "btnRutaOptima";
            btnRutaOptima.Size = new Size(148, 39);
            btnRutaOptima.TabIndex = 49;
            btnRutaOptima.Text = "Calcular Ruta Óptima";
            btnRutaOptima.UseVisualStyleBackColor = true;
            btnRutaOptima.Click += btnRutaOptima_Click;
            // 
            // FormGrafoConectividad
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 30, 28);
            ClientSize = new Size(800, 661);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(lblEstadoGeneral);
            Controls.Add(pictureBox3);
            Controls.Add(btnCerrar);
            Controls.Add(lblDetalleOperacion);
            Controls.Add(lblTitulo);
            Controls.Add(panelGrafo);
            Name = "FormGrafoConectividad";
            Text = "FormGrafoConectividad";
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numLatencia).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelGrafo;
        private Label lblTitulo;
        private Label lblDetalleOperacion;
        private Button btnCerrar;
        private PictureBox pictureBox3;
        private Label lblEstadoGeneral;
        private Button btnRecuperarRed;
        private Button btnDesconectarRed;
        private Button btnEliminarPasarela;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private Button btnAgregarConexion;
        private NumericUpDown numLatencia;
        private Label label3;
        private ComboBox cboDestinoArista;
        private ComboBox cboOrigenArista;
        private Label label2;
        private Button btnAgregarPasarela;
        private TextBox txtPasarela;
        private Label label1;
        private GroupBox groupBox3;
        private Label label4;
        private GroupBox groupBox4;
        private Button btnRutaOptima;
        private ComboBox cmbPasarela;
    }
}