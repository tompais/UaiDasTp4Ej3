namespace UaiDasTp4Ej3
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuPrincipal;
        private ToolStripMenuItem menuAdministracion;
      private ToolStripMenuItem menuCategorias;
    private ToolStripMenuItem menuPreguntas;
        private ToolStripMenuItem menuJugadores;
  private ToolStripMenuItem menuJuego;
        private ToolStripMenuItem menuIniciarJuego;
        private ToolStripMenuItem menuHistorialPartidas;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
 {
        components.Dispose();
 }
         base.Dispose(disposing);
     }

   private void InitializeComponent()
        {
     this.menuPrincipal = new System.Windows.Forms.MenuStrip();
    this.menuAdministracion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCategorias = new System.Windows.Forms.ToolStripMenuItem();
         this.menuPreguntas = new System.Windows.Forms.ToolStripMenuItem();
          this.menuJugadores = new System.Windows.Forms.ToolStripMenuItem();
          this.menuJuego = new System.Windows.Forms.ToolStripMenuItem();
            this.menuIniciarJuego = new System.Windows.Forms.ToolStripMenuItem();
         this.menuHistorialPartidas = new System.Windows.Forms.ToolStripMenuItem();
          this.menuPrincipal.SuspendLayout();
    this.SuspendLayout();
   // 
    // menuPrincipal
    // 
     this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAdministracion,
            this.menuJuego});
       this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
        this.menuPrincipal.Name = "menuPrincipal";
        this.menuPrincipal.Size = new System.Drawing.Size(1200, 24);
     this.menuPrincipal.TabIndex = 1;
            this.menuPrincipal.Text = "menuStrip1";
            // 
         // menuAdministracion
      // 
 this.menuAdministracion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCategorias,
        this.menuPreguntas,
            this.menuJugadores});
   this.menuAdministracion.Name = "menuAdministracion";
     this.menuAdministracion.Size = new System.Drawing.Size(100, 20);
       this.menuAdministracion.Text = "Administración";
          // 
            // menuCategorias
            // 
         this.menuCategorias.Name = "menuCategorias";
            this.menuCategorias.Size = new System.Drawing.Size(180, 22);
            this.menuCategorias.Text = "Categorías";
            this.menuCategorias.Click += new System.EventHandler(this.MenuCategorias_Click);
            // 
        // menuPreguntas
            // 
 this.menuPreguntas.Name = "menuPreguntas";
      this.menuPreguntas.Size = new System.Drawing.Size(180, 22);
     this.menuPreguntas.Text = "Preguntas";
       this.menuPreguntas.Click += new System.EventHandler(this.MenuPreguntas_Click);
   // 
            // menuJugadores
// 
  this.menuJugadores.Name = "menuJugadores";
 this.menuJugadores.Size = new System.Drawing.Size(180, 22);
          this.menuJugadores.Text = "Jugadores";
  this.menuJugadores.Click += new System.EventHandler(this.MenuJugadores_Click);
    // 
     // menuJuego
            // 
            this.menuJuego.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
     this.menuIniciarJuego,
 this.menuHistorialPartidas});
       this.menuJuego.Name = "menuJuego";
            this.menuJuego.Size = new System.Drawing.Size(50, 20);
        this.menuJuego.Text = "Juego";
            // 
       // menuIniciarJuego
          // 
        this.menuIniciarJuego.Name = "menuIniciarJuego";
            this.menuIniciarJuego.Size = new System.Drawing.Size(180, 22);
     this.menuIniciarJuego.Text = "Iniciar Juego";
    this.menuIniciarJuego.Click += new System.EventHandler(this.MenuIniciarJuego_Click);
   // 
            // menuHistorialPartidas
            // 
      this.menuHistorialPartidas.Name = "menuHistorialPartidas";
            this.menuHistorialPartidas.Size = new System.Drawing.Size(180, 22);
            this.menuHistorialPartidas.Text = "Historial de Partidas";
     this.menuHistorialPartidas.Click += new System.EventHandler(this.MenuHistorialPartidas_Click);
  // 
            // FormPrincipal
            // 
       this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
this.Controls.Add(this.menuPrincipal);
      this.IsMdiContainer = true;
        this.MainMenuStrip = this.menuPrincipal;
       this.Name = "FormPrincipal";
  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Trivia - UAI DAS TP4";
this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
   this.menuPrincipal.ResumeLayout(false);
          this.menuPrincipal.PerformLayout();
            this.ResumeLayout(false);
          this.PerformLayout();
        }
 }
}
