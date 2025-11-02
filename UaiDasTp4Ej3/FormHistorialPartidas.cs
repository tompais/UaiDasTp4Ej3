using APP;
using ABS;

namespace UaiDasTp4Ej3;

public partial class FormHistorialPartidas : Form
{
    private readonly IRepositorioPartidaJuego _repositorioPartida;
    private readonly IRepositorioJugador _repositorioJugador;
    private DataGridView dgvPartidas = null!;
    private ComboBox cboFiltroJugador = null!;
    private ComboBox cboFiltroEstado = null!;
    private Button btnFiltrar = null!;
    private Button btnLimpiarFiltros = null!;
    private Label lblFiltroJugador = null!;
    private Label lblFiltroEstado = null!;

    public FormHistorialPartidas()
    {
        _repositorioPartida = Configuracion.ObtenerServicio<IRepositorioPartidaJuego>();
        _repositorioJugador = Configuracion.ObtenerServicio<IRepositorioJugador>();
        InitializeComponent();
        CargarJugadores();
        CargarPartidas();
    }

    private void InitializeComponent()
    {
        this.dgvPartidas = new DataGridView();
        this.cboFiltroJugador = new ComboBox();
        this.cboFiltroEstado = new ComboBox();
        this.btnFiltrar = new Button();
        this.btnLimpiarFiltros = new Button();
        this.lblFiltroJugador = new Label();
        this.lblFiltroEstado = new Label();
        ((System.ComponentModel.ISupportInitialize)this.dgvPartidas).BeginInit();
        this.SuspendLayout();

        // lblFiltroJugador
        this.lblFiltroJugador.AutoSize = true;
        this.lblFiltroJugador.Location = new Point(12, 15);
        this.lblFiltroJugador.Name = "lblFiltroJugador";
        this.lblFiltroJugador.Size = new Size(52, 15);
        this.lblFiltroJugador.TabIndex = 0;
        this.lblFiltroJugador.Text = "Jugador:";

        // cboFiltroJugador
        this.cboFiltroJugador.DropDownStyle = ComboBoxStyle.DropDownList;
        this.cboFiltroJugador.FormattingEnabled = true;
        this.cboFiltroJugador.Location = new Point(70, 12);
        this.cboFiltroJugador.Name = "cboFiltroJugador";
        this.cboFiltroJugador.Size = new Size(250, 23);
        this.cboFiltroJugador.TabIndex = 1;

        // lblFiltroEstado
        this.lblFiltroEstado.AutoSize = true;
        this.lblFiltroEstado.Location = new Point(340, 15);
        this.lblFiltroEstado.Name = "lblFiltroEstado";
        this.lblFiltroEstado.Size = new Size(45, 15);
        this.lblFiltroEstado.TabIndex = 2;
        this.lblFiltroEstado.Text = "Estado:";

        // cboFiltroEstado
        this.cboFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        this.cboFiltroEstado.FormattingEnabled = true;
        this.cboFiltroEstado.Items.AddRange(new object[] { "Todos", "EnCurso", "Finalizada", "Abandonada" });
        this.cboFiltroEstado.Location = new Point(391, 12);
        this.cboFiltroEstado.Name = "cboFiltroEstado";
        this.cboFiltroEstado.Size = new Size(150, 23);
        this.cboFiltroEstado.TabIndex = 3;
        this.cboFiltroEstado.SelectedIndex = 0;

        // btnFiltrar
        this.btnFiltrar.Location = new Point(560, 11);
        this.btnFiltrar.Name = "btnFiltrar";
        this.btnFiltrar.Size = new Size(90, 25);
        this.btnFiltrar.TabIndex = 4;
        this.btnFiltrar.Text = "Filtrar";
        this.btnFiltrar.UseVisualStyleBackColor = true;
        this.btnFiltrar.Click += BtnFiltrar_Click;

        // btnLimpiarFiltros
        this.btnLimpiarFiltros.Location = new Point(656, 11);
        this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
        this.btnLimpiarFiltros.Size = new Size(110, 25);
        this.btnLimpiarFiltros.TabIndex = 5;
        this.btnLimpiarFiltros.Text = "Limpiar Filtros";
        this.btnLimpiarFiltros.UseVisualStyleBackColor = true;
        this.btnLimpiarFiltros.Click += BtnLimpiarFiltros_Click;

        // dgvPartidas
        this.dgvPartidas.AllowUserToAddRows = false;
        this.dgvPartidas.AllowUserToDeleteRows = false;
        this.dgvPartidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvPartidas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvPartidas.Location = new Point(12, 50);
        this.dgvPartidas.MultiSelect = false;
        this.dgvPartidas.Name = "dgvPartidas";
        this.dgvPartidas.ReadOnly = true;
        this.dgvPartidas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dgvPartidas.Size = new Size(960, 480);
        this.dgvPartidas.TabIndex = 6;

        // FormHistorialPartidas
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(984, 541);
        this.Controls.Add(this.dgvPartidas);
        this.Controls.Add(this.btnLimpiarFiltros);
        this.Controls.Add(this.btnFiltrar);
        this.Controls.Add(this.cboFiltroEstado);
        this.Controls.Add(this.lblFiltroEstado);
        this.Controls.Add(this.cboFiltroJugador);
        this.Controls.Add(this.lblFiltroJugador);
        this.Name = "FormHistorialPartidas";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Historial de Partidas";
        ((System.ComponentModel.ISupportInitialize)this.dgvPartidas).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private void CargarJugadores()
    {
        try
        {
            var jugadores = _repositorioJugador.ObtenerTodos();
            var listaConTodos = new List<dynamic> { new { Id = 0, Nombre = "Todos" } };
            listaConTodos.AddRange(jugadores.Select(j => new { j.Id, Nombre = $"{j.Nombre} {j.Apellido}" }));

            cboFiltroJugador.DataSource = listaConTodos;
            cboFiltroJugador.DisplayMember = "Nombre";
            cboFiltroJugador.ValueMember = "Id";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar jugadores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CargarPartidas()
    {
        try
        {
            var partidas = _repositorioPartida.ObtenerTodos();

            // Enriquecer con información del jugador
            foreach (var partida in partidas)
            {
                var jugador = _repositorioJugador.ObtenerPorId(partida.JugadorId);
                if (jugador != null)
                {
                    partida.Jugador = jugador;
                }
            }

            dgvPartidas.DataSource = partidas;

            if (dgvPartidas.Columns.Count > 0)
            {
                dgvPartidas.Columns["Id"].Width = 50;
                dgvPartidas.Columns["JugadorId"].Visible = false;
                dgvPartidas.Columns["FechaInicio"].HeaderText = "Fecha Inicio";
                dgvPartidas.Columns["FechaFin"].HeaderText = "Fecha Fin";
                dgvPartidas.Columns["NivelActual"].HeaderText = "Nivel";
                dgvPartidas.Columns["PuntajeTotal"].HeaderText = "Puntaje";
                dgvPartidas.Columns["Estado"].Width = 100;

                if (dgvPartidas.Columns.Contains("Jugador"))
                {
                    dgvPartidas.Columns["Jugador"].Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar partidas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnFiltrar_Click(object? sender, EventArgs e)
    {
        try
        {
            var jugadorId = (int)(cboFiltroJugador.SelectedValue ?? 0);
            var estado = cboFiltroEstado.SelectedItem?.ToString();

            var partidas = jugadorId > 0
              ? _repositorioPartida.ObtenerPorJugador(jugadorId)
              : _repositorioPartida.ObtenerTodos();

            if (estado != "Todos" && !string.IsNullOrEmpty(estado))
            {
                partidas = partidas.Where(p => p.Estado == estado).ToList();
            }

            // Enriquecer con información del jugador
            foreach (var partida in partidas)
            {
                var jugador = _repositorioJugador.ObtenerPorId(partida.JugadorId);
                if (jugador != null)
                {
                    partida.Jugador = jugador;
                }
            }

            dgvPartidas.DataSource = partidas;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al filtrar partidas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnLimpiarFiltros_Click(object? sender, EventArgs e)
    {
        cboFiltroJugador.SelectedIndex = 0;
        cboFiltroEstado.SelectedIndex = 0;
        CargarPartidas();
    }
}
