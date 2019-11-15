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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.consolaTexto = new System.Windows.Forms.RichTextBox();
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
            this.graficaDeVectoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarTraduccionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limpiaDocumentosRecientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTraduccion = new System.Windows.Forms.RichTextBox();
            this.textAnalizar = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textAnalizar)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // consolaTexto
            // 
            this.consolaTexto.BackColor = System.Drawing.SystemColors.MenuBar;
            this.consolaTexto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.consolaTexto.Location = new System.Drawing.Point(12, 821);
            this.consolaTexto.Name = "consolaTexto";
            this.consolaTexto.Size = new System.Drawing.Size(1442, 209);
            this.consolaTexto.TabIndex = 1;
            this.consolaTexto.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.documentoToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(9, 97);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(247, 28);
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
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.AbrirToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.GuardarToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.salirToolStripMenuItem.Text = "Salir";
            // 
            // documentoToolStripMenuItem
            // 
            this.documentoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analizarToolStripMenuItem,
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
            this.tablaDeSimbolosToolStripMenuItem,
            this.graficaDeVectoresToolStripMenuItem,
            this.guardarTraduccionToolStripMenuItem});
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
            this.reporteToolStripMenuItem.Click += new System.EventHandler(this.ReporteToolStripMenuItem_Click);
            // 
            // tablaDeSimbolosToolStripMenuItem
            // 
            this.tablaDeSimbolosToolStripMenuItem.Name = "tablaDeSimbolosToolStripMenuItem";
            this.tablaDeSimbolosToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            this.tablaDeSimbolosToolStripMenuItem.Text = "Tabla de Simbolos";
            this.tablaDeSimbolosToolStripMenuItem.Click += new System.EventHandler(this.TablaDeSimbolosToolStripMenuItem_Click);
            // 
            // graficaDeVectoresToolStripMenuItem
            // 
            this.graficaDeVectoresToolStripMenuItem.Name = "graficaDeVectoresToolStripMenuItem";
            this.graficaDeVectoresToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            this.graficaDeVectoresToolStripMenuItem.Text = "Grafica de Vectores";
            this.graficaDeVectoresToolStripMenuItem.Click += new System.EventHandler(this.GraficaDeVectoresToolStripMenuItem_Click);
            // 
            // guardarTraduccionToolStripMenuItem
            // 
            this.guardarTraduccionToolStripMenuItem.Name = "guardarTraduccionToolStripMenuItem";
            this.guardarTraduccionToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            this.guardarTraduccionToolStripMenuItem.Text = "Guardar Traduccion";
            this.guardarTraduccionToolStripMenuItem.Click += new System.EventHandler(this.GuardarTraduccionToolStripMenuItem_Click);
            // 
            // limpiaDocumentosRecientesToolStripMenuItem
            // 
            this.limpiaDocumentosRecientesToolStripMenuItem.Name = "limpiaDocumentosRecientesToolStripMenuItem";
            this.limpiaDocumentosRecientesToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.limpiaDocumentosRecientesToolStripMenuItem.Text = "Limpia Documentos Recientes";
            this.limpiaDocumentosRecientesToolStripMenuItem.Click += new System.EventHandler(this.LimpiaDocumentosRecientesToolStripMenuItem_Click);
            // 
            // analizarToolStripMenuItem
            // 
            this.analizarToolStripMenuItem.Name = "analizarToolStripMenuItem";
            this.analizarToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.analizarToolStripMenuItem.Text = "Analizar";
            this.analizarToolStripMenuItem.Click += new System.EventHandler(this.AnalizarToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // richTraduccion
            // 
            this.richTraduccion.BackColor = System.Drawing.SystemColors.Menu;
            this.richTraduccion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTraduccion.Location = new System.Drawing.Point(1460, 180);
            this.richTraduccion.Name = "richTraduccion";
            this.richTraduccion.Size = new System.Drawing.Size(557, 850);
            this.richTraduccion.TabIndex = 10;
            this.richTraduccion.Text = "";
            // 
            // textAnalizar
            // 
            this.textAnalizar.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.textAnalizar.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:]*" +
    "(?<range>:)\\s*(?<range>[^;]+);";
            this.textAnalizar.AutoScrollMinSize = new System.Drawing.Size(31, 18);
            this.textAnalizar.BackBrush = null;
            this.textAnalizar.CharHeight = 18;
            this.textAnalizar.CharWidth = 10;
            this.textAnalizar.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textAnalizar.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textAnalizar.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.textAnalizar.IsReplaceMode = false;
            this.textAnalizar.Location = new System.Drawing.Point(6, 6);
            this.textAnalizar.Name = "textAnalizar";
            this.textAnalizar.Paddings = new System.Windows.Forms.Padding(0);
            this.textAnalizar.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textAnalizar.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textAnalizar.ServiceColors")));
            this.textAnalizar.Size = new System.Drawing.Size(1425, 619);
            this.textAnalizar.TabIndex = 9;
            this.textAnalizar.Zoom = 100;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(9, 155);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1445, 660);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textAnalizar);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1437, 631);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pestaña 1";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.acercaDeToolStripMenuItem.Text = "Acerca De";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.AcercaDeToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1920, 1055);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.richTraduccion);
            this.Controls.Add(this.consolaTexto);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textAnalizar)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox consolaTexto;
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
        private System.Windows.Forms.RichTextBox richTraduccion;
        private System.Windows.Forms.ToolStripMenuItem graficaDeVectoresToolStripMenuItem;
        private FastColoredTextBoxNS.FastColoredTextBox textAnalizar;
        private System.Windows.Forms.ToolStripMenuItem guardarTraduccionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analizarToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
    }
}

