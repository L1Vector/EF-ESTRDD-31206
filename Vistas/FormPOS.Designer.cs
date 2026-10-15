namespace semana7Stack
{
    partial class FormPOS
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
            label1 = new Label();
            label2 = new Label();
            txtValor = new TextBox();
            lstVisualizacion = new ListBox();
            lblInfo = new Label();
            btnPush = new Button();
            btnPop = new Button();
            btnPeek = new Button();
            btnContains = new Button();
            btnLimpiar = new Button();
            btnPagar = new Button();
            txtCosto = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(150, 30);
            label1.Name = "label1";
            label1.Size = new Size(291, 41);
            label1.TabIndex = 0;
            label1.Text = "Carrito de Compras";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 116);
            label2.Name = "label2";
            label2.Size = new Size(61, 20);
            label2.TabIndex = 1;
            label2.Text = "Código:";
            // 
            // txtValor
            // 
            txtValor.Location = new Point(118, 113);
            txtValor.Margin = new Padding(3, 4, 3, 4);
            txtValor.Name = "txtValor";
            txtValor.Size = new Size(195, 27);
            txtValor.TabIndex = 2;
            // 
            // lstVisualizacion
            // 
            lstVisualizacion.FormattingEnabled = true;
            lstVisualizacion.Location = new Point(40, 151);
            lstVisualizacion.Margin = new Padding(3, 4, 3, 4);
            lstVisualizacion.Name = "lstVisualizacion";
            lstVisualizacion.Size = new Size(437, 384);
            lstVisualizacion.TabIndex = 3;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(40, 553);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(50, 20);
            lblInfo.TabIndex = 4;
            lblInfo.Text = "label3";
            // 
            // btnPush
            // 
            btnPush.Location = new Point(550, 151);
            btnPush.Margin = new Padding(3, 4, 3, 4);
            btnPush.Name = "btnPush";
            btnPush.Size = new Size(165, 45);
            btnPush.TabIndex = 5;
            btnPush.Text = "Agregar Productos";
            btnPush.UseVisualStyleBackColor = true;
            btnPush.Click += btnPush_Click;
            // 
            // btnPop
            // 
            btnPop.Location = new Point(550, 218);
            btnPop.Margin = new Padding(3, 4, 3, 4);
            btnPop.Name = "btnPop";
            btnPop.Size = new Size(165, 45);
            btnPop.TabIndex = 6;
            btnPop.Text = "Deshacer";
            btnPop.UseVisualStyleBackColor = true;
            btnPop.Click += btnPop_Click;
            // 
            // btnPeek
            // 
            btnPeek.Location = new Point(550, 285);
            btnPeek.Margin = new Padding(3, 4, 3, 4);
            btnPeek.Name = "btnPeek";
            btnPeek.Size = new Size(165, 45);
            btnPeek.TabIndex = 7;
            btnPeek.Text = "Ver último agregado";
            btnPeek.UseVisualStyleBackColor = true;
            btnPeek.Click += btnPeek_Click;
            // 
            // btnContains
            // 
            btnContains.Location = new Point(550, 352);
            btnContains.Margin = new Padding(3, 4, 3, 4);
            btnContains.Name = "btnContains";
            btnContains.Size = new Size(165, 45);
            btnContains.TabIndex = 8;
            btnContains.Text = "Buscar Atrículo";
            btnContains.UseVisualStyleBackColor = true;
            btnContains.Click += btnContains_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(550, 419);
            btnLimpiar.Margin = new Padding(3, 4, 3, 4);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(165, 45);
            btnLimpiar.TabIndex = 10;
            btnLimpiar.Text = "Vaciar Carrito";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnPagar
            // 
            btnPagar.Location = new Point(550, 486);
            btnPagar.Name = "btnPagar";
            btnPagar.Size = new Size(165, 45);
            btnPagar.TabIndex = 11;
            btnPagar.Text = "Pagar";
            btnPagar.UseVisualStyleBackColor = true;
            btnPagar.Click += btnPagar_Click;
            // 
            // txtCosto
            // 
            txtCosto.Location = new Point(393, 113);
            txtCosto.Margin = new Padding(3, 4, 3, 4);
            txtCosto.Name = "txtCosto";
            txtCosto.Size = new Size(84, 27);
            txtCosto.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(337, 116);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 13;
            label3.Text = "Costo:";
            // 
            // FormPOS
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(756, 600);
            Controls.Add(label3);
            Controls.Add(txtCosto);
            Controls.Add(btnPagar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnContains);
            Controls.Add(btnPeek);
            Controls.Add(btnPop);
            Controls.Add(btnPush);
            Controls.Add(lblInfo);
            Controls.Add(lstVisualizacion);
            Controls.Add(txtValor);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormPOS";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtValor;
        private ListBox lstVisualizacion;
        private Label lblInfo;
        private Button btnPush;
        private Button btnPop;
        private Button btnPeek;
        private Button btnContains;
        private Button btnLimpiar;
        private Button btnPagar;
        private TextBox txtCosto;
        private Label label3;
    }
}
