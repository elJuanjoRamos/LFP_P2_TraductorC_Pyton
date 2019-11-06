namespace LFP_P2_TraductorC_Pyton
{
    partial class Form1
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
            this.textAnalizar = new System.Windows.Forms.RichTextBox();
            this.consolaTexto = new System.Windows.Forms.RichTextBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.analizar = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialFlatButton1 = new MaterialSkin.Controls.MaterialFlatButton();
            this.richTraduccion = new System.Windows.Forms.RichTextBox();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();
            // 
            // textAnalizar
            // 
            this.textAnalizar.Location = new System.Drawing.Point(34, 207);
            this.textAnalizar.Name = "textAnalizar";
            this.textAnalizar.Size = new System.Drawing.Size(803, 330);
            this.textAnalizar.TabIndex = 0;
            this.textAnalizar.Text = "";
            // 
            // consolaTexto
            // 
            this.consolaTexto.Location = new System.Drawing.Point(34, 603);
            this.consolaTexto.Name = "consolaTexto";
            this.consolaTexto.Size = new System.Drawing.Size(803, 133);
            this.consolaTexto.TabIndex = 1;
            this.consolaTexto.Text = "";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(30, 567);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(79, 24);
            this.materialLabel1.TabIndex = 2;
            this.materialLabel1.Text = "Consola";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(30, 168);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(57, 24);
            this.materialLabel2.TabIndex = 3;
            this.materialLabel2.Text = "Texto";
            // 
            // analizar
            // 
            this.analizar.AutoSize = true;
            this.analizar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.analizar.Depth = 0;
            this.analizar.Location = new System.Drawing.Point(742, 156);
            this.analizar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.analizar.MouseState = MaterialSkin.MouseState.HOVER;
            this.analizar.Name = "analizar";
            this.analizar.Primary = false;
            this.analizar.Size = new System.Drawing.Size(95, 36);
            this.analizar.TabIndex = 4;
            this.analizar.Text = "Analizar";
            this.analizar.UseVisualStyleBackColor = true;
            this.analizar.Click += new System.EventHandler(this.Analizar_Click);
            // 
            // materialFlatButton1
            // 
            this.materialFlatButton1.AutoSize = true;
            this.materialFlatButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton1.Depth = 0;
            this.materialFlatButton1.Location = new System.Drawing.Point(493, 156);
            this.materialFlatButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButton1.Name = "materialFlatButton1";
            this.materialFlatButton1.Primary = false;
            this.materialFlatButton1.Size = new System.Drawing.Size(213, 36);
            this.materialFlatButton1.TabIndex = 5;
            this.materialFlatButton1.Text = "materialFlatButton1";
            this.materialFlatButton1.UseVisualStyleBackColor = true;
            this.materialFlatButton1.Click += new System.EventHandler(this.MaterialFlatButton1_Click);
            // 
            // richTraduccion
            // 
            this.richTraduccion.Location = new System.Drawing.Point(866, 200);
            this.richTraduccion.Name = "richTraduccion";
            this.richTraduccion.Size = new System.Drawing.Size(428, 536);
            this.richTraduccion.TabIndex = 6;
            this.richTraduccion.Text = "";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(862, 168);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(104, 24);
            this.materialLabel3.TabIndex = 7;
            this.materialLabel3.Text = "Traduccion";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1395, 794);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.richTraduccion);
            this.Controls.Add(this.materialFlatButton1);
            this.Controls.Add(this.analizar);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.consolaTexto);
            this.Controls.Add(this.textAnalizar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox textAnalizar;
        private System.Windows.Forms.RichTextBox consolaTexto;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialFlatButton analizar;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton1;
        private System.Windows.Forms.RichTextBox richTraduccion;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
    }
}

