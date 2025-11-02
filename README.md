# Sistema de Trivia - UAI DAS TP4 Ejercicio 3

## ?? Descripción

Sistema de trivia desarrollado con **.NET 8**, **WinForms**, **ADO.NET** en modo conectado con **SQL Server** y **Stored Procedures**.

El proyecto implementa una arquitectura de capas limpia aplicando principios **SOLID**, **DRY**, **YAGNI**, **KISS**, **Clean Code** y **Clean Architecture**.

**Características destacadas:**
- ? **Inyección de Dependencias** con `Microsoft.Extensions.DependencyInjection`
- ? **Patrón Singleton** para DatabaseContext
- ? **Patrón Scoped** para Repositorios y Servicios
- ? **Variables de entorno** para configuración
- ? **Microsoft.Data.SqlClient** (versión moderna)
- ? **0 Warnings** de compilación ?

---

## ?? Requisitos Cumplidos

? Base de datos con stored procedures para CRUD de todas las tablas  
? Interfaz MDI con formularios SDI para administración  
? Sistema de niveles para el juego de trivia  
? A partir del nivel 4, respuestas erróneas restan la mitad de los puntos de acierto  
? Creación y administración de preguntas  
? Creación y administración de jugadores  
? Las preguntas no se repiten durante una partida  
? **6 categorías** (requisito: al menos 5)

---

## ? Inicio Rápido

### 1?? Crear la Base de Datos

```bash
# Opción A: Desde SSMS (SQL Server Management Studio)
# - Abrir Database/CreateDatabase.sql
# - Ejecutar (F5)

# Opción B: Desde línea de comandos
sqlcmd -S localhost -i Database/CreateDatabase.sql
```

### 2?? Configurar Conexión

**Opción A: Variables de Entorno (Recomendado para SQL Server Authentication)**

```bash
# Windows (PowerShell)
$env:TRIVIA_DB_SERVER="192.168.1.100,1433"
$env:TRIVIA_DB_DATABASE="TriviaDB"
$env:TRIVIA_DB_USER="sa"
$env:TRIVIA_DB_PASSWORD="tu_password"

# Windows (CMD)
set TRIVIA_DB_SERVER=192.168.1.100,1433
set TRIVIA_DB_DATABASE=TriviaDB
set TRIVIA_DB_USER=sa
set TRIVIA_DB_PASSWORD=tu_password

# Linux/Mac
export TRIVIA_DB_SERVER="192.168.1.100,1433"
export TRIVIA_DB_DATABASE="TriviaDB"
export TRIVIA_DB_USER="sa"
export TRIVIA_DB_PASSWORD="tu_password"
```

**Opción B: Connection String Completo**

```bash
# Windows (PowerShell)
$env:TRIVIA_CONNECTION_STRING="Server=192.168.1.100,1433;Database=TriviaDB;User Id=sa;Password=tu_password;TrustServerCertificate=true;"

# Linux/Mac
export TRIVIA_CONNECTION_STRING="Server=192.168.1.100,1433;Database=TriviaDB;User Id=sa;Password=tu_password;TrustServerCertificate=true;"
```

**Opción C: Valor por Defecto (Windows Authentication)**

Si no configuras variables de entorno, se usará:
```
Server=localhost;Database=TriviaDB;Integrated Security=true;TrustServerCertificate=true;
```

### 3?? Ejecutar

```bash
dotnet run --project UaiDasTp4Ej3/UaiDasTp4Ej3.csproj
```

---

## ??? Arquitectura del Proyecto

### Capas

```
UaiDasTp4Ej3.sln
??? UaiDasTp4Ej3    ? WinForms (Interfaz de usuario)
??? APP             ? Configuración y DI
??? SERV          ? Lógica de negocio
??? REPO            ? Acceso a datos (ADO.NET)
??? CONTEXT         ? Contexto de BD
??? ABS           ? Interfaces (abstracciones)
??? DOM  ? Entidades del dominio
```

### Entidades del Dominio

1. **Categoria**: Categorías de preguntas
2. **Pregunta**: Preguntas con nivel y puntajes
3. **OpcionRespuesta**: Opciones de respuesta (4 por pregunta)
4. **Jugador**: Jugadores registrados
5. **PartidaJuego**: Partidas jugadas
6. **RespuestaJugador**: Respuestas dadas en cada partida

### Formularios (MDI/SDI)

1. **Form1** (MDI) - Menú principal
2. **FormCategorias** - ABM de categorías
3. **FormPreguntas** - ABM de preguntas
4. **FormOpcionesRespuesta** - ABM de opciones
5. **FormJugadores** - ABM de jugadores
6. **FormSeleccionJugador** - Inicio de juego
7. **FormJuego** - Pantalla del juego
8. **FormHistorialPartidas** - Consulta de partidas

---

## ??? Base de Datos

### Tablas (6)

```sql
Categorias      -- Categorías de preguntas
Preguntas      -- Preguntas con niveles y puntuación
OpcionesRespuesta    -- 4 opciones por pregunta
Jugadores            -- Jugadores registrados
PartidasJuego      -- Partidas en curso o finalizadas
RespuestasJugador  -- Historial de respuestas
```

### Stored Procedures (31)

#### Por Entidad (CRUD Completo)
- **Categorias**: 5 SPs
- **Preguntas**: 8 SPs (incluye búsquedas especializadas)
- **OpcionesRespuesta**: 6 SPs
- **Jugadores**: 6 SPs
- **PartidasJuego**: 6 SPs
- **RespuestasJugador**: 5 SPs

#### Operaciones
- `sp_Agregar[Entidad]` - INSERT
- `sp_Modificar[Entidad]` - UPDATE
- `sp_Eliminar[Entidad]` - DELETE
- `sp_Obtener[Entidad]PorId` - SELECT por ID
- `sp_ObtenerTodas[Entidad]` - SELECT todos
- SPs especializados para consultas específicas

### Datos Iniciales

El script SQL incluye:
- **6 categorías**: Historia, Geografía, Ciencia, Deportes, Arte y Cultura, Tecnología
- **8 preguntas de ejemplo** (niveles 1 y 4)
- **32 opciones de respuesta**

---

## ?? Funcionalidades

### 1. Administración

**Categorías**
- Crear, editar, eliminar categorías
- Validación de nombres únicos

**Preguntas**
- Crear preguntas con nivel y puntuación
- Asignar categoría
- Definir penalización para nivel 4+
- Gestionar 4 opciones de respuesta por pregunta
- Solo una opción correcta

**Jugadores**
- Registrar jugadores con nombre, apellido y email
- Email único como validación

### 2. Juego

**Flujo del Juego**
1. Seleccionar jugador
2. Crear nueva partida o continuar existente
3. Responder preguntas del nivel actual
4. Las preguntas no se repiten en la misma partida
5. Avanzar de nivel al completar las preguntas
6. Sistema de puntuación con penalización en nivel 4+

**Puntuación**
- **Nivel 1-3**: Respuesta correcta suma puntos, incorrecta = 0
- **Nivel 4+**: Respuesta correcta suma puntos, incorrecta resta 50% de los puntos

### 3. Historial
- Ver todas las partidas jugadas
- Filtrar por jugador
- Filtrar por estado (EnCurso, Finalizada, Abandonada)

---

## ??? Configuración Avanzada

### Variables de Entorno Soportadas

| Variable | Descripción | Ejemplo |
|----------|-------------|---------|
| `TRIVIA_DB_SERVER` | Servidor SQL (con puerto si es necesario) | `192.168.1.100,1433` |
| `TRIVIA_DB_DATABASE` | Nombre de la base de datos | `TriviaDB` |
| `TRIVIA_DB_USER` | Usuario de SQL Server | `sa` |
| `TRIVIA_DB_PASSWORD` | Contraseña | `MiPassword123` |
| `TRIVIA_CONNECTION_STRING` | Connection string completo | `Server=...` |

### Prioridad de Configuración

1. Variables individuales (`TRIVIA_DB_*`)
2. Connection string completo (`TRIVIA_CONNECTION_STRING`)
3. Valor por defecto (Windows Authentication en localhost)

