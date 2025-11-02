using APP;
using ABS;
using DOM;

namespace UaiDasTp4Ej3;

public partial class FormCategorias : Form
{
private readonly IRepositorioCategoria _repositorio;
    private DataGridView dgvCategorias = null!;
    private TextBox txtNombre = null!;
  private TextBox txtDescripcion = null!;
    private Button btnAgregar = null!;
    private Button btnModificar = null!;
    private Button btnEliminar = null!;
    private Button btnLimpiar = null!;
    private Label lblNombre = null!;
    private Label lblDescripcion = null!;
    private int? _idSeleccionado;

    public FormCategorias()
    {
        _repositorio = Configuracion.ObtenerServicio<IRepositorioCategoria>();
        InitializeComponent();
     CargarCategorias();
    }

    private void InitializeComponent()
    {
        this.dgvCategorias = new DataGridView();
        this.txtNombre = new TextBox();
        this.txtDescripcion = new TextBox();
this.btnAgregar = new Button();
        this.btnModificar = new Button();
   this.btnEliminar = new Button();
        this.btnLimpiar = new Button();
        this.lblNombre = new Label();
        this.lblDescripcion = new Label();
        ((System.ComponentModel.ISupportInitialize)this.dgvCategorias).BeginInit();
     this.SuspendLayout();
        
        // dgvCategorias
        this.dgvCategorias.AllowUserToAddRows = false;
        this.dgvCategorias.AllowUserToDeleteRows = false;
        this.dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
  this.dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvCategorias.Location = new Point(12, 12);
   this.dgvCategorias.MultiSelect = false;
    this.dgvCategorias.Name = "dgvCategorias";
        this.dgvCategorias.ReadOnly = true;
        this.dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dgvCategorias.Size = new Size(760, 300);
        this.dgvCategorias.TabIndex = 0;
        this.dgvCategorias.SelectionChanged += DgvCategorias_SelectionChanged;
        
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
        
        // lblDescripcion
        this.lblDescripcion.AutoSize = true;
        this.lblDescripcion.Location = new Point(12, 385);
        this.lblDescripcion.Name = "lblDescripcion";
        this.lblDescripcion.Size = new Size(72, 15);
        this.lblDescripcion.TabIndex = 3;
 this.lblDescripcion.Text = "Descripción:";
     
   // txtDescripcion
        this.txtDescripcion.Location = new Point(12, 403);
   this.txtDescripcion.MaxLength = 500;
        this.txtDescripcion.Multiline = true;
        this.txtDescripcion.Name = "txtDescripcion";
     this.txtDescripcion.Size = new Size(500, 60);
   this.txtDescripcion.TabIndex = 4;
        
 // btnAgregar
        this.btnAgregar.Location = new Point(12, 480);
        this.btnAgregar.Name = "btnAgregar";
        this.btnAgregar.Size = new Size(100, 30);
        this.btnAgregar.TabIndex = 5;
        this.btnAgregar.Text = "Agregar";
        this.btnAgregar.UseVisualStyleBackColor = true;
        this.btnAgregar.Click += BtnAgregar_Click;
  
        // btnModificar
        this.btnModificar.Location = new Point(118, 480);
        this.btnModificar.Name = "btnModificar";
        this.btnModificar.Size = new Size(100, 30);
        this.btnModificar.TabIndex = 6;
    this.btnModificar.Text = "Modificar";
        this.btnModificar.UseVisualStyleBackColor = true;
        this.btnModificar.Click += BtnModificar_Click;
        
        // btnEliminar
  this.btnEliminar.Location = new Point(224, 480);
        this.btnEliminar.Name = "btnEliminar";
        this.btnEliminar.Size = new Size(100, 30);
   this.btnEliminar.TabIndex = 7;
      this.btnEliminar.Text = "Eliminar";
        this.btnEliminar.UseVisualStyleBackColor = true;
        this.btnEliminar.Click += BtnEliminar_Click;
     
     // btnLimpiar
    this.btnLimpiar.Location = new Point(330, 480);
        this.btnLimpiar.Name = "btnLimpiar";
      this.btnLimpiar.Size = new Size(100, 30);
     this.btnLimpiar.TabIndex = 8;
   this.btnLimpiar.Text = "Limpiar";
   this.btnLimpiar.UseVisualStyleBackColor = true;
        this.btnLimpiar.Click += BtnLimpiar_Click;
  
  // FormCategorias
        this.AutoScaleDimensions = new SizeF(7F, 15F);
      this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(784, 531);
        this.Controls.Add(this.btnLimpiar);
 this.Controls.Add(this.btnEliminar);
        this.Controls.Add(this.btnModificar);
    this.Controls.Add(this.btnAgregar);
        this.Controls.Add(this.txtDescripcion);
      this.Controls.Add(this.lblDescripcion);
        this.Controls.Add(this.txtNombre);
        this.Controls.Add(this.lblNombre);
  this.Controls.Add(this.dgvCategorias);
        this.Name = "FormCategorias";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Administración de Categorías";
 ((System.ComponentModel.ISupportInitialize)this.dgvCategorias).EndInit();
        this.ResumeLayout(false);
    this.PerformLayout();
}

    private void CargarCategorias()
    {
        try
        {
   var categorias = _repositorio.ObtenerTodos();
      dgvCategorias.DataSource = categorias;
        
  if (dgvCategorias.Columns.Count > 0)
     {
         dgvCategorias.Columns["Id"].Width = 50;
      }
     }
        catch (Exception ex)
        {
    MessageBox.Show($"Error al cargar categorías: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
         var categoria = new Categoria(0, txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
          _repositorio.Agregar(categoria);
            MessageBox.Show("Categoría agregada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarCampos();
            CargarCategorias();
    }
   catch (Exception ex)
        {
     MessageBox.Show($"Error al agregar categoría: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
 }

 private void BtnModificar_Click(object? sender, EventArgs e)
 {
      if (!_idSeleccionado.HasValue)
        {
      MessageBox.Show("Debe seleccionar una categoría para modificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
}

        if (!ValidarDatos())
        {
   return;
        }

        try
{
          var categoria = new Categoria(_idSeleccionado.Value, txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
 _repositorio.Modificar(categoria);
 MessageBox.Show("Categoría modificada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
   LimpiarCampos();
            CargarCategorias();
        }
        catch (Exception ex)
   {
  MessageBox.Show($"Error al modificar categoría: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnEliminar_Click(object? sender, EventArgs e)
    {
  if (!_idSeleccionado.HasValue)
   {
            MessageBox.Show("Debe seleccionar una categoría para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
         return;
   }

        var resultado = MessageBox.Show("¿Está seguro de eliminar esta categoría?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
   
    if (resultado == DialogResult.Yes)
  {
       try
    {
 _repositorio.Eliminar(_idSeleccionado.Value);
       MessageBox.Show("Categoría eliminada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
   LimpiarCampos();
      CargarCategorias();
      }
catch (Exception ex)
     {
         MessageBox.Show($"Error al eliminar categoría: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
       }
        }
    }

    private void BtnLimpiar_Click(object? sender, EventArgs e) => LimpiarCampos();

    private void DgvCategorias_SelectionChanged(object? sender, EventArgs e)
    {
   if (dgvCategorias.SelectedRows.Count > 0)
        {
   var fila = dgvCategorias.SelectedRows[0];
    _idSeleccionado = (int)fila.Cells["Id"].Value;
  txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
 txtDescripcion.Text = fila.Cells["Descripcion"].Value.ToString();
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

   if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
   {
  MessageBox.Show("La descripción es obligatoria", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
       txtDescripcion.Focus();
return false;
        }

    return true;
    }

    private void LimpiarCampos()
 {
   _idSeleccionado = null;
 txtNombre.Clear();
      txtDescripcion.Clear();
   txtNombre.Focus();
        dgvCategorias.ClearSelection();
    }
}
