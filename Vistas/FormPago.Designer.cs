namespace semana7Stack.Vistas
{
    partial class FormPago
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
            label1 = new Label();
            lstLista = new ListBox();
            btnGenerar = new Button();
            btnCarrito = new Button();
            btnAdelante = new Button();
            btnAtras = new Button();
            btnLimpiar = new Button();
            cbxTipo = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            btnsimulador = new Button();
            btnhistorial = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(228, 25);
            label1.Name = "label1";
            label1.Size = new Size(254, 38);
            label1.TabIndex = 0;
            label1.Text = "Registro de Pagos";
            // 
            // lstLista
            // 
            lstLista.FormattingEnabled = true;
            lstLista.Location = new Point(33, 155);
            lstLista.Name = "lstLista";
            lstLista.Size = new Size(257, 264);
            lstLista.TabIndex = 1;
            // 
            // btnGenerar
            // 
            btnGenerar.Location = new Point(329, 155);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(153, 44);
            btnGenerar.TabIndex = 3;
            btnGenerar.Text = "Generar Pago";
            btnGenerar.UseVisualStyleBackColor = true;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // btnCarrito
            // 
            btnCarrito.Location = new Point(329, 296);
            btnCarrito.Name = "btnCarrito";
            btnCarrito.Size = new Size(153, 44);
            btnCarrito.TabIndex = 6;
            btnCarrito.Text = "Volver al Carrito";
            btnCarrito.UseVisualStyleBackColor = true;
            btnCarrito.Click += btnCarrito_Click_1;
            // 
            // btnAdelante
            // 
            btnAdelante.Location = new Point(33, 425);
            btnAdelante.Name = "btnAdelante";
            btnAdelante.Size = new Size(111, 49);
            btnAdelante.TabIndex = 10;
            btnAdelante.Text = "Adelante";
            btnAdelante.UseVisualStyleBackColor = true;
            btnAdelante.Click += btnAdelante_Click;
            // 
            // btnAtras
            // 
            btnAtras.Location = new Point(180, 425);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(110, 49);
            btnAtras.TabIndex = 11;
            btnAtras.Text = "Atras";
            btnAtras.UseVisualStyleBackColor = true;
            btnAtras.Click += btnAtras_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(329, 226);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(153, 44);
            btnLimpiar.TabIndex = 17;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // cbxTipo
            // 
            cbxTipo.FormattingEnabled = true;
            cbxTipo.Location = new Point(505, 171);
            cbxTipo.Name = "cbxTipo";
            cbxTipo.Size = new Size(153, 28);
            cbxTipo.TabIndex = 19;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(505, 148);
            label4.Name = "label4";
            label4.Size = new Size(144, 20);
            label4.TabIndex = 20;
            label4.Text = "Tipo de Transacción:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 123);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 9;
            label3.Text = "label3";
            // 
            // btnsimulador
            // 
            btnsimulador.Location = new Point(329, 368);
            btnsimulador.Name = "btnsimulador";
            btnsimulador.Size = new Size(153, 51);
            btnsimulador.TabIndex = 21;
            btnsimulador.Text = "Simulador de errores";
            btnsimulador.UseVisualStyleBackColor = true;
            btnsimulador.Click += btnsimulador_Click;
            // 
            // btnhistorial
            // 
            btnhistorial.Location = new Point(329, 445);
            btnhistorial.Name = "btnhistorial";
            btnhistorial.Size = new Size(153, 29);
            btnhistorial.TabIndex = 22;
            btnhistorial.Text = "Ver historial";
            btnhistorial.UseVisualStyleBackColor = true;
            btnhistorial.Click += btnhistorial_Click;
            // 
            // FormPago
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 522);
            Controls.Add(btnhistorial);
            Controls.Add(btnsimulador);
            Controls.Add(label4);
            Controls.Add(cbxTipo);
            Controls.Add(btnLimpiar);
            Controls.Add(btnAtras);
            Controls.Add(btnAdelante);
            Controls.Add(label3);
            Controls.Add(btnCarrito);
            Controls.Add(btnGenerar);
            Controls.Add(lstLista);
            Controls.Add(label1);
            Name = "FormPago";
            Text = "FormPago";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListBox lstLista;
        private Button btnGenerar;
        private Button btnCarrito;
        private Button btnAdelante;
        private Button btnAtras;
        private Button btnLimpiar;
        private ComboBox cbxTipo;
        private Label label4;
        private Label label3;
        private Button btnsimulador;
        private Button btnhistorial;
    }
}