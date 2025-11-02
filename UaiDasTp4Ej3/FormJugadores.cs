using APP;
using ABS;
using DOM;

namespace UaiDasTp4Ej3;

public partial class FormJugadores : Form
{
    private readonly IRepositorioJugador _repositorio;
    private DataGridView dgvJugadores = null!;
    private TextBox txtNombre = null!;
    private TextBox txtApellido = null!;
    private TextBox txtEmail = null!;
    private Button btnAgregar = null!;
    private Button btnModificar = null!;
    private Button btnEliminar = null!;
    private Button btnLimpiar = null!;
    private Label lblNombre = null!;
    private Label lblApellido = null!;
    private Label lblEmail = null!;
    private int? _idSeleccionado;

    public FormJugadores()
 {
      _repositorio = Configuracion.ObtenerServicio<IRepositorioJugador>();
      InitializeComponent();
 CargarJugadores();
    }

    private void InitializeComponent()
    {
     this.dgvJugadores = new DataGridView();
        this.txtNombre = new TextBox();
  this.txtApellido = new TextBox();
        this.txtEmail = new TextBox();
        this.btnAgregar = new Button();
   this.btnModificar = new Button();
      this.btnEliminar = new Button();
 this.btnLimpiar = new Button();
        this.lblNombre = new Label();
 this.lblApellido = new Label();
        this.lblEmail = new Label();
((System.ComponentModel.ISupportInitialize)this.dgvJugadores).BeginInit();
        this.SuspendLayout();
        
        // dgvJugadores
        this.dgvJugadores.AllowUserToAddRows = false;
     this.dgvJugadores.AllowUserToDeleteRows = false;
      this.dgvJugadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
 this.dgvJugadores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvJugadores.Location = new Point(12, 12);
     this.dgvJugadores.MultiSelect = false;
        this.dgvJugadores.Name = "dgvJugadores";
   this.dgvJugadores.ReadOnly = true;
   this.dgvJugadores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvJugadores.Size = new Size(760, 300);
      this.dgvJugadores.TabIndex = 0;
  this.dgvJugadores.SelectionChanged += DgvJugadores_SelectionChanged;
     
  // lblNombre
      this.lblNombre.AutoSize = true;
      this.lblNombre.Location = new Point(12, 330);
     this.lblNombre.Name = "lblNombre";
        this.lblNombre.Size = new Size(54, 15);
  this.lblNombre.TabIndex = 1;
        this.lblNombre.Text = "Nombre:";
        
    // txtNombre
        this.txtNombre.Location = new Point(12, 348);
  this.txtNombre.MaxLength = 100;
        this.txtNombre.Name = "txtNombre";
   this.txtNombre.Size = new Size(300, 23);
   this.txtNombre.TabIndex = 2;
  
        // lblApellido
        this.lblApellido.AutoSize = true;
   this.lblApellido.Location = new Point(12, 385);
        this.lblApellido.Name = "lblApellido";
  this.lblApellido.Size = new Size(54, 15);
 this.lblApellido.TabIndex = 3;
 this.lblApellido.Text = "Apellido:";
        
 // txtApellido
  this.txtApellido.Location = new Point(12, 403);
 this.txtApellido.MaxLength = 100;
   this.txtApellido.Name = "txtApellido";
     this.txtApellido.Size = new Size(300, 23);
    this.txtApellido.TabIndex = 4;
     
   // lblEmail
        this.lblEmail.AutoSize = true;
this.lblEmail.Location = new Point(12, 440);
        this.lblEmail.Name = "lblEmail";
        this.lblEmail.Size = new Size(39, 15);
this.lblEmail.TabIndex = 5;
   this.lblEmail.Text = "Email:";
        
  // txtEmail
  this.txtEmail.Location = new Point(12, 458);
        this.txtEmail.MaxLength = 150;
 this.txtEmail.Name = "txtEmail";
        this.txtEmail.Size = new Size(300, 23);
   this.txtEmail.TabIndex = 6;
        
   // btnAgregar
        this.btnAgregar.Location = new Point(12, 500);
        this.btnAgregar.Name = "btnAgregar";
this.btnAgregar.Size = new Size(100, 30);
   this.btnAgregar.TabIndex = 7;
        this.btnAgregar.Text = "Agregar";
        this.btnAgregar.UseVisualStyleBackColor = true;
   this.btnAgregar.Click += BtnAgregar_Click;
        
        // btnModificar
    this.btnModificar.Location = new Point(118, 500);
     this.btnModificar.Name = "btnModificar";
        this.btnModificar.Size = new Size(100, 30);
  this.btnModificar.TabIndex = 8;
   this.btnModificar.Text = "Modificar";
     this.btnModificar.UseVisualStyleBackColor = true;
  this.btnModificar.Click += BtnModificar_Click;
        
    // btnEliminar
   this.btnEliminar.Location = new Point(224, 500);
   this.btnEliminar.Name = "btnEliminar";
   this.btnEliminar.Size = new Size(100, 30);
  this.btnEliminar.TabIndex = 9;
   this.btnEliminar.Text = "Eliminar";
        this.btnEliminar.UseVisualStyleBackColor = true;
        this.btnEliminar.Click += BtnEliminar_Click;
 
     // btnLimpiar
this.btnLimpiar.Location = new Point(330, 500);
  this.btnLimpiar.Name = "btnLimpiar";
        this.btnLimpiar.Size = new Size(100, 30);
      this.btnLimpiar.TabIndex = 10;
  this.btnLimpiar.Text = "Limpiar";
   this.btnLimpiar.UseVisualStyleBackColor = true;
        this.btnLimpiar.Click += BtnLimpiar_Click;
        
  // FormJugadores
      this.AutoScaleDimensions = new SizeF(7F, 15F);
     this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(784, 551);
        this.Controls.Add(this.btnLimpiar);
        this.Controls.Add(this.btnEliminar);
        this.Controls.Add(this.btnModificar);
     this.Controls.Add(this.btnAgregar);
        this.Controls.Add(this.txtEmail);
 this.Controls.Add(this.lblEmail);
this.Controls.Add(this.txtApellido);
   this.Controls.Add(this.lblApellido);
        this.Controls.Add(this.txtNombre);
  this.Controls.Add(this.lblNombre);
        this.Controls.Add(this.dgvJugadores);
        this.Name = "FormJugadores";
        this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Administración de Jugadores";
        ((System.ComponentModel.ISupportInitialize)this.dgvJugadores).EndInit();
 this.ResumeLayout(false);
   this.PerformLayout();
    }

    private void CargarJugadores()
    {
        try
      {
            var jugadores = _repositorio.ObtenerTodos();
   dgvJugadores.DataSource = jugadores;
        
            if (dgvJugadores.Columns.Count > 0)
 {
  dgvJugadores.Columns["Id"].Width = 50;
  }
        }
        catch (Exception ex)
        {
 MessageBox.Show($"Error al cargar jugadores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 }
 }

    private void BtnAgregar_Click(object? sender, EventArgs e)
  {
        if (!ValidarDatos())
        {
       return;
      }

   try
        {
var jugador = new Jugador(0, txtNombre.Text.Trim(), txtApellido.Text.Trim(), txtEmail.Text.Trim());
        _repositorio.Agregar(jugador);
    MessageBox.Show("Jugador agregado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
      LimpiarCampos();
    CargarJugadores();
        }
   catch (Exception ex)
   {
            MessageBox.Show($"Error al agregar jugador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnModificar_Click(object? sender, EventArgs e)
    {
        if (!_idSeleccionado.HasValue)
 {
  MessageBox.Show("Debe seleccionar un jugador para modificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
     }

    if (!ValidarDatos())
   {
  return;
        }

        try
   {
         var jugador = new Jugador(_idSeleccionado.Value, txtNombre.Text.Trim(), txtApellido.Text.Trim(), txtEmail.Text.Trim());
      _repositorio.Modificar(jugador);
     MessageBox.Show("Jugador modificado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarCampos();
     CargarJugadores();
        }
        catch (Exception ex)
  {
       MessageBox.Show($"Error al modificar jugador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnEliminar_Click(object? sender, EventArgs e)
 {
if (!_idSeleccionado.HasValue)
        {
            MessageBox.Show("Debe seleccionar un jugador para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    return;
      }

    var resultado = MessageBox.Show("¿Está seguro de eliminar este jugador?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        
 if (resultado == DialogResult.Yes)
        {
            try
     {
    _repositorio.Eliminar(_idSeleccionado.Value);
      MessageBox.Show("Jugador eliminado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
      LimpiarCampos();
      CargarJugadores();
  }
       catch (Exception ex)
    {
         MessageBox.Show($"Error al eliminar jugador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    }
    }

    private void BtnLimpiar_Click(object? sender, EventArgs e) => LimpiarCampos();

    private void DgvJugadores_SelectionChanged(object? sender, EventArgs e)
    {
   if (dgvJugadores.SelectedRows.Count > 0)
      {
     var fila = dgvJugadores.SelectedRows[0];
 _idSeleccionado = (int)fila.Cells["Id"].Value;
        txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
      txtApellido.Text = fila.Cells["Apellido"].Value.ToString();
     txtEmail.Text = fila.Cells["Email"].Value.ToString();
        }
    }

    private bool ValidarDatos()
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text))
        {
   MessageBox.Show("El nombre es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
   txtNombre.Focus();
         return false;
        }

   if (string.IsNullOrWhiteSpace(txtApellido.Text))
        {
 MessageBox.Show("El apellido es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
 txtApellido.Focus();
     return false;
 }

     if (string.IsNullOrWhiteSpace(txtEmail.Text))
     {
 MessageBox.Show("El email es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
       txtEmail.Focus();
   return false;
        }

        if (!txtEmail.Text.Contains("@"))
        {
    MessageBox.Show("El email no es válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtEmail.Focus();
            return false;
        }

    return true;
}

    private void LimpiarCampos()
    {
    _idSeleccionado = null;
     txtNombre.Clear();
     txtApellido.Clear();
   txtEmail.Clear();
        txtNombre.Focus();
        dgvJugadores.ClearSelection();
    }
}
