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
            label2 = new Label();
            button1 = new Button();
            txtCódigo = new TextBox();
            btnCarrito = new Button();
            btnError = new Button();
            btnEliminar = new Button();
            label3 = new Label();
            btnAdelante = new Button();
            btnAtras = new Button();
            btnLimpiar = new Button();
            btnValidar = new Button();
            cbxTipo = new ComboBox();
            label4 = new Label();
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 122);
            label2.Name = "label2";
            label2.Size = new Size(65, 20);
            label2.TabIndex = 2;
            label2.Text = "Código: ";
            // 
            // button1
            // 
            button1.Location = new Point(329, 155);
            button1.Name = "button1";
            button1.Size = new Size(153, 44);
            button1.TabIndex = 3;
            button1.Text = "Registrar Pago";
            button1.UseVisualStyleBackColor = true;
            // 
            // txtCódigo
            // 
            txtCódigo.Location = new Point(104, 119);
            txtCódigo.Name = "txtCódigo";
            txtCódigo.Size = new Size(186, 27);
            txtCódigo.TabIndex = 4;
            // 
            // btnCarrito
            // 
            btnCarrito.Location = new Point(505, 375);
            btnCarrito.Name = "btnCarrito";
            btnCarrito.Size = new Size(153, 44);
            btnCarrito.TabIndex = 6;
            btnCarrito.Text = "Volver al Carrito";
            btnCarrito.UseVisualStyleBackColor = true;
            // 
            // btnError
            // 
            btnError.Location = new Point(329, 300);
            btnError.Name = "btnError";
            btnError.Size = new Size(153, 46);
            btnError.TabIndex = 7;
            btnError.Text = "Registro de Errores";
            btnError.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(505, 300);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(153, 46);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar Registro";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 491);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 9;
            label3.Text = "label3";
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
            btnLimpiar.Location = new Point(329, 375);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(153, 44);
            btnLimpiar.TabIndex = 17;
            btnLimpiar.Text = "Limpiar Registros";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnValidar
            // 
            btnValidar.Location = new Point(329, 224);
            btnValidar.Name = "btnValidar";
            btnValidar.Size = new Size(153, 44);
            btnValidar.TabIndex = 18;
            btnValidar.Text = "Validar Pago";
            btnValidar.UseVisualStyleBackColor = true;
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
            // FormPago
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 522);
            Controls.Add(label4);
            Controls.Add(cbxTipo);
            Controls.Add(btnValidar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnAtras);
            Controls.Add(btnAdelante);
            Controls.Add(label3);
            Controls.Add(btnEliminar);
            Controls.Add(btnError);
            Controls.Add(btnCarrito);
            Controls.Add(txtCódigo);
            Controls.Add(button1);
            Controls.Add(label2);
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
        private Label label2;
        private Button button1;
        private TextBox txtCódigo;
        private Button btnCarrito;
        private Button btnError;
        private Button btnEliminar;
        private Label label3;
        private Button btnAdelante;
        private Button btnAtras;
        private Button btnLimpiar;
        private Button btnValidar;
        private ComboBox cbxTipo;
        private Label label4;
    }
}