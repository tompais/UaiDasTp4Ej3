using ABS;
using DOM;

namespace UaiDasTp4Ej3;

public partial class FormOpcionesRespuesta : Form
{
    private readonly int _preguntaId;
    private readonly IRepositorioOpcionRespuesta _repositorio;
    private DataGridView dgvOpciones = null!;
    private TextBox txtTextoOpcion = null!;
    private CheckBox chkEsCorrecta = null!;
    private Button btnAgregar = null!;
    private Button btnModificar = null!;
    private Button btnEliminar = null!;
    private Button btnCerrar = null!;
    private Label lblTextoOpcion = null!;
    private Label lblInfo = null!;
    private int? _idSeleccionado;

    public FormOpcionesRespuesta(int preguntaId, IRepositorioOpcionRespuesta repositorio)
    {
        _preguntaId = preguntaId;
        _repositorio = repositorio;
        InitializeComponent();
        CargarOpciones();
    }

    private void InitializeComponent()
    {
   this.dgvOpciones = new DataGridView();
        this.txtTextoOpcion = new TextBox();
        this.chkEsCorrecta = new CheckBox();
        this.btnAgregar = new Button();
        this.btnModificar = new Button();
   this.btnEliminar = new Button();
        this.btnCerrar = new Button();
      this.lblTextoOpcion = new Label();
 this.lblInfo = new Label();
        ((System.ComponentModel.ISupportInitialize)this.dgvOpciones).BeginInit();
        this.SuspendLayout();
        
        // lblInfo
     this.lblInfo.AutoSize = true;
        this.lblInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.lblInfo.Location = new Point(12, 12);
  this.lblInfo.Name = "lblInfo";
        this.lblInfo.Size = new Size(450, 15);
        this.lblInfo.TabIndex = 0;
        this.lblInfo.Text = "Importante: Debe agregar al menos 2 opciones, y al menos una debe ser correcta.";
        
     // dgvOpciones
        this.dgvOpciones.AllowUserToAddRows = false;
        this.dgvOpciones.AllowUserToDeleteRows = false;
        this.dgvOpciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvOpciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvOpciones.Location = new Point(12, 40);
        this.dgvOpciones.MultiSelect = false;
        this.dgvOpciones.Name = "dgvOpciones";
     this.dgvOpciones.ReadOnly = true;
        this.dgvOpciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
   this.dgvOpciones.Size = new Size(760, 200);
     this.dgvOpciones.TabIndex = 1;
        this.dgvOpciones.SelectionChanged += DgvOpciones_SelectionChanged;

        // lblTextoOpcion
        this.lblTextoOpcion.AutoSize = true;
        this.lblTextoOpcion.Location = new Point(12, 260);
        this.lblTextoOpcion.Name = "lblTextoOpcion";
        this.lblTextoOpcion.Size = new Size(82, 15);
        this.lblTextoOpcion.TabIndex = 2;
        this.lblTextoOpcion.Text = "Texto Opción:";
        
        // txtTextoOpcion
        this.txtTextoOpcion.Location = new Point(12, 278);
        this.txtTextoOpcion.MaxLength = 300;
        this.txtTextoOpcion.Multiline = true;
this.txtTextoOpcion.Name = "txtTextoOpcion";
        this.txtTextoOpcion.Size = new Size(500, 50);
        this.txtTextoOpcion.TabIndex = 3;
        
        // chkEsCorrecta
      this.chkEsCorrecta.AutoSize = true;
        this.chkEsCorrecta.Location = new Point(530, 280);
        this.chkEsCorrecta.Name = "chkEsCorrecta";
        this.chkEsCorrecta.Size = new Size(87, 19);
   this.chkEsCorrecta.TabIndex = 4;
        this.chkEsCorrecta.Text = "Es Correcta";
   
        // btnAgregar
   this.btnAgregar.Location = new Point(12, 345);
      this.btnAgregar.Name = "btnAgregar";
        this.btnAgregar.Size = new Size(100, 30);
  this.btnAgregar.TabIndex = 5;
     this.btnAgregar.Text = "Agregar";
      this.btnAgregar.UseVisualStyleBackColor = true;
        this.btnAgregar.Click += BtnAgregar_Click;
        
  // btnModificar
   this.btnModificar.Location = new Point(118, 345);
        this.btnModificar.Name = "btnModificar";
        this.btnModificar.Size = new Size(100, 30);
    this.btnModificar.TabIndex = 6;
    this.btnModificar.Text = "Modificar";
this.btnModificar.UseVisualStyleBackColor = true;
        this.btnModificar.Click += BtnModificar_Click;
        
        // btnEliminar
        this.btnEliminar.Location = new Point(224, 345);
        this.btnEliminar.Name = "btnEliminar";
        this.btnEliminar.Size = new Size(100, 30);
        this.btnEliminar.TabIndex = 7;
        this.btnEliminar.Text = "Eliminar";
        this.btnEliminar.UseVisualStyleBackColor = true;
        this.btnEliminar.Click += BtnEliminar_Click;
        
     // btnCerrar
        this.btnCerrar.Location = new Point(672, 345);
        this.btnCerrar.Name = "btnCerrar";
      this.btnCerrar.Size = new Size(100, 30);
        this.btnCerrar.TabIndex = 8;
        this.btnCerrar.Text = "Cerrar";
        this.btnCerrar.UseVisualStyleBackColor = true;
        this.btnCerrar.Click += BtnCerrar_Click;
   
    // FormOpcionesRespuesta
   this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(784, 391);
        this.Controls.Add(this.btnCerrar);
      this.Controls.Add(this.btnEliminar);
        this.Controls.Add(this.btnModificar);
        this.Controls.Add(this.btnAgregar);
     this.Controls.Add(this.chkEsCorrecta);
 this.Controls.Add(this.txtTextoOpcion);
     this.Controls.Add(this.lblTextoOpcion);
        this.Controls.Add(this.dgvOpciones);
        this.Controls.Add(this.lblInfo);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
  this.Name = "FormOpcionesRespuesta";
      this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Opciones de Respuesta";
        ((System.ComponentModel.ISupportInitialize)this.dgvOpciones).EndInit();
    this.ResumeLayout(false);
  this.PerformLayout();
    }

