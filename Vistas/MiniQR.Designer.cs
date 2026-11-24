namespace semana7Stack.Vistas
{
    partial class MiniQR
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
            components = new System.ComponentModel.Container();
            lblMonto = new Label();
            picQR = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            lblTiempo = new Label();
            btnSimularFalloRed = new Button();
            ((System.ComponentModel.ISupportInitialize)picQR).BeginInit();
            SuspendLayout();
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Location = new Point(12, 342);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(38, 15);
            lblMonto.TabIndex = 0;
            lblMonto.Text = "label1";
            // 
            // picQR
            // 
            picQR.Image = EF_Estructu_III.Properties.Resources.dawid_bochno_thumb;
            picQR.Location = new Point(39, 9);
            picQR.Margin = new Padding(3, 2, 3, 2);
            picQR.Name = "picQR";
            picQR.Size = new Size(333, 270);
            picQR.SizeMode = PictureBoxSizeMode.Zoom;
            picQR.TabIndex = 1;
            picQR.TabStop = false;
            // 
            // lblTiempo
            // 
            lblTiempo.AutoSize = true;
            lblTiempo.Location = new Point(231, 342);
            lblTiempo.Name = "lblTiempo";
            lblTiempo.Size = new Size(38, 15);
            lblTiempo.TabIndex = 2;
            lblTiempo.Text = "label1";
            // 
            // btnSimularFalloRed
            // 
            btnSimularFalloRed.BackColor = Color.FromArgb(226, 30, 28);
            btnSimularFalloRed.FlatAppearance.BorderSize = 0;
            btnSimularFalloRed.FlatStyle = FlatStyle.Flat;
            btnSimularFalloRed.Font = new Font("Microsoft Sans Serif", 10.8F);
            btnSimularFalloRed.Location = new Point(132, 284);
            btnSimularFalloRed.Name = "btnSimularFalloRed";
            btnSimularFalloRed.Size = new Size(171, 40);
            btnSimularFalloRed.TabIndex = 28;
            btnSimularFalloRed.Text = "Simular falla de red";
            btnSimularFalloRed.UseVisualStyleBackColor = false;
            btnSimularFalloRed.Click += btnSimularFalloRed_Click;
            // 
            // MiniQR
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(443, 382);
            Controls.Add(btnSimularFalloRed);
            Controls.Add(lblTiempo);
            Controls.Add(picQR);
            Controls.Add(lblMonto);
            Margin = new Padding(3, 2, 3, 2);
            Name = "MiniQR";
            Text = "MiniQR";
            ((System.ComponentModel.ISupportInitialize)picQR).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMonto;
        private PictureBox picQR;
        private System.Windows.Forms.Timer timer1;
        private Label lblTiempo;
        private Button btnSimularFalloRed;
    }
}