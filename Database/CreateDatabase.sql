-- Script idempotente para crear la base de datos de Trivia
-- Este script puede ejecutarse múltiples veces sin problemas

USE master;
GO

-- Eliminar la base de datos si existe
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'TriviaDB')
BEGIN
    ALTER DATABASE TriviaDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE TriviaDB;
END
GO

-- Crear la base de datos
CREATE DATABASE TriviaDB;
GO

USE TriviaDB;
GO

-- ===================================================================
-- CREAR TABLAS
-- ===================================================================

-- Tabla Categorias
IF OBJECT_ID('dbo.Categorias', 'U') IS NOT NULL
    DROP TABLE dbo.Categorias;
GO

CREATE TABLE dbo.Categorias
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NOT NULL,
    CONSTRAINT UQ_Categorias_Nombre UNIQUE (Nombre)
);
GO

-- Tabla Preguntas
IF OBJECT_ID('dbo.Preguntas', 'U') IS NOT NULL
    DROP TABLE dbo.Preguntas;
GO

CREATE TABLE dbo.Preguntas
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TextoPregunta NVARCHAR(500) NOT NULL,
    CategoriaId INT NOT NULL,
    Nivel INT NOT NULL CHECK (Nivel >= 1),
    PuntosAcierto INT NOT NULL CHECK (PuntosAcierto >= 0),
    PuntosError INT NULL CHECK (PuntosError <= 0),
    CONSTRAINT FK_Preguntas_Categorias FOREIGN KEY (CategoriaId) REFERENCES dbo.Categorias(Id)
);
GO

CREATE INDEX IX_Preguntas_CategoriaId ON dbo.Preguntas(CategoriaId);
CREATE INDEX IX_Preguntas_Nivel ON dbo.Preguntas(Nivel);
GO

-- Tabla OpcionesRespuesta
IF OBJECT_ID('dbo.OpcionesRespuesta', 'U') IS NOT NULL
    DROP TABLE dbo.OpcionesRespuesta;
GO

CREATE TABLE dbo.OpcionesRespuesta
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PreguntaId INT NOT NULL,
    TextoOpcion NVARCHAR(300) NOT NULL,
    EsCorrecta BIT NOT NULL,
  CONSTRAINT FK_OpcionesRespuesta_Preguntas FOREIGN KEY (PreguntaId) REFERENCES dbo.Preguntas(Id) ON DELETE CASCADE
);
GO

CREATE INDEX IX_OpcionesRespuesta_PreguntaId ON dbo.OpcionesRespuesta(PreguntaId);
GO

-- Tabla Jugadores
IF OBJECT_ID('dbo.Jugadores', 'U') IS NOT NULL
    DROP TABLE dbo.Jugadores;
GO

CREATE TABLE dbo.Jugadores
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
 Apellido NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    CONSTRAINT UQ_Jugadores_Email UNIQUE (Email)
);
GO

-- Tabla PartidasJuego
IF OBJECT_ID('dbo.PartidasJuego', 'U') IS NOT NULL
    DROP TABLE dbo.PartidasJuego;
GO

CREATE TABLE dbo.PartidasJuego
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    JugadorId INT NOT NULL,
    FechaInicio DATETIME NOT NULL DEFAULT GETDATE(),
    FechaFin DATETIME NULL,
    NivelActual INT NOT NULL DEFAULT 1 CHECK (NivelActual >= 1),
    PuntajeTotal INT NOT NULL DEFAULT 0,
 Estado NVARCHAR(20) NOT NULL CHECK (Estado IN ('EnCurso', 'Finalizada', 'Abandonada')),
    CONSTRAINT FK_PartidasJuego_Jugadores FOREIGN KEY (JugadorId) REFERENCES dbo.Jugadores(Id)
);
GO

CREATE INDEX IX_PartidasJuego_JugadorId ON dbo.PartidasJuego(JugadorId);
CREATE INDEX IX_PartidasJuego_Estado ON dbo.PartidasJuego(Estado);
GO

-- Tabla RespuestasJugador
IF OBJECT_ID('dbo.RespuestasJugador', 'U') IS NOT NULL
 DROP TABLE dbo.RespuestasJugador;
GO

CREATE TABLE dbo.RespuestasJugador
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
  PartidaId INT NOT NULL,
 PreguntaId INT NOT NULL,
  OpcionSeleccionadaId INT NOT NULL,
    EsCorrecta BIT NOT NULL,
    PuntosObtenidos INT NOT NULL,
    FechaRespuesta DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_RespuestasJugador_Partidas FOREIGN KEY (PartidaId) REFERENCES dbo.PartidasJuego(Id) ON DELETE CASCADE,
    CONSTRAINT FK_RespuestasJugador_Preguntas FOREIGN KEY (PreguntaId) REFERENCES dbo.Preguntas(Id),
    CONSTRAINT FK_RespuestasJugador_Opciones FOREIGN KEY (OpcionSeleccionadaId) REFERENCES dbo.OpcionesRespuesta(Id)
);
GO

CREATE INDEX IX_RespuestasJugador_PartidaId ON dbo.RespuestasJugador(PartidaId);
CREATE INDEX IX_RespuestasJugador_PreguntaId ON dbo.RespuestasJugador(PreguntaId);
GO

-- ===================================================================
-- STORED PROCEDURES - CATEGORIAS
-- ===================================================================

IF OBJECT_ID('dbo.sp_AgregarCategoria', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_AgregarCategoria;
GO

CREATE PROCEDURE dbo.sp_AgregarCategoria
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO dbo.Categorias (Nombre, Descripcion)
    VALUES (@Nombre, @Descripcion);
END
GO

IF OBJECT_ID('dbo.sp_ModificarCategoria', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ModificarCategoria;
GO

CREATE PROCEDURE dbo.sp_ModificarCategoria
    @Id INT,
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(500)
AS
BEGIN
 SET NOCOUNT ON;
    
    UPDATE dbo.Categorias
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_EliminarCategoria', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_EliminarCategoria;
GO

CREATE PROCEDURE dbo.sp_EliminarCategoria
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM dbo.Categorias WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerCategoriaPorId', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerCategoriaPorId;
GO

CREATE PROCEDURE dbo.sp_ObtenerCategoriaPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, Nombre, Descripcion
    FROM dbo.Categorias
    WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerTodasCategorias', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerTodasCategorias;
GO

CREATE PROCEDURE dbo.sp_ObtenerTodasCategorias
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, Nombre, Descripcion
    FROM dbo.Categorias
    ORDER BY Nombre;
END
GO

-- ===================================================================
-- STORED PROCEDURES - PREGUNTAS
-- ===================================================================

IF OBJECT_ID('dbo.sp_AgregarPregunta', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_AgregarPregunta;
GO

CREATE PROCEDURE dbo.sp_AgregarPregunta
    @TextoPregunta NVARCHAR(500),
    @CategoriaId INT,
    @Nivel INT,
    @PuntosAcierto INT,
    @PuntosError INT = NULL
AS
BEGIN
 SET NOCOUNT ON;
    
    INSERT INTO dbo.Preguntas (TextoPregunta, CategoriaId, Nivel, PuntosAcierto, PuntosError)
    VALUES (@TextoPregunta, @CategoriaId, @Nivel, @PuntosAcierto, @PuntosError);
END
GO

IF OBJECT_ID('dbo.sp_ModificarPregunta', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ModificarPregunta;
GO

CREATE PROCEDURE dbo.sp_ModificarPregunta
    @Id INT,
    @TextoPregunta NVARCHAR(500),
    @CategoriaId INT,
    @Nivel INT,
    @PuntosAcierto INT,
    @PuntosError INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE dbo.Preguntas
    SET TextoPregunta = @TextoPregunta,
        CategoriaId = @CategoriaId,
      Nivel = @Nivel,
        PuntosAcierto = @PuntosAcierto,
        PuntosError = @PuntosError
    WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_EliminarPregunta', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_EliminarPregunta;
GO

CREATE PROCEDURE dbo.sp_EliminarPregunta
  @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM dbo.Preguntas WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerPreguntaPorId', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerPreguntaPorId;
GO

CREATE PROCEDURE dbo.sp_ObtenerPreguntaPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, TextoPregunta, CategoriaId, Nivel, PuntosAcierto, PuntosError
    FROM dbo.Preguntas
    WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerTodasPreguntas', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerTodasPreguntas;
GO

CREATE PROCEDURE dbo.sp_ObtenerTodasPreguntas
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, TextoPregunta, CategoriaId, Nivel, PuntosAcierto, PuntosError
    FROM dbo.Preguntas
    ORDER BY Nivel, CategoriaId;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerPreguntasPorCategoria', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerPreguntasPorCategoria;
GO

CREATE PROCEDURE dbo.sp_ObtenerPreguntasPorCategoria
    @CategoriaId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, TextoPregunta, CategoriaId, Nivel, PuntosAcierto, PuntosError
    FROM dbo.Preguntas
    WHERE CategoriaId = @CategoriaId
    ORDER BY Nivel;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerPreguntasPorNivel', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerPreguntasPorNivel;
GO

CREATE PROCEDURE dbo.sp_ObtenerPreguntasPorNivel
  @Nivel INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, TextoPregunta, CategoriaId, Nivel, PuntosAcierto, PuntosError
    FROM dbo.Preguntas
    WHERE Nivel = @Nivel
    ORDER BY CategoriaId;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerPreguntasNoRespondidas', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerPreguntasNoRespondidas;
GO

CREATE PROCEDURE dbo.sp_ObtenerPreguntasNoRespondidas
    @PartidaId INT,
    @Nivel INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT p.Id, p.TextoPregunta, p.CategoriaId, p.Nivel, p.PuntosAcierto, p.PuntosError
    FROM dbo.Preguntas p
    WHERE p.Nivel = @Nivel
      AND p.Id NOT IN (
          SELECT PreguntaId 
          FROM dbo.RespuestasJugador 
          WHERE PartidaId = @PartidaId
      )
    ORDER BY NEWID(); -- Orden aleatorio
END
GO

IF OBJECT_ID('dbo.sp_ObtenerPreguntaCompleta', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerPreguntaCompleta;
GO

CREATE PROCEDURE dbo.sp_ObtenerPreguntaCompleta
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.Id,
    p.TextoPregunta,
        p.CategoriaId,
        p.Nivel,
        p.PuntosAcierto,
        p.PuntosError,
      o.Id AS OpcionId,
        o.TextoOpcion,
      o.EsCorrecta
    FROM dbo.Preguntas p
    LEFT JOIN dbo.OpcionesRespuesta o ON p.Id = o.PreguntaId
 WHERE p.Id = @Id;
END
GO

-- ===================================================================
-- STORED PROCEDURES - OPCIONES RESPUESTA
-- ===================================================================

IF OBJECT_ID('dbo.sp_AgregarOpcionRespuesta', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_AgregarOpcionRespuesta;
GO

CREATE PROCEDURE dbo.sp_AgregarOpcionRespuesta
    @PreguntaId INT,
    @TextoOpcion NVARCHAR(300),
    @EsCorrecta BIT
AS
BEGIN
 SET NOCOUNT ON;
    
    INSERT INTO dbo.OpcionesRespuesta (PreguntaId, TextoOpcion, EsCorrecta)
    VALUES (@PreguntaId, @TextoOpcion, @EsCorrecta);
END
GO

IF OBJECT_ID('dbo.sp_ModificarOpcionRespuesta', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ModificarOpcionRespuesta;
GO

CREATE PROCEDURE dbo.sp_ModificarOpcionRespuesta
    @Id INT,
    @PreguntaId INT,
    @TextoOpcion NVARCHAR(300),
    @EsCorrecta BIT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE dbo.OpcionesRespuesta
    SET PreguntaId = @PreguntaId,
    TextoOpcion = @TextoOpcion,
        EsCorrecta = @EsCorrecta
    WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_EliminarOpcionRespuesta', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_EliminarOpcionRespuesta;
GO

CREATE PROCEDURE dbo.sp_EliminarOpcionRespuesta
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM dbo.OpcionesRespuesta WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerOpcionRespuestaPorId', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerOpcionRespuestaPorId;
GO

CREATE PROCEDURE dbo.sp_ObtenerOpcionRespuestaPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, PreguntaId, TextoOpcion, EsCorrecta
    FROM dbo.OpcionesRespuesta
WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerTodasOpcionesRespuesta', 'P') IS NOT NULL
 DROP PROCEDURE dbo.sp_ObtenerTodasOpcionesRespuesta;
GO

CREATE PROCEDURE dbo.sp_ObtenerTodasOpcionesRespuesta
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, PreguntaId, TextoOpcion, EsCorrecta
    FROM dbo.OpcionesRespuesta
    ORDER BY PreguntaId;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerOpcionesPorPregunta', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerOpcionesPorPregunta;
GO

CREATE PROCEDURE dbo.sp_ObtenerOpcionesPorPregunta
    @PreguntaId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, PreguntaId, TextoOpcion, EsCorrecta
    FROM dbo.OpcionesRespuesta
    WHERE PreguntaId = @PreguntaId
    ORDER BY Id;
END
GO

-- ===================================================================
-- STORED PROCEDURES - JUGADORES
-- ===================================================================

IF OBJECT_ID('dbo.sp_AgregarJugador', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_AgregarJugador;
GO

CREATE PROCEDURE dbo.sp_AgregarJugador
    @Nombre NVARCHAR(100),
@Apellido NVARCHAR(100),
    @Email NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO dbo.Jugadores (Nombre, Apellido, Email)
    VALUES (@Nombre, @Apellido, @Email);
END
GO

IF OBJECT_ID('dbo.sp_ModificarJugador', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ModificarJugador;
GO

CREATE PROCEDURE dbo.sp_ModificarJugador
 @Id INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Email NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE dbo.Jugadores
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        Email = @Email
    WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_EliminarJugador', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_EliminarJugador;
GO

CREATE PROCEDURE dbo.sp_EliminarJugador
  @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM dbo.Jugadores WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerJugadorPorId', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerJugadorPorId;
GO

CREATE PROCEDURE dbo.sp_ObtenerJugadorPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, Nombre, Apellido, Email
    FROM dbo.Jugadores
    WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerTodosJugadores', 'P') IS NOT NULL
  DROP PROCEDURE dbo.sp_ObtenerTodosJugadores;
GO

CREATE PROCEDURE dbo.sp_ObtenerTodosJugadores
AS
BEGIN
    SET NOCOUNT ON;
    
SELECT Id, Nombre, Apellido, Email
    FROM dbo.Jugadores
    ORDER BY Apellido, Nombre;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerJugadorPorEmail', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerJugadorPorEmail;
GO

CREATE PROCEDURE dbo.sp_ObtenerJugadorPorEmail
@Email NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, Nombre, Apellido, Email
    FROM dbo.Jugadores
    WHERE Email = @Email;
END
GO

-- ===================================================================
-- STORED PROCEDURES - PARTIDAS JUEGO
-- ===================================================================

IF OBJECT_ID('dbo.sp_AgregarPartidaJuego', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_AgregarPartidaJuego;
GO

CREATE PROCEDURE dbo.sp_AgregarPartidaJuego
 @JugadorId INT,
    @FechaInicio DATETIME,
    @FechaFin DATETIME = NULL,
    @NivelActual INT,
    @PuntajeTotal INT,
    @Estado NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO dbo.PartidasJuego (JugadorId, FechaInicio, FechaFin, NivelActual, PuntajeTotal, Estado)
    VALUES (@JugadorId, @FechaInicio, @FechaFin, @NivelActual, @PuntajeTotal, @Estado);
END
GO

IF OBJECT_ID('dbo.sp_ModificarPartidaJuego', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ModificarPartidaJuego;
GO

CREATE PROCEDURE dbo.sp_ModificarPartidaJuego
    @Id INT,
    @JugadorId INT,
    @FechaInicio DATETIME,
    @FechaFin DATETIME = NULL,
    @NivelActual INT,
    @PuntajeTotal INT,
    @Estado NVARCHAR(20)
AS
BEGIN
  SET NOCOUNT ON;
    
    UPDATE dbo.PartidasJuego
    SET JugadorId = @JugadorId,
        FechaInicio = @FechaInicio,
        FechaFin = @FechaFin,
        NivelActual = @NivelActual,
    PuntajeTotal = @PuntajeTotal,
        Estado = @Estado
    WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_EliminarPartidaJuego', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_EliminarPartidaJuego;
GO

CREATE PROCEDURE dbo.sp_EliminarPartidaJuego
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM dbo.PartidasJuego WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerPartidaJuegoPorId', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerPartidaJuegoPorId;
GO

CREATE PROCEDURE dbo.sp_ObtenerPartidaJuegoPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, JugadorId, FechaInicio, FechaFin, NivelActual, PuntajeTotal, Estado
    FROM dbo.PartidasJuego
    WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerTodasPartidasJuego', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerTodasPartidasJuego;
GO

CREATE PROCEDURE dbo.sp_ObtenerTodasPartidasJuego
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, JugadorId, FechaInicio, FechaFin, NivelActual, PuntajeTotal, Estado
    FROM dbo.PartidasJuego
    ORDER BY FechaInicio DESC;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerPartidasPorJugador', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerPartidasPorJugador;
GO

CREATE PROCEDURE dbo.sp_ObtenerPartidasPorJugador
    @JugadorId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, JugadorId, FechaInicio, FechaFin, NivelActual, PuntajeTotal, Estado
    FROM dbo.PartidasJuego
    WHERE JugadorId = @JugadorId
    ORDER BY FechaInicio DESC;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerPartidaEnCurso', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerPartidaEnCurso;
GO

CREATE PROCEDURE dbo.sp_ObtenerPartidaEnCurso
    @JugadorId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT TOP 1 Id, JugadorId, FechaInicio, FechaFin, NivelActual, PuntajeTotal, Estado
    FROM dbo.PartidasJuego
    WHERE JugadorId = @JugadorId AND Estado = 'EnCurso'
    ORDER BY FechaInicio DESC;
END
GO

-- ===================================================================
-- STORED PROCEDURES - RESPUESTAS JUGADOR
-- ===================================================================

IF OBJECT_ID('dbo.sp_AgregarRespuestaJugador', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_AgregarRespuestaJugador;
GO

CREATE PROCEDURE dbo.sp_AgregarRespuestaJugador
    @PartidaId INT,
    @PreguntaId INT,
    @OpcionSeleccionadaId INT,
    @EsCorrecta BIT,
  @PuntosObtenidos INT,
    @FechaRespuesta DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO dbo.RespuestasJugador (PartidaId, PreguntaId, OpcionSeleccionadaId, EsCorrecta, PuntosObtenidos, FechaRespuesta)
    VALUES (@PartidaId, @PreguntaId, @OpcionSeleccionadaId, @EsCorrecta, @PuntosObtenidos, @FechaRespuesta);
END
GO

IF OBJECT_ID('dbo.sp_ModificarRespuestaJugador', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ModificarRespuestaJugador;
GO

CREATE PROCEDURE dbo.sp_ModificarRespuestaJugador
    @Id INT,
    @PartidaId INT,
    @PreguntaId INT,
    @OpcionSeleccionadaId INT,
    @EsCorrecta BIT,
    @PuntosObtenidos INT,
    @FechaRespuesta DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE dbo.RespuestasJugador
    SET PartidaId = @PartidaId,
        PreguntaId = @PreguntaId,
        OpcionSeleccionadaId = @OpcionSeleccionadaId,
        EsCorrecta = @EsCorrecta,
        PuntosObtenidos = @PuntosObtenidos,
  FechaRespuesta = @FechaRespuesta
    WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_EliminarRespuestaJugador', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_EliminarRespuestaJugador;
GO

CREATE PROCEDURE dbo.sp_EliminarRespuestaJugador
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM dbo.RespuestasJugador WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerRespuestaJugadorPorId', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerRespuestaJugadorPorId;
GO

CREATE PROCEDURE dbo.sp_ObtenerRespuestaJugadorPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, PartidaId, PreguntaId, OpcionSeleccionadaId, EsCorrecta, PuntosObtenidos, FechaRespuesta
    FROM dbo.RespuestasJugador
    WHERE Id = @Id;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerTodasRespuestasJugador', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerTodasRespuestasJugador;
GO

CREATE PROCEDURE dbo.sp_ObtenerTodasRespuestasJugador
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, PartidaId, PreguntaId, OpcionSeleccionadaId, EsCorrecta, PuntosObtenidos, FechaRespuesta
    FROM dbo.RespuestasJugador
    ORDER BY FechaRespuesta;
END
GO

IF OBJECT_ID('dbo.sp_ObtenerRespuestasPorPartida', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerRespuestasPorPartida;
GO

CREATE PROCEDURE dbo.sp_ObtenerRespuestasPorPartida
    @PartidaId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, PartidaId, PreguntaId, OpcionSeleccionadaId, EsCorrecta, PuntosObtenidos, FechaRespuesta
    FROM dbo.RespuestasJugador
    WHERE PartidaId = @PartidaId
    ORDER BY FechaRespuesta;
END
GO

-- ===================================================================
-- DATOS INICIALES - Al menos 5 categorías
-- ===================================================================

INSERT INTO dbo.Categorias (Nombre, Descripcion) VALUES 
    ('Historia', 'Preguntas sobre eventos históricos y personajes'),
    ('Geografía', 'Preguntas sobre países, capitales y accidentes geográficos'),
    ('Ciencia', 'Preguntas sobre física, química, biología y astronomía'),
    ('Deportes', 'Preguntas sobre deportes, atletas y competiciones'),
    ('Arte y Cultura', 'Preguntas sobre literatura, música, cine y artes plásticas'),
    ('Tecnología', 'Preguntas sobre informática, innovación y avances tecnológicos');
GO

-- Preguntas de ejemplo (Nivel 1)
INSERT INTO dbo.Preguntas (TextoPregunta, CategoriaId, Nivel, PuntosAcierto, PuntosError) VALUES
    ('¿En qué año se descubrió América?', 1, 1, 10, NULL),
  ('¿Cuál es la capital de Francia?', 2, 1, 10, NULL),
    ('¿Cuántos planetas tiene el sistema solar?', 3, 1, 10, NULL),
    ('¿En qué deporte se utiliza una raqueta?', 4, 1, 10, NULL),
    ('¿Quién pintó la Mona Lisa?', 5, 1, 10, NULL);
GO

-- Preguntas de ejemplo (Nivel 4 - con penalización)
INSERT INTO dbo.Preguntas (TextoPregunta, CategoriaId, Nivel, PuntosAcierto, PuntosError) VALUES
    ('¿Quién fue el primer emperador romano?', 1, 4, 50, -25),
    ('¿Cuál es el río más largo del mundo?', 2, 4, 50, -25),
    ('¿Qué científico formuló la teoría de la relatividad?', 3, 4, 50, -25);
GO

-- Opciones para la primera pregunta (¿En qué año se descubrió América?)
INSERT INTO dbo.OpcionesRespuesta (PreguntaId, TextoOpcion, EsCorrecta) VALUES
    (1, '1492', 1),
    (1, '1500', 0),
    (1, '1485', 0),
(1, '1520', 0);
GO

-- Opciones para la segunda pregunta (¿Cuál es la capital de Francia?)
INSERT INTO dbo.OpcionesRespuesta (PreguntaId, TextoOpcion, EsCorrecta) VALUES
    (2, 'París', 1),
    (2, 'Londres', 0),
    (2, 'Berlín', 0),
    (2, 'Madrid', 0);
GO

-- Opciones para la tercera pregunta (¿Cuántos planetas tiene el sistema solar?)
INSERT INTO dbo.OpcionesRespuesta (PreguntaId, TextoOpcion, EsCorrecta) VALUES
    (3, '8', 1),
    (3, '9', 0),
    (3, '7', 0),
    (3, '10', 0);
GO

-- Opciones para la cuarta pregunta (¿En qué deporte se utiliza una raqueta?)
INSERT INTO dbo.OpcionesRespuesta (PreguntaId, TextoOpcion, EsCorrecta) VALUES
    (4, 'Tenis', 1),
    (4, 'Fútbol', 0),
    (4, 'Natación', 0),
    (4, 'Atletismo', 0);
GO

-- Opciones para la quinta pregunta (¿Quién pintó la Mona Lisa?)
INSERT INTO dbo.OpcionesRespuesta (PreguntaId, TextoOpcion, EsCorrecta) VALUES
    (5, 'Leonardo da Vinci', 1),
    (5, 'Miguel Ángel', 0),
 (5, 'Rafael', 0),
    (5, 'Botticelli', 0);
GO

-- Opciones para pregunta nivel 4 (¿Quién fue el primer emperador romano?)
INSERT INTO dbo.OpcionesRespuesta (PreguntaId, TextoOpcion, EsCorrecta) VALUES
    (6, 'Augusto', 1),
    (6, 'Julio César', 0),
    (6, 'Nerón', 0),
    (6, 'Trajano', 0);
GO

-- Opciones para pregunta nivel 4 (¿Cuál es el río más largo del mundo?)
INSERT INTO dbo.OpcionesRespuesta (PreguntaId, TextoOpcion, EsCorrecta) VALUES
    (7, 'Amazonas', 1),
    (7, 'Nilo', 0),
    (7, 'Yangtsé', 0),
    (7, 'Misisipi', 0);
GO

-- Opciones para pregunta nivel 4 (¿Qué científico formuló la teoría de la relatividad?)
INSERT INTO dbo.OpcionesRespuesta (PreguntaId, TextoOpcion, EsCorrecta) VALUES
    (8, 'Albert Einstein', 1),
    (8, 'Isaac Newton', 0),
    (8, 'Galileo Galilei', 0),
    (8, 'Stephen Hawking', 0);
GO

PRINT 'Base de datos TriviaDB creada exitosamente con todas las tablas, stored procedures y datos iniciales.';
GO