  private void CargarOpciones()
    {
        try
        {
        var opciones = _repositorio.ObtenerPorPregunta(_preguntaId);
            dgvOpciones.DataSource = opciones;
        
            if (dgvOpciones.Columns.Count > 0)
            {
     dgvOpciones.Columns["Id"].Width = 50;
if (dgvOpciones.Columns.Contains("PreguntaId"))
                {
dgvOpciones.Columns["PreguntaId"].Visible = false;
       }
            }
        }
        catch (Exception ex)
     {
     MessageBox.Show($"Error al cargar opciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var opcion = new OpcionRespuesta(0, _preguntaId, txtTextoOpcion.Text.Trim(), chkEsCorrecta.Checked);
            _repositorio.Agregar(opcion);
      MessageBox.Show("Opción agregada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
  LimpiarCampos();
         CargarOpciones();
        }
        catch (Exception ex)
    {
            MessageBox.Show($"Error al agregar opción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
  }
    }

    private void BtnModificar_Click(object? sender, EventArgs e)
    {
        if (!_idSeleccionado.HasValue)
        {
    MessageBox.Show("Debe seleccionar una opción para modificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!ValidarDatos())
        {
     return;
        }

        try
        {
            var opcion = new OpcionRespuesta(_idSeleccionado.Value, _preguntaId, txtTextoOpcion.Text.Trim(), chkEsCorrecta.Checked);
            _repositorio.Modificar(opcion);
     MessageBox.Show("Opción modificada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarCampos();
  CargarOpciones();
        }
        catch (Exception ex)
     {
       MessageBox.Show($"Error al modificar opción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnEliminar_Click(object? sender, EventArgs e)
    {
    if (!_idSeleccionado.HasValue)
        {
            MessageBox.Show("Debe seleccionar una opción para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
        }

        var resultado = MessageBox.Show("¿Está seguro de eliminar esta opción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        
  if (resultado == DialogResult.Yes)
        {
      try
            {
  _repositorio.Eliminar(_idSeleccionado.Value);
      MessageBox.Show("Opción eliminada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
 LimpiarCampos();
       CargarOpciones();
 }
          catch (Exception ex)
            {
           MessageBox.Show($"Error al eliminar opción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
     }
    }

    private void BtnCerrar_Click(object? sender, EventArgs e) => this.Close();

    private void DgvOpciones_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvOpciones.SelectedRows.Count > 0)
 {
  var fila = dgvOpciones.SelectedRows[0];
            _idSeleccionado = (int)fila.Cells["Id"].Value;
        txtTextoOpcion.Text = fila.Cells["TextoOpcion"].Value.ToString();
      chkEsCorrecta.Checked = (bool)fila.Cells["EsCorrecta"].Value;
    }
    }

 private bool ValidarDatos()
    {
        if (string.IsNullOrWhiteSpace(txtTextoOpcion.Text))
        {
            MessageBox.Show("El texto de la opción es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTextoOpcion.Focus();
            return false;
        }

 return true;
    }

    private void LimpiarCampos()
    {
        _idSeleccionado = null;
        txtTextoOpcion.Clear();
        chkEsCorrecta.Checked = false;
        txtTextoOpcion.Focus();
        dgvOpciones.ClearSelection();
    }
}
