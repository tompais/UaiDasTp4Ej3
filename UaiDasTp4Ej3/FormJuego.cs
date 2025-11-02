using APP;
using ABS;
using SERV;
using DOM;

namespace UaiDasTp4Ej3;

public partial class FormJuego : Form
{
    private readonly int _partidaId;
    private readonly int _jugadorId;
  private readonly ServicioJuego _servicioJuego;
    private Pregunta? _preguntaActual;
    
    private Label lblNivel = null!;
    private Label lblPuntaje = null!;
    private Label lblPregunta = null!;
    private GroupBox grpOpciones = null!;
    private RadioButton[] rbOpciones = null!;
    private Button btnResponder = null!;
    private Button btnSiguiente = null!;
    private Button btnFinalizarJuego = null!;
    private Label lblResultado = null!;

    public FormJuego(int partidaId, int jugadorId)
{
 _partidaId = partidaId;
        _jugadorId = jugadorId;
        _servicioJuego = Configuracion.ObtenerServicio<ServicioJuego>();
        rbOpciones = new RadioButton[4];
        
    InitializeComponent();
        ActualizarEstadoPartida();
        CargarSiguientePregunta();
    }

  private void InitializeComponent()
  {
        this.lblNivel = new Label();
        this.lblPuntaje = new Label();
     this.lblPregunta = new Label();
        this.grpOpciones = new GroupBox();
        this.btnResponder = new Button();
   this.btnSiguiente = new Button();
        this.btnFinalizarJuego = new Button();
        this.lblResultado = new Label();
      
        for (int i = 0; i < 4; i++)
 {
   this.rbOpciones[i] = new RadioButton();
        }
  
        this.grpOpciones.SuspendLayout();
   this.SuspendLayout();
     
     // lblNivel
 this.lblNivel.AutoSize = true;
this.lblNivel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
this.lblNivel.Location = new Point(20, 20);
  this.lblNivel.Name = "lblNivel";
     this.lblNivel.Size = new Size(80, 21);
        this.lblNivel.TabIndex = 0;
   this.lblNivel.Text = "Nivel: 1";
     
  // lblPuntaje
        this.lblPuntaje.AutoSize = true;
   this.lblPuntaje.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
      this.lblPuntaje.Location = new Point(20, 50);
  this.lblPuntaje.Name = "lblPuntaje";
        this.lblPuntaje.Size = new Size(90, 21);
this.lblPuntaje.TabIndex = 1;
   this.lblPuntaje.Text = "Puntaje: 0";

        // lblPregunta
this.lblPregunta.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        this.lblPregunta.Location = new Point(20, 90);
 this.lblPregunta.Name = "lblPregunta";
        this.lblPregunta.Size = new Size(760, 80);
        this.lblPregunta.TabIndex = 2;
   this.lblPregunta.Text = "Pregunta aparecerá aquí";
   
        // grpOpciones
this.grpOpciones.Location = new Point(20, 190);
        this.grpOpciones.Name = "grpOpciones";
        this.grpOpciones.Size = new Size(760, 200);
        this.grpOpciones.TabIndex = 3;
      this.grpOpciones.TabStop = false;
   this.grpOpciones.Text = "Opciones";
   
        // rbOpciones
        for (int i = 0; i < 4; i++)
        {
  this.rbOpciones[i].AutoSize = true;
   this.rbOpciones[i].Font = new Font("Segoe UI", 11F);
   this.rbOpciones[i].Location = new Point(20, 30 + (i * 40));
            this.rbOpciones[i].Name = $"rbOpcion{i}";
       this.rbOpciones[i].Size = new Size(100, 24);
       this.rbOpciones[i].TabIndex = i;
            this.rbOpciones[i].Text = $"Opción {i + 1}";
            this.grpOpciones.Controls.Add(this.rbOpciones[i]);
  }
 
  // lblResultado
this.lblResultado.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        this.lblResultado.Location = new Point(20, 410);
  this.lblResultado.Name = "lblResultado";
   this.lblResultado.Size = new Size(760, 40);
        this.lblResultado.TabIndex = 4;
   this.lblResultado.TextAlign = ContentAlignment.MiddleCenter;
        this.lblResultado.Visible = false;
        
        // btnResponder
        this.btnResponder.Font = new Font("Segoe UI", 12F);
   this.btnResponder.Location = new Point(20, 470);
  this.btnResponder.Name = "btnResponder";
        this.btnResponder.Size = new Size(150, 40);
        this.btnResponder.TabIndex = 5;
 this.btnResponder.Text = "Responder";
   this.btnResponder.UseVisualStyleBackColor = true;
        this.btnResponder.Click += BtnResponder_Click;
     
        // btnSiguiente
 this.btnSiguiente.Font = new Font("Segoe UI", 12F);
        this.btnSiguiente.Location = new Point(190, 470);
        this.btnSiguiente.Name = "btnSiguiente";
  this.btnSiguiente.Size = new Size(150, 40);
 this.btnSiguiente.TabIndex = 6;
        this.btnSiguiente.Text = "Siguiente";
        this.btnSiguiente.UseVisualStyleBackColor = true;
this.btnSiguiente.Visible = false;
   this.btnSiguiente.Click += BtnSiguiente_Click;
        
     // btnFinalizarJuego
        this.btnFinalizarJuego.Font = new Font("Segoe UI", 12F);
        this.btnFinalizarJuego.Location = new Point(630, 470);
  this.btnFinalizarJuego.Name = "btnFinalizarJuego";
        this.btnFinalizarJuego.Size = new Size(150, 40);
     this.btnFinalizarJuego.TabIndex = 7;
    this.btnFinalizarJuego.Text = "Finalizar Juego";
 this.btnFinalizarJuego.UseVisualStyleBackColor = true;
        this.btnFinalizarJuego.Click += BtnFinalizarJuego_Click;
 
    // FormJuego
        this.AutoScaleDimensions = new SizeF(7F, 15F);
   this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(800, 530);
   this.Controls.Add(this.btnFinalizarJuego);
        this.Controls.Add(this.btnSiguiente);
   this.Controls.Add(this.btnResponder);
        this.Controls.Add(this.lblResultado);
     this.Controls.Add(this.grpOpciones);
this.Controls.Add(this.lblPregunta);
   this.Controls.Add(this.lblPuntaje);
    this.Controls.Add(this.lblNivel);
   this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = "FormJuego";
        this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Juego de Trivia";
        this.grpOpciones.ResumeLayout(false);
   this.grpOpciones.PerformLayout();
        this.ResumeLayout(false);
  this.PerformLayout();
    }