### Ejemplos de Connection Strings

**SQL Server Authentication (IP y Puerto)**
```
Server=192.168.1.100,1433;Database=TriviaDB;User Id=sa;Password=tu_password;TrustServerCertificate=true;
```

**Windows Authentication (Local)**
```
Server=localhost;Database=TriviaDB;Integrated Security=true;TrustServerCertificate=true;
```

**SQL Server Express**
```
Server=.\SQLEXPRESS;Database=TriviaDB;Integrated Security=true;TrustServerCertificate=true;
```

**Named Instance**
```
Server=SERVIDOR\INSTANCIA;Database=TriviaDB;Integrated Security=true;TrustServerCertificate=true;
```

---

## ?? Tecnologías Utilizadas

- **.NET 8**
- **C# 12** con características modernas:
  - Constructor primario
  - Expression-bodied members
  - Pattern matching
  - Null-coalescing operators
- **Windows Forms** para UI
- **ADO.NET** en modo conectado sincrónico
- **Microsoft.Data.SqlClient 6.1.2** (versión moderna)
- **SQL Server** con Stored Procedures

---

## ?? Buenas Prácticas Aplicadas

### Principios SOLID

- **S**ingle Responsibility: Cada clase tiene una única responsabilidad
- **O**pen/Closed: Extensible mediante interfaces
- **L**iskov Substitution: Las implementaciones respetan sus contratos
- **I**nterface Segregation: Interfaces específicas y cohesivas
- **D**ependency Inversion: Dependencia de abstracciones

### Otros Principios

