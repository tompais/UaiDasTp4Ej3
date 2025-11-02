using APP;
using ABS;

namespace UaiDasTp4Ej3;

public partial class FormSeleccionJugador : Form
{
    private readonly IRepositorioJugador _repositorioJugador;
    private readonly IRepositorioPartidaJuego _repositorioPartida;
    private ComboBox cboJugador = null!;
    private Button btnIniciar = null!;
    private Button btnCancelar = null!;
    private Label lblSeleccione = null!;

    public FormSeleccionJugador()
 {
        _repositorioJugador = Configuracion.ObtenerServicio<IRepositorioJugador>();
        _repositorioPartida = Configuracion.ObtenerServicio<IRepositorioPartidaJuego>();
   InitializeComponent();
        CargarJugadores();
    }

private void InitializeComponent()
    {
   this.cboJugador = new ComboBox();
     this.btnIniciar = new Button();
   this.btnCancelar = new Button();
  this.lblSeleccione = new Label();
     this.SuspendLayout();
        
     // lblSeleccione
        this.lblSeleccione.AutoSize = true;
        this.lblSeleccione.Location = new Point(20, 20);
    this.lblSeleccione.Name = "lblSeleccione";
     this.lblSeleccione.Size = new Size(130, 15);
 this.lblSeleccione.TabIndex = 0;
     this.lblSeleccione.Text = "Seleccione el jugador:";
   
   // cboJugador
   this.cboJugador.DropDownStyle = ComboBoxStyle.DropDownList;
   this.cboJugador.FormattingEnabled = true;
   this.cboJugador.Location = new Point(20, 40);
        this.cboJugador.Name = "cboJugador";
        this.cboJugador.Size = new Size(350, 23);
   this.cboJugador.TabIndex = 1;
        
   // btnIniciar
        this.btnIniciar.Location = new Point(20, 80);
   this.btnIniciar.Name = "btnIniciar";
        this.btnIniciar.Size = new Size(120, 35);
  this.btnIniciar.TabIndex = 2;
this.btnIniciar.Text = "Iniciar Juego";
    this.btnIniciar.UseVisualStyleBackColor = true;
        this.btnIniciar.Click += BtnIniciar_Click;
   
        // btnCancelar
        this.btnCancelar.Location = new Point(150, 80);
   this.btnCancelar.Name = "btnCancelar";
 this.btnCancelar.Size = new Size(120, 35);
     this.btnCancelar.TabIndex = 3;
        this.btnCancelar.Text = "Cancelar";
   this.btnCancelar.UseVisualStyleBackColor = true;
    this.btnCancelar.Click += BtnCancelar_Click;
   
        // FormSeleccionJugador
   this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
this.ClientSize = new Size(394, 141);
        this.Controls.Add(this.btnCancelar);
     this.Controls.Add(this.btnIniciar);
     this.Controls.Add(this.cboJugador);
        this.Controls.Add(this.lblSeleccione);
   this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
        this.MinimizeBox = false;
  this.Name = "FormSeleccionJugador";
   this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Iniciar Juego - Seleccionar Jugador";
     this.ResumeLayout(false);
        this.PerformLayout();
  }

  private void CargarJugadores()
    {
        try
        {
       var jugadores = _repositorioJugador.ObtenerTodos();
     
            if (jugadores.Count == 0)
       {
    MessageBox.Show("No hay jugadores registrados. Por favor, registre un jugador primero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
         this.Close();
        return;
            }

            cboJugador.DataSource = jugadores;
     cboJugador.DisplayMember = "Nombre";
 cboJugador.ValueMember = "Id";
        }
   catch (Exception ex)
  {
         MessageBox.Show($"Error al cargar jugadores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
}

    private void BtnIniciar_Click(object? sender, EventArgs e)
    {
        if (cboJugador.SelectedValue == null)
 {
 MessageBox.Show("Debe seleccionar un jugador", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    return;
   }

  try
   {
    var jugadorId = (int)cboJugador.SelectedValue;
            
   // Verificar si el jugador ya tiene una partida en curso
       var partidaEnCurso = _repositorioPartida.ObtenerPartidaEnCurso(jugadorId);
            
     if (partidaEnCurso != null)
        {
    var resultado = MessageBox.Show(
      "El jugador tiene una partida en curso. ¿Desea continuarla?",
        "Partida en Curso",
      MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
        );
  
      if (resultado == DialogResult.Yes)
    {
    AbrirFormularioJuego(partidaEnCurso.Id, jugadorId);
            }
  }
   else
         {
      // Crear nueva partida
var servicioJuego = Configuracion.ObtenerServicio<SERV.ServicioJuego>();
var partida = servicioJuego.IniciarPartida(jugadorId);

    // Necesitamos obtener el ID de la partida recién creada
   var partidaNueva = _repositorioPartida.ObtenerPartidaEnCurso(jugadorId);
  
     if (partidaNueva != null)
        {
      AbrirFormularioJuego(partidaNueva.Id, jugadorId);
       }
   else
       {
 MessageBox.Show("Error al crear la partida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
       }
     }
     }
catch (Exception ex)
    {
MessageBox.Show($"Error al iniciar el juego: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void AbrirFormularioJuego(int partidaId, int jugadorId)
    {
        var formJuego = new FormJuego(partidaId, jugadorId);
        
        if (this.MdiParent != null)
{
 formJuego.MdiParent = this.MdiParent;
        }
  
  formJuego.Show();
        this.Close();
    }

    private void BtnCancelar_Click(object? sender, EventArgs e) => this.Close();
}
