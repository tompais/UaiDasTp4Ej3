using APP;
using ABS;
using DOM;

namespace UaiDasTp4Ej3;

public partial class FormPreguntas : Form
{
    private readonly IRepositorioPregunta _repositorioPregunta;
    private readonly IRepositorioCategoria _repositorioCategoria;
    private readonly IRepositorioOpcionRespuesta _repositorioOpcion;
    private DataGridView dgvPreguntas = null!;
    private TextBox txtTextoPregunta = null!;
    private ComboBox cboCategoria = null!;
    private NumericUpDown nudNivel = null!;
    private NumericUpDown nudPuntosAcierto = null!;
    private NumericUpDown nudPuntosError = null!;
    private CheckBox chkTienePenalizacion = null!;
    private Button btnAgregar = null!;
    private Button btnModificar = null!;
    private Button btnEliminar = null!;
  private Button btnLimpiar = null!;
    private Button btnGestionarOpciones = null!;
    private Label lblTextoPregunta = null!;
    private Label lblCategoria = null!;
    private Label lblNivel = null!;
 private Label lblPuntosAcierto = null!;
  private Label lblPuntosError = null!;
    private int? _idSeleccionado;

    public FormPreguntas()
    {
        _repositorioPregunta = Configuracion.ObtenerServicio<IRepositorioPregunta>();
        _repositorioCategoria = Configuracion.ObtenerServicio<IRepositorioCategoria>();
      _repositorioOpcion = Configuracion.ObtenerServicio<IRepositorioOpcionRespuesta>();
        InitializeComponent();
        CargarCategorias();
        CargarPreguntas();
    }

    private void InitializeComponent()
{
        this.dgvPreguntas = new DataGridView();
        this.txtTextoPregunta = new TextBox();
  this.cboCategoria = new ComboBox();
        this.nudNivel = new NumericUpDown();
        this.nudPuntosAcierto = new NumericUpDown();
     this.nudPuntosError = new NumericUpDown();
        this.chkTienePenalizacion = new CheckBox();
   this.btnAgregar = new Button();
     this.btnModificar = new Button();
this.btnEliminar = new Button();
this.btnLimpiar = new Button();
     this.btnGestionarOpciones = new Button();
   this.lblTextoPregunta = new Label();
   this.lblCategoria = new Label();
        this.lblNivel = new Label();
    this.lblPuntosAcierto = new Label();
     this.lblPuntosError = new Label();
        ((System.ComponentModel.ISupportInitialize)this.dgvPreguntas).BeginInit();
  ((System.ComponentModel.ISupportInitialize)this.nudNivel).BeginInit();
      ((System.ComponentModel.ISupportInitialize)this.nudPuntosAcierto).BeginInit();
      ((System.ComponentModel.ISupportInitialize)this.nudPuntosError).BeginInit();
this.SuspendLayout();
 
        // dgvPreguntas
  this.dgvPreguntas.AllowUserToAddRows = false;
 this.dgvPreguntas.AllowUserToDeleteRows = false;
        this.dgvPreguntas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
     this.dgvPreguntas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
     this.dgvPreguntas.Location = new Point(12, 12);
      this.dgvPreguntas.MultiSelect = false;
   this.dgvPreguntas.Name = "dgvPreguntas";
        this.dgvPreguntas.ReadOnly = true;
        this.dgvPreguntas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dgvPreguntas.Size = new Size(960, 300);
   this.dgvPreguntas.TabIndex = 0;
     this.dgvPreguntas.SelectionChanged += DgvPreguntas_SelectionChanged;
  
  // lblTextoPregunta
        this.lblTextoPregunta.AutoSize = true;
   this.lblTextoPregunta.Location = new Point(12, 330);
        this.lblTextoPregunta.Name = "lblTextoPregunta";
        this.lblTextoPregunta.Size = new Size(58, 15);
   this.lblTextoPregunta.TabIndex = 1;
        this.lblTextoPregunta.Text = "Pregunta:";
    
  // txtTextoPregunta
     this.txtTextoPregunta.Location = new Point(12, 348);
     this.txtTextoPregunta.MaxLength = 500;
        this.txtTextoPregunta.Multiline = true;
        this.txtTextoPregunta.Name = "txtTextoPregunta";
   this.txtTextoPregunta.Size = new Size(600, 60);
   this.txtTextoPregunta.TabIndex = 2;
        
 // lblCategoria
     this.lblCategoria.AutoSize = true;
        this.lblCategoria.Location = new Point(12, 425);
   this.lblCategoria.Name = "lblCategoria";
   this.lblCategoria.Size = new Size(61, 15);
        this.lblCategoria.TabIndex = 3;
   this.lblCategoria.Text = "Categoría:";
        
        // cboCategoria
        this.cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cboCategoria.FormattingEnabled = true;
   this.cboCategoria.Location = new Point(12, 443);
        this.cboCategoria.Name = "cboCategoria";
this.cboCategoria.Size = new Size(300, 23);
  this.cboCategoria.TabIndex = 4;
        
        // lblNivel
this.lblNivel.AutoSize = true;
        this.lblNivel.Location = new Point(330, 425);
     this.lblNivel.Name = "lblNivel";
        this.lblNivel.Size = new Size(37, 15);
   this.lblNivel.TabIndex = 5;
        this.lblNivel.Text = "Nivel:";
  
 // nudNivel
     this.nudNivel.Location = new Point(330, 443);
     this.nudNivel.Minimum = 1;
        this.nudNivel.Name = "nudNivel";
   this.nudNivel.Size = new Size(80, 23);
   this.nudNivel.TabIndex = 6;
   this.nudNivel.Value = 1;
  this.nudNivel.ValueChanged += NudNivel_ValueChanged;
     
     // lblPuntosAcierto
        this.lblPuntosAcierto.AutoSize = true;
     this.lblPuntosAcierto.Location = new Point(12, 480);
        this.lblPuntosAcierto.Name = "lblPuntosAcierto";
   this.lblPuntosAcierto.Size = new Size(96, 15);
this.lblPuntosAcierto.TabIndex = 7;
  this.lblPuntosAcierto.Text = "Puntos Acierto:";
        
   // nudPuntosAcierto
        this.nudPuntosAcierto.Location = new Point(12, 498);
  this.nudPuntosAcierto.Maximum = 1000;
 this.nudPuntosAcierto.Name = "nudPuntosAcierto";
this.nudPuntosAcierto.Size = new Size(120, 23);
    this.nudPuntosAcierto.TabIndex = 8;
   this.nudPuntosAcierto.Value = 10;
        
     // chkTienePenalizacion
        this.chkTienePenalizacion.AutoSize = true;
        this.chkTienePenalizacion.Location = new Point(150, 500);
   this.chkTienePenalizacion.Name = "chkTienePenalizacion";
  this.chkTienePenalizacion.Size = new Size(180, 19);
 this.chkTienePenalizacion.TabIndex = 9;
     this.chkTienePenalizacion.Text = "Tiene penalización (Nivel 4+)";
        this.chkTienePenalizacion.CheckedChanged += ChkTienePenalizacion_CheckedChanged;
  
   // lblPuntosError
        this.lblPuntosError.AutoSize = true;
        this.lblPuntosError.Location = new Point(350, 480);
 this.lblPuntosError.Name = "lblPuntosError";
     this.lblPuntosError.Size = new Size(78, 15);
   this.lblPuntosError.TabIndex = 10;
        this.lblPuntosError.Text = "Puntos Error:";
this.lblPuntosError.Visible = false;
 
   // nudPuntosError
this.nudPuntosError.Location = new Point(350, 498);
  this.nudPuntosError.Maximum = 0;
      this.nudPuntosError.Minimum = -1000;
        this.nudPuntosError.Name = "nudPuntosError";
        this.nudPuntosError.Size = new Size(120, 23);
  this.nudPuntosError.TabIndex = 11;
        this.nudPuntosError.Visible = false;
  
        // btnAgregar
     this.btnAgregar.Location = new Point(12, 540);
     this.btnAgregar.Name = "btnAgregar";
   this.btnAgregar.Size = new Size(100, 30);
  this.btnAgregar.TabIndex = 12;
        this.btnAgregar.Text = "Agregar";
        this.btnAgregar.UseVisualStyleBackColor = true;
     this.btnAgregar.Click += BtnAgregar_Click;
        
        // btnModificar
   this.btnModificar.Location = new Point(118, 540);
   this.btnModificar.Name = "btnModificar";
   this.btnModificar.Size = new Size(100, 30);
        this.btnModificar.TabIndex = 13;
   this.btnModificar.Text = "Modificar";
    this.btnModificar.UseVisualStyleBackColor = true;
        this.btnModificar.Click += BtnModificar_Click;
  
        // btnEliminar
     this.btnEliminar.Location = new Point(224, 540);
   this.btnEliminar.Name = "btnEliminar";
   this.btnEliminar.Size = new Size(100, 30);
 this.btnEliminar.TabIndex = 14;
   this.btnEliminar.Text = "Eliminar";
        this.btnEliminar.UseVisualStyleBackColor = true;
   this.btnEliminar.Click += BtnEliminar_Click;
  
    // btnLimpiar
this.btnLimpiar.Location = new Point(330, 540);
        this.btnLimpiar.Name = "btnLimpiar";
this.btnLimpiar.Size = new Size(100, 30);
 this.btnLimpiar.TabIndex = 15;
        this.btnLimpiar.Text = "Limpiar";
   this.btnLimpiar.UseVisualStyleBackColor = true;
   this.btnLimpiar.Click += BtnLimpiar_Click;
     
     // btnGestionarOpciones
  this.btnGestionarOpciones.Location = new Point(450, 540);
this.btnGestionarOpciones.Name = "btnGestionarOpciones";
        this.btnGestionarOpciones.Size = new Size(150, 30);
this.btnGestionarOpciones.TabIndex = 16;
        this.btnGestionarOpciones.Text = "Gestionar Opciones";
 this.btnGestionarOpciones.UseVisualStyleBackColor = true;
        this.btnGestionarOpciones.Click += BtnGestionarOpciones_Click;
  
        // FormPreguntas
 this.AutoScaleDimensions = new SizeF(7F, 15F);
this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(984, 591);
        this.Controls.Add(this.btnGestionarOpciones);
      this.Controls.Add(this.btnLimpiar);
        this.Controls.Add(this.btnEliminar);
this.Controls.Add(this.btnModificar);
     this.Controls.Add(this.btnAgregar);
        this.Controls.Add(this.nudPuntosError);
   this.Controls.Add(this.lblPuntosError);
 this.Controls.Add(this.chkTienePenalizacion);
        this.Controls.Add(this.nudPuntosAcierto);
this.Controls.Add(this.lblPuntosAcierto);
   this.Controls.Add(this.nudNivel);
        this.Controls.Add(this.lblNivel);
    this.Controls.Add(this.cboCategoria);
     this.Controls.Add(this.lblCategoria);
        this.Controls.Add(this.txtTextoPregunta);
   this.Controls.Add(this.lblTextoPregunta);
     this.Controls.Add(this.dgvPreguntas);
   this.Name = "FormPreguntas";
  this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Administración de Preguntas";
 ((System.ComponentModel.ISupportInitialize)this.dgvPreguntas).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.nudNivel).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.nudPuntosAcierto).EndInit();
      ((System.ComponentModel.ISupportInitialize)this.nudPuntosError).EndInit();
        this.ResumeLayout(false);
