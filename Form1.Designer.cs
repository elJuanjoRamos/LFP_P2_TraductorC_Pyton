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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteTokensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteErroresLexicosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tablaDeSimbolosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limpiaDocumentosRecientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textAnalizar
            // 
            this.textAnalizar.Location = new System.Drawing.Point(34, 207);
            this.textAnalizar.Name = "textAnalizar";
            this.textAnalizar.Size = new System.Drawing.Size(862, 390);
            this.textAnalizar.TabIndex = 0;
            this.textAnalizar.Text = "";
            // 
            // consolaTexto
            // 
            this.consolaTexto.Location = new System.Drawing.Point(34, 642);
            this.consolaTexto.Name = "consolaTexto";
            this.consolaTexto.Size = new System.Drawing.Size(862, 159);
            this.consolaTexto.TabIndex = 1;
            this.consolaTexto.Text = "";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(30, 615);
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
            this.richTraduccion.Location = new System.Drawing.Point(922, 207);
            this.richTraduccion.Name = "richTraduccion";
            this.richTraduccion.Size = new System.Drawing.Size(523, 601);
            this.richTraduccion.TabIndex = 6;
            this.richTraduccion.Text = "";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(918, 168);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(104, 24);
            this.materialLabel3.TabIndex = 7;
            this.materialLabel3.Text = "Traduccion";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.documentoToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(9, 81);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(397, 28);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.AbrirToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.GuardarToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.salirToolStripMenuItem.Text = "Salir";
            // 
            // documentoToolStripMenuItem
            // 
            this.documentoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportesToolStripMenuItem,
            this.limpiaDocumentosRecientesToolStripMenuItem});
            this.documentoToolStripMenuItem.Name = "documentoToolStripMenuItem";
            this.documentoToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.documentoToolStripMenuItem.Text = "Documento";
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reporteTokensToolStripMenuItem,
            this.reporteErroresLexicosToolStripMenuItem,
            this.reporteToolStripMenuItem,
            this.tablaDeSimbolosToolStripMenuItem});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // reporteTokensToolStripMenuItem
            // 
            this.reporteTokensToolStripMenuItem.Name = "reporteTokensToolStripMenuItem";
            this.reporteTokensToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            this.reporteTokensToolStripMenuItem.Text = "Reporte Tokens";
            this.reporteTokensToolStripMenuItem.Click += new System.EventHandler(this.ReporteTokensToolStripMenuItem_Click);
            // 
            // reporteErroresLexicosToolStripMenuItem
            // 
            this.reporteErroresLexicosToolStripMenuItem.Name = "reporteErroresLexicosToolStripMenuItem";
            this.reporteErroresLexicosToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            this.reporteErroresLexicosToolStripMenuItem.Text = "Reporte Errores Lexicos";
            this.reporteErroresLexicosToolStripMenuItem.Click += new System.EventHandler(this.ReporteErroresLexicosToolStripMenuItem_Click);
            // 
            // reporteToolStripMenuItem
            // 
            this.reporteToolStripMenuItem.Name = "reporteToolStripMenuItem";
            this.reporteToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            this.reporteToolStripMenuItem.Text = "Reporte Errorres Sintactico";
            // 
            // tablaDeSimbolosToolStripMenuItem
            // 
            this.tablaDeSimbolosToolStripMenuItem.Name = "tablaDeSimbolosToolStripMenuItem";
            this.tablaDeSimbolosToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            this.tablaDeSimbolosToolStripMenuItem.Text = "Tabla de Simbolos";
            // 
            // limpiaDocumentosRecientesToolStripMenuItem
            // 
            this.limpiaDocumentosRecientesToolStripMenuItem.Name = "limpiaDocumentosRecientesToolStripMenuItem";
            this.limpiaDocumentosRecientesToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.limpiaDocumentosRecientesToolStripMenuItem.Text = "Limpia Documentos Recientes";
            this.limpiaDocumentosRecientesToolStripMenuItem.Click += new System.EventHandler(this.LimpiaDocumentosRecientesToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1519, 863);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.richTraduccion);
            this.Controls.Add(this.materialFlatButton1);
            this.Controls.Add(this.analizar);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.consolaTexto);
            this.Controls.Add(this.textAnalizar);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteTokensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteErroresLexicosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tablaDeSimbolosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem limpiaDocumentosRecientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
    }
}