    private void ActualizarEstadoPartida()
    {
 try
        {
  var repoPartida = Configuracion.ObtenerServicio<IRepositorioPartidaJuego>();
   var partida = repoPartida.ObtenerPorId(_partidaId);
 
  if (partida != null)
       {
    lblNivel.Text = $"Nivel: {partida.NivelActual}";
   lblPuntaje.Text = $"Puntaje: {partida.PuntajeTotal}";
            }
        }
        catch (Exception ex)
  {
   MessageBox.Show($"Error al actualizar estado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CargarSiguientePregunta()
    {
        try
   {
     _preguntaActual = _servicioJuego.ObtenerSiguientePregunta(_partidaId);
  
     if (_preguntaActual == null)
   {
       var resultado = MessageBox.Show(
   "No hay más preguntas disponibles en este nivel. ¿Desea avanzar al siguiente nivel?",
"Nivel Completado",
   MessageBoxButtons.YesNo,
  MessageBoxIcon.Question
          );
  
if (resultado == DialogResult.Yes)
{
      _servicioJuego.AvanzarNivel(_partidaId);
      ActualizarEstadoPartida();
      CargarSiguientePregunta();
     }
  else
    {
          _servicioJuego.FinalizarPartida(_partidaId);
     MessageBox.Show("¡Felicitaciones! Has finalizado el juego.", "Juego Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
      this.Close();
   }
     return;
  }
      
     lblPregunta.Text = _preguntaActual.TextoPregunta;
   
    // Limpiar opciones anteriores
            foreach (var rb in rbOpciones)
 {
     rb.Checked = false;
   rb.Visible = false;
   rb.Enabled = true;
  }
     
      // Cargar nuevas opciones
   for (int i = 0; i < _preguntaActual.Opciones.Count && i < 4; i++)
   {
       rbOpciones[i].Text = _preguntaActual.Opciones[i].TextoOpcion;
    rbOpciones[i].Tag = _preguntaActual.Opciones[i];
                rbOpciones[i].Visible = true;
     }
       
      lblResultado.Visible = false;
  btnResponder.Visible = true;
        btnSiguiente.Visible = false;
        }
    catch (Exception ex)
   {
   MessageBox.Show($"Error al cargar pregunta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 }
    }

    private void BtnResponder_Click(object? sender, EventArgs e)
    {
   if (_preguntaActual == null)
        {
     return;
 }
   
  var opcionSeleccionada = rbOpciones.FirstOrDefault(rb => rb.Checked);
   
 if (opcionSeleccionada == null)
  {
      MessageBox.Show("Debe seleccionar una opción", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
 return;
        }

 try
        {
var opcion = (OpcionRespuesta)opcionSeleccionada.Tag!;
  var (esCorrecta, puntosObtenidos) = _servicioJuego.RegistrarRespuesta(_partidaId, _preguntaActual.Id, opcion.Id);
      
  ActualizarEstadoPartida();
 
    // Mostrar resultado
        if (esCorrecta)
  {
                lblResultado.Text = $"¡CORRECTO! +{puntosObtenidos} puntos";
      lblResultado.ForeColor = Color.Green;
}
            else
    {
                lblResultado.Text = puntosObtenidos < 0 
   ? $"INCORRECTO. {puntosObtenidos} puntos"
          : "INCORRECTO. 0 puntos";
        lblResultado.ForeColor = Color.Red;
            }
       
      lblResultado.Visible = true;
   
   // Deshabilitar opciones
        foreach (var rb in rbOpciones)
        {
         rb.Enabled = false;
     }
          
          btnResponder.Visible = false;
   btnSiguiente.Visible = true;
        }
      catch (Exception ex)
   {
      MessageBox.Show($"Error al registrar respuesta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    }

    private void BtnSiguiente_Click(object? sender, EventArgs e) => CargarSiguientePregunta();

    private void BtnFinalizarJuego_Click(object? sender, EventArgs e)
{
     var resultado = MessageBox.Show(
   "¿Está seguro de finalizar el juego?",
        "Confirmar",
        MessageBoxButtons.YesNo,
 MessageBoxIcon.Question
      );
        
 if (resultado == DialogResult.Yes)
 {
            try
            {
 _servicioJuego.FinalizarPartida(_partidaId);
   MessageBox.Show("Juego finalizado. ¡Gracias por jugar!", "Juego Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
     this.Close();
         }
   catch (Exception ex)
     {
   MessageBox.Show($"Error al finalizar juego: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
       }
 }
    }
}