this.PerformLayout();
    }

    private void CargarCategorias()
 {
   try
   {
     var categorias = _repositorioCategoria.ObtenerTodos();
   cboCategoria.DataSource = categorias;
   cboCategoria.DisplayMember = "Nombre";
      cboCategoria.ValueMember = "Id";
   }
        catch (Exception ex)
   {
      MessageBox.Show($"Error al cargar categorías: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CargarPreguntas()
    {
    try
   {
   var preguntas = _repositorioPregunta.ObtenerTodos();
 dgvPreguntas.DataSource = preguntas;
        
       if (dgvPreguntas.Columns.Count > 0)
        {
            dgvPreguntas.Columns["Id"].Width = 50;
     dgvPreguntas.Columns["Nivel"].Width = 60;
     dgvPreguntas.Columns["PuntosAcierto"].Width = 100;
       dgvPreguntas.Columns["PuntosError"].Width = 100;
          if (dgvPreguntas.Columns.Contains("Categoria"))
   {
            dgvPreguntas.Columns["Categoria"].Visible = false;
        }
         if (dgvPreguntas.Columns.Contains("Opciones"))
     {
  dgvPreguntas.Columns["Opciones"].Visible = false;
     }
            }
   }
   catch (Exception ex)
    {
   MessageBox.Show($"Error al cargar preguntas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
      var puntosError = chkTienePenalizacion.Checked ? (int)nudPuntosError.Value : (int?)null;
      var pregunta = new Pregunta(
                0,
      txtTextoPregunta.Text.Trim(),
            (int)cboCategoria.SelectedValue!,
        (int)nudNivel.Value,
   (int)nudPuntosAcierto.Value,
     puntosError
 );
  
      _repositorioPregunta.Agregar(pregunta);
     MessageBox.Show("Pregunta agregada exitosamente. No olvide agregar las opciones de respuesta.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
 LimpiarCampos();
   CargarPreguntas();
        }
      catch (Exception ex)
     {
   MessageBox.Show($"Error al agregar pregunta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnModificar_Click(object? sender, EventArgs e)
 {
 if (!_idSeleccionado.HasValue)
  {
     MessageBox.Show("Debe seleccionar una pregunta para modificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

      if (!ValidarDatos())
     {
    return;
        }

  try
    {
       var puntosError = chkTienePenalizacion.Checked ? (int)nudPuntosError.Value : (int?)null;
   var pregunta = new Pregunta(
          _idSeleccionado.Value,
    txtTextoPregunta.Text.Trim(),
    (int)cboCategoria.SelectedValue!,
(int)nudNivel.Value,
   (int)nudPuntosAcierto.Value,
    puntosError
  );
            
     _repositorioPregunta.Modificar(pregunta);
     MessageBox.Show("Pregunta modificada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
    LimpiarCampos();
            CargarPreguntas();
        }
  catch (Exception ex)
   {
 MessageBox.Show($"Error al modificar pregunta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnEliminar_Click(object? sender, EventArgs e)
{
        if (!_idSeleccionado.HasValue)
        {
 MessageBox.Show("Debe seleccionar una pregunta para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
     return;
    }

   var resultado = MessageBox.Show("¿Está seguro de eliminar esta pregunta? Se eliminarán también todas sus opciones.", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        
   if (resultado == DialogResult.Yes)
   {
            try
          {
    _repositorioPregunta.Eliminar(_idSeleccionado.Value);
    MessageBox.Show("Pregunta eliminada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
 LimpiarCampos();
    CargarPreguntas();
     }
            catch (Exception ex)
       {
        MessageBox.Show($"Error al eliminar pregunta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
        }
    }

    private void BtnLimpiar_Click(object? sender, EventArgs e) => LimpiarCampos();

    private void BtnGestionarOpciones_Click(object? sender, EventArgs e)
    {
        if (!_idSeleccionado.HasValue)
   {
      MessageBox.Show("Debe seleccionar una pregunta para gestionar sus opciones", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
  }

   var formOpciones = new FormOpcionesRespuesta(_idSeleccionado.Value, _repositorioOpcion);
        formOpciones.ShowDialog();
    }

    private void DgvPreguntas_SelectionChanged(object? sender, EventArgs e)
    {
     if (dgvPreguntas.SelectedRows.Count > 0)
        {
var fila = dgvPreguntas.SelectedRows[0];
     _idSeleccionado = (int)fila.Cells["Id"].Value;
   txtTextoPregunta.Text = fila.Cells["TextoPregunta"].Value.ToString();
cboCategoria.SelectedValue = (int)fila.Cells["CategoriaId"].Value;
     nudNivel.Value = (int)fila.Cells["Nivel"].Value;
     nudPuntosAcierto.Value = (int)fila.Cells["PuntosAcierto"].Value;
  
      var puntosError = fila.Cells["PuntosError"].Value;
          if (puntosError != null && puntosError != DBNull.Value)
            {
    chkTienePenalizacion.Checked = true;
    nudPuntosError.Value = (int)puntosError;
      }
            else
            {
     chkTienePenalizacion.Checked = false;
        }
        }
    }

  private void NudNivel_ValueChanged(object? sender, EventArgs e)
    {
        // Si el nivel es 4 o mayor, sugerir activar la penalización
        if (nudNivel.Value >= 4)
     {
    chkTienePenalizacion.Enabled = true;
    }
 }

    private void ChkTienePenalizacion_CheckedChanged(object? sender, EventArgs e)
    {
      lblPuntosError.Visible = chkTienePenalizacion.Checked;
  nudPuntosError.Visible = chkTienePenalizacion.Checked;
   
   if (chkTienePenalizacion.Checked)
      {
          // Calcular automáticamente: mitad de puntos de acierto, pero negativo
      nudPuntosError.Value = -(int)nudPuntosAcierto.Value / 2;
        }
    }

    private bool ValidarDatos()
    {
   if (string.IsNullOrWhiteSpace(txtTextoPregunta.Text))
        {
   MessageBox.Show("El texto de la pregunta es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTextoPregunta.Focus();
            return false;
}

        if (cboCategoria.SelectedValue == null)
   {
      MessageBox.Show("Debe seleccionar una categoría", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
 cboCategoria.Focus();
        return false;
  }

   if (nudPuntosAcierto.Value <= 0)
        {
     MessageBox.Show("Los puntos de acierto deben ser mayores a 0", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
 nudPuntosAcierto.Focus();
            return false;
  }

        return true;
    }

private void LimpiarCampos()
 {
    _idSeleccionado = null;
      txtTextoPregunta.Clear();
        if (cboCategoria.Items.Count > 0)
 {
     cboCategoria.SelectedIndex = 0;
     }
 nudNivel.Value = 1;
        nudPuntosAcierto.Value = 10;
   nudPuntosError.Value = 0;
        chkTienePenalizacion.Checked = false;
        txtTextoPregunta.Focus();
        dgvPreguntas.ClearSelection();
    }
}