- **DRY** (Don't Repeat Yourself)
- **KISS** (Keep It Simple, Stupid)
- **YAGNI** (You Aren't Gonna Need It)
- **Clean Code**: Nombres descriptivos, métodos cortos
- **Clean Architecture**: Separación de responsabilidades

### Características de C# 12

- ? Constructor primario en todas las clases
- ? Expression-bodied members donde es apropiado
- ? Braces obligatorios en todas las estructuras de control
- ? IDs de solo lectura en entidades
- ? Pattern matching moderno

---

## ?? Seguridad

- **SQL Injection**: Protegido mediante Stored Procedures con parámetros
- **Validación de entrada**: En todos los formularios
- **Manejo de errores**: Try-catch en todas las operaciones
- **Connection strings**: Soporte para variables de entorno (no hardcoded)
- **TrustServerCertificate**: Configurado para entornos de desarrollo

---

## ?? Reglas del Juego

1. El jugador inicia una nueva partida o continúa una existente
2. Se presentan preguntas aleatorias del nivel actual
3. Las preguntas no se repiten en la misma partida
4. Cada pregunta tiene 4 opciones (solo una correcta)
5. Los puntos varían según la pregunta
6. **Nivel 4+**: Respuestas incorrectas restan 50% de los puntos de acierto
7. Al completar las preguntas del nivel, se puede avanzar al siguiente
8. La partida se puede finalizar en cualquier momento

---

## ?? Uso del Sistema

### Primer Uso

1. **Ejecutar script SQL** para crear la base de datos
2. **Configurar variables de entorno** (opcional)
3. **Compilar y ejecutar** la aplicación
4. **Crear un jugador** (Administración ? Jugadores)
5. **Revisar preguntas** (ya hay 8 de ejemplo)
6. **Jugar** (Juego ? Iniciar Juego)

### Crear Preguntas

**Nivel 1-3 (Sin penalización)**
```
Pregunta: ¿En qué año se descubrió América?
Categoría: Historia
Nivel: 1
Puntos Acierto: 10
Tiene Penalización: NO

Opciones:
? 1492 (Correcta)
? 1500
? 1485
? 1520
```

**Nivel 4+ (Con penalización)**
```
Pregunta: ¿Quién fue el primer emperador romano?
Categoría: Historia
Nivel: 4
Puntos Acierto: 50
Tiene Penalización: SÍ
Puntos Error: -25 (automático: -50%)

Opciones:
? Augusto (Correcta)
? Julio César
? Nerón
? Trajano
```

---

## ?? Solución de Problemas

### Error de conexión a la base de datos

**Síntoma**: `Cannot open database 'TriviaDB'`

**Soluciones**:
1. Verificar que SQL Server esté ejecutándose
2. Verificar las variables de entorno o connection string
3. Ejecutar el script `Database/CreateDatabase.sql`

### No aparecen preguntas en el juego

**Síntoma**: "No hay más preguntas disponibles"

**Soluciones**:
1. Crear más preguntas para el nivel actual
2. Verificar que las preguntas tengan al menos 2 opciones
3. Verificar que al menos una opción sea correcta

### Error de autenticación SQL Server

**Síntoma**: `Login failed for user`

**Soluciones**:
1. Verificar usuario y contraseña en variables de entorno
2. Verificar que SQL Server Authentication esté habilitado
3. Verificar permisos del usuario en la base de datos

---

## ?? Comandos Útiles

```bash
# Compilar el proyecto
dotnet build

# Limpiar y recompilar
dotnet clean
dotnet build

# Ejecutar
dotnet run --project UaiDasTp4Ej3/UaiDasTp4Ej3.csproj

# Ver proyectos de la solución
dotnet sln list

# Restaurar paquetes
dotnet restore
```

---

## ?? Estructura de Archivos

```
UaiDasTp4Ej3/
??? Database/
?   ??? CreateDatabase.sql  ? Ejecutar primero
??? ABS/ ? Interfaces
?   ??? IRepositorioBase.cs
?   ??? IRepositorioCategoria.cs
?   ??? ...
??? DOM/    ? Entidades
?   ??? Categoria.cs
?   ??? Pregunta.cs
?   ??? ...
??? CONTEXT/            ? Conexión DB
?   ??? DatabaseContext.cs
??? REPO/        ? Repositorios ADO.NET
?   ??? RepositorioCategoria.cs
?   ??? ...
??? SERV/       ? Servicios
?   ??? ServicioJuego.cs
??? APP/      ? Configuración
?   ??? Configuracion.cs      ? Variables de entorno
??? UaiDasTp4Ej3/     ? WinForms
?   ??? Program.cs
?   ??? Form1.cs
?   ??? FormCategorias.cs
?   ??? ...
??? README.md        ?? Este archivo
```

---

## ?? Estadísticas del Proyecto

- **Proyectos**: 7
- **Entidades**: 6
- **Formularios**: 7
- **Stored Procedures**: 31
- **Líneas de código**: ~3,800
- **Versión .NET**: 8
- **Versión C#**: 12
- **Warnings de compilación**: 0 ?

---

## ?? Checklist de Implementación

- [x] Base de datos normalizada (3FN)
- [x] 31 Stored Procedures con CRUD completo
- [x] 6 Categorías predefinidas
- [x] Arquitectura en capas (7 proyectos)
- [x] Interfaces MDI y SDI
- [x] Sistema de niveles
- [x] Penalización en nivel 4+
- [x] Preguntas no se repiten
- [x] Gestión de jugadores
- [x] Gestión de preguntas con opciones
- [x] Historial de partidas
- [x] SOLID, DRY, KISS, YAGNI
- [x] Constructor primario (C# 12)
- [x] Expression bodies
- [x] IDs de solo lectura
- [x] Inyección de dependencias
- [x] ADO.NET modo conectado
- [x] Microsoft.Data.SqlClient 6.1.2
- [x] Variables de entorno para conexión
- [x] Script SQL idempotente
- [x] Documentación completa

---

## ?? Autor

Proyecto desarrollado para la asignatura **Desarrollo y Arquitectura de Software (DAS)**  
**Universidad Abierta Interamericana (UAI)**

**Tecnologías**: .NET 8, C# 12, WinForms, ADO.NET (Microsoft.Data.SqlClient), SQL Server  
**Arquitectura**: Clean Architecture con SOLID + DI  
**Estado**: ? Producción Ready (0 warnings, 0 errores)  
**Año**: 2025
