namespace UaiDasTp4Ej3
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
          InitializeComponent();
        }

        private void MenuCategorias_Click(object? sender, EventArgs e) => AbrirFormularioHijo<FormCategorias>();

  private void MenuPreguntas_Click(object? sender, EventArgs e) => AbrirFormularioHijo<FormPreguntas>();

      private void MenuJugadores_Click(object? sender, EventArgs e) => AbrirFormularioHijo<FormJugadores>();

      private void MenuIniciarJuego_Click(object? sender, EventArgs e) => AbrirFormularioHijo<FormSeleccionJugador>();

   private void MenuHistorialPartidas_Click(object? sender, EventArgs e) => AbrirFormularioHijo<FormHistorialPartidas>();

        private void AbrirFormularioHijo<T>() where T : Form, new()
{
       // Verificar si ya existe una instancia abierta
            var formularioExistente = this.MdiChildren.OfType<T>().FirstOrDefault();

            if (formularioExistente != null)
            {
             formularioExistente.Activate();
   }
            else
      {
       var formulario = new T
          {
  MdiParent = this
         };
   formulario.Show();
            }
        }
    }
}
