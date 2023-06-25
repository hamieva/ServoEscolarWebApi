using System.Text;

namespace ServoEscolarWebApi.Datos
{
    public static partial class Datos
    {
        static Datos()
        {
            try
            {
                Familias = new List<Familia>();
                CiclosEscolares = new List<CicloEscolar>();
                Inscripciones = new List<Inscripcion>();
                Alumnos = new List<Alumno>();
                Familiares = new List<Familiar>();
                Grados = new()
            {
                {1, ("Preescolar", "K1") },
                {2, ("Preescolar", "K2") },
                {3, ("Preescolar", "K3") },
                {4, ("Primaria", "P1") },
                {5, ("Primaria", "P2") },
                {6, ("Primaria", "P3") },
                {7, ("Primaria", "P4") },
                {8, ("Primaria", "P5") },
                {9, ("Primaria", "P6") },
                {10, ("Secundaria", "S1") },
                {11, ("Secundaria", "S2") },
                {12, ("Secundaria", "S3") }
            };
                GenerarDatos();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al generar los datos de prueba.", ex);
            }
        }

        private static readonly DateTime FechaInicialModificacion = DateTime.Now.AddYears(-1);

        private static readonly DateTime FechaFinalModificacion = DateTime.Now;

        private static readonly Random random = new();

        internal static Dictionary<int, (string, string)> Grados
        {
            get;
        }

        internal static List<Familia> Familias
        {
            get;
        }

        internal static List<Alumno> Alumnos
        {
            get;
        }

        internal static List<CicloEscolar> CiclosEscolares
        {
            get;
        }

        internal static List<Inscripcion> Inscripciones
        {
            get;
        }

        internal static List<Familiar> Familiares
        {
            get;
        }

        private static void GenerarDatos()
        {
            var numeroFamilias = random.Next(20, 50);

            GenerarFamilias(numeroFamilias);
            GenerarAlumnos();
            GenerarCiclosEscolares();
            GenerarInscripciones();
            GenerarFamiliares();
        }

        private static void GenerarFamilias(int numeroFamilias)
        {
            for (int i = 1; i <= numeroFamilias; i++)
            {
                var nuevaFamilia = new Familia()
                {
                    Id = i,
                    Clave = $"Clave {i}",
                    Apellidos = $"Apellidos {i}",
                    FechaModificacion = ObtenerFechaModificacion(),
                    Alumnos = new List<Alumno>(),
                    Familiares = new List<Familiar>()
                };

                Familias.Add(nuevaFamilia);
            }
        }

        private static void GenerarAlumnos()
        {
            var idAlumno = 0;

            foreach (var familia in Familias)
            {
                var cantidad = random.Next(1, 4);

                for (int i = 1; i <= cantidad; i++)
                {
                    idAlumno++;
                    var nuevoAlumno = new Alumno()
                    {
                        Id = idAlumno,
                        Nombre = $"Nombre {idAlumno}",
                        ApellidoPaterno = $"Apellido Paterno {idAlumno}",
                        ApellidoMaterno = $"Apellido Materno {idAlumno}",
                        EstadoNacimiento = $"Estado de nacimiento {idAlumno}",
                        CURP = $"CURP {idAlumno}",
                        FamiliaId = familia.Id,
                        FechaNacimiento = ObtenerFechaNacimientoAleatoria(),
                        Matricula = ObtenerMatriculaAleatoria(),
                        RFC = $"RFC {idAlumno}",
                        Sexo = ObtenerSexo(idAlumno),
                        FechaModificacion = ObtenerFechaModificacion()
                    };
                    Alumnos.Add(nuevoAlumno);
                }
            }
        }

        private static void GenerarCiclosEscolares()
        {
            var cantidad = 20;
            var añoFinal = DateTime.Now.Year;
            var idCicloEscolar = 0;

            for (int i = añoFinal - cantidad; i <= añoFinal; i++)
            {
                idCicloEscolar++;
                var nuevoCicloEscolar = new CicloEscolar()
                {
                    Id = idCicloEscolar,
                    Año = i,
                    Etiqueta = $"Ciclo {i}",
                    Nombre = $"Ciclo escolar {i} - {i + 1}",
                    FechaInicial = ObtenerFechaInicial(i),
                    FechaFinal = ObtenerFechaFinal(i),
                    FechaModificacion = ObtenerFechaModificacion()
                };
                CiclosEscolares.Add(nuevoCicloEscolar);
            }
        }

        private static void GenerarInscripciones()
        {
            var idInscripcion = 0;

            foreach (var alumno in Alumnos)
            {
                var inscripciones = random.Next(1, 12);
                var estatus = ObtenerEstatus(inscripciones);
                var idCicloEscolarInicial = ObtenerIdCicloEscolarInicial(inscripciones, estatus);
                var idGradoInicial = ObtenerIdGradoInicial(inscripciones, estatus);
                var seccionInicial = ObtenerSeccionInicial(idGradoInicial);

                var idCicloEscolar = idCicloEscolarInicial;

                for (int i = 1; i <= inscripciones; i++)
                {
                    idInscripcion++;

                    var fechaInscripcion = ObtenerFechaInscripcion(idCicloEscolar);
                    var seccion = ObtenerSeccion(idGradoInicial, i);
                    var grado = ObtenerGrado(idGradoInicial, i);
                    var grupo = ObtenerGrupo();

                    var inscripcion = new Inscripcion()
                    {
                        Id = idInscripcion,
                        AlumnoId = alumno.Id,
                        CicloEscolarId = idCicloEscolar,
                        FechaInscripcion = fechaInscripcion,
                        Seccion = seccion,
                        Grado = grado,
                        Grupo = grupo,
                        TipoIngreso = i == 1 ? "Nuevo Ingreso" : "Regular",
                        Baja = estatus == Estatus.Baja && i == inscripciones,
                        FechaBaja = estatus == Estatus.Baja ? ObtenerFechaBaja(fechaInscripcion, idCicloEscolar) : null,
                        FechaModificacion = ObtenerFechaModificacion()
                    };
                    Inscripciones.Add(inscripcion);

                    idCicloEscolar++;
                }
            }
            var bajas = Inscripciones.Where(i => i.Baja).ToList();
        }
    }
    
    public static partial class Datos
    { 
        private static DateTime? ObtenerFechaBaja(DateTime fechaInscripcion, int idCicloEscolar)
        {
            var cicloEscolar = CiclosEscolares.First(c => c.Id == idCicloEscolar);
            var fecha = GenerarFechaAleatoria(fechaInscripcion, cicloEscolar.FechaFinal);
            return fecha;
        }

        private static void GenerarFamiliares()
        {
            foreach (var familia in Familias)
            {
                var cantidad = random.Next(2, 5);

                for (int i = 1; i <= cantidad; i++)
                {
                    var sexo = ObtenerSexo(i);

                    Familiares.Add(new Familiar()
                    {
                        Id = i,
                        ApellidoPaterno = $"Apellido paterno {i}",
                        ApellidoMaterno = $"Apellido materno {i}",
                        Contraseña = ObtenerContraseña(),
                        CURP = $"CURP {i}",
                        EstadoNacimiento = $"Estado de nacimiento {i}",
                        FamiliaId = familia.Id,
                        FechaNacimiento = ObtenerFechaNacimientoAleatoria(),
                        Nombre = $"Nombre {i}",
                        NombreUsuario = $"Nombre usuario {i}",
                        Parentesco = ObtenerParentesco(i, sexo),
                        RFC = ObtenerRFC(),
                        ServiciosWeb = ObtenerServiciosWeb(),
                        Sexo = sexo,
                        Telefonos = ObtenerTelefonos()
                    });
                }
            }
        }

        private static DateTime ObtenerFechaNacimientoAleatoria()
        {
            var año = random.Next(1995, DateTime.Now.Year);
            var mes = random.Next(1, 12);
            var diasEnMes = DateTime.DaysInMonth(año, mes);
            var dia = random.Next(1, diasEnMes);

            var fechaNacimiento = new DateTime(año, mes, dia);
            return fechaNacimiento;
        }

        private static string ObtenerMatriculaAleatoria()
        {
            var matricula = random.Next(100000, 300000);
            return matricula.ToString();
        }

        private static string ObtenerSexo(int numero)
        {
            return numero % 2 == 0 ? "Masculino" : "Femenino";
        }

        private static string ObtenerParentesco(int i, string sexo)
        {
            if (i == 1)
            {
                return "Madre";
            }
            else if (i == 2)
            {
                return "Padre";
            }

            if (sexo == "Masculino")
            {
                string[] parentescosMasculinos = { "Hermano", "Tío", "Abuelo", "Primo" };
                int index = random.Next(parentescosMasculinos.Length);
                return parentescosMasculinos[index];
            }
            else if (sexo == "Femenino")
            {
                string[] parentescosFemeninos = { "Hermana", "Tía", "Abuela", "Prima" };
                int index = random.Next(parentescosFemeninos.Length);
                return parentescosFemeninos[index];
            }
            else
            {
                return "Sin parentesco asignado";
            }
        }

        private static string ObtenerRFC()
        {
            var vocales = new string[] { "A", "E", "I", "O", "U" };
            var consonantes = new string[] { "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z" };
            var homoclaveCaracteres = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "Ñ", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            // Generar letras iniciales
            var rfc = "";
            rfc += consonantes[random.Next(consonantes.Length)];
            rfc += vocales[random.Next(vocales.Length)];
            rfc += consonantes[random.Next(consonantes.Length)];

            // Generar números aleatorios de 0 a 9
            for (int i = 0; i < 6; i++)
            {
                rfc += random.Next(10);
            }

            // Generar homoclave de 3 caracteres
            for (int i = 0; i < 3; i++)
            {
                rfc += homoclaveCaracteres[random.Next(homoclaveCaracteres.Length)];
            }

            return rfc;
        }

        private static string ObtenerContraseña()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var longitud = random.Next(5, 10);
            var stringBuilder = new StringBuilder(longitud);

            for (int i = 0; i < longitud; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }

            return stringBuilder.ToString();
        }

        private static IEnumerable<ServicioWeb> ObtenerServiciosWeb()
        {
            int cantidadServicios = random.Next(1, 5);
            var serviciosWeb = new List<ServicioWeb>();

            for (int i = 0; i < cantidadServicios; i++)
            {
                var servicio = new ServicioWeb
                {
                    Id = i + 1,
                    Etiqueta = $"Etiqueta {i}",
                    Cuenta = $"Cuenta {i}"
                };

                serviciosWeb.Add(servicio);
            }

            return serviciosWeb;
        }

        private static IEnumerable<Telefono> ObtenerTelefonos()
        {
            int cantidadTelefonos = random.Next(1, 5);
            var telefonos = new List<Telefono>();

            for (int i = 0; i < cantidadTelefonos; i++)
            {
                var telefono = new Telefono
                {
                    Id = i + 1,
                    Etiqueta = $"Etiqueta {i}",
                    Numero = $"Teléfono {i}"
                };

                telefonos.Add(telefono);
            }

            return telefonos;
        }

        private static DateTime ObtenerFechaInicial(int año)
        {
            var dia = random.Next(15, 31);
            return new DateTime(año, 8, dia);
        }

        private static DateTime ObtenerFechaFinal(int año)
        {
            var dia = random.Next(7, 20);
            return new DateTime(año + 1, 7, dia);
        }

        private static DateTime ObtenerFechaModificacion()
        {
            return GenerarFechaAleatoria(FechaInicialModificacion, FechaFinalModificacion);
        }

        private static string ObtenerGrupo()
        {
            var idGrupo = random.Next(1, 3);

            if (idGrupo == 1)
            {
                return "A";
            }
            else if (idGrupo == 2)
            {
                return "B";
            }
            else
            {
                return "C";
            }
        }

        private static Estatus ObtenerEstatus(int inscripciones)
        {
            if (inscripciones == 12)
            {
                return Estatus.Regular;
            }

            var valoresEnum = Enum.GetValues(typeof(Estatus));
            var indiceAleatorio = random.Next(0, valoresEnum.Length);
            var valor = (Estatus)valoresEnum.GetValue(indiceAleatorio);
            return valor;
        }

        private static int ObtenerIdCicloEscolarInicial(int inscripciones, Estatus estatus)
        {
            var idCicloEscolar = CiclosEscolares.FirstOrDefault(c => c.Año == DateTime.Now.Year).Id;

            switch (estatus)
            {
                case Estatus.Regular:
                    return idCicloEscolar - inscripciones;
                case Estatus.Graduado:
                    return idCicloEscolar - inscripciones;
                case Estatus.Baja:
                    var idCicloBaja = idCicloEscolar - inscripciones - random.Next(1, inscripciones);
                    return idCicloBaja;
                default:
                    return 0;
            }
        }

        private static string ObtenerSeccionInicial(int idGradoIniial)
        {
            return Grados[idGradoIniial].Item1;
        }

        private static int ObtenerIdGradoInicial(int inscripciones, Estatus estatus)
        {
            var idGrado = 0;
            var valor = 12 - (inscripciones - 1);

            switch (estatus)
            {
                case Estatus.Regular:
                    idGrado = random.Next(1, valor + 1);
                    break;
                case Estatus.Graduado:
                    idGrado = valor;
                    break;
                case Estatus.Baja:
                    var limiteSuperior = Math.Max(1, valor - inscripciones);
                    idGrado = random.Next(1, limiteSuperior + 1);
                    break;
            }
            return idGrado;
        }

        private static DateTime ObtenerFechaInscripcion(int idCicloEscolar)
        {
            var cicloEscolar = CiclosEscolares.FirstOrDefault(c => c.Id == idCicloEscolar);
            var fechaInscripcion = GenerarFechaAleatoria(cicloEscolar.FechaInicial, cicloEscolar.FechaFinal);
            return fechaInscripcion;
        }

        private static DateTime GenerarFechaAleatoria(DateTime fechaInicio, DateTime fechaFin)
        {
            var ticks = (long)(random.NextDouble() * (fechaFin.Ticks - fechaInicio.Ticks)) + fechaInicio.Ticks;
            var fechaAleatoria = new DateTime(ticks);
            return fechaAleatoria;
        }

        private static string ObtenerSeccion(int idGradoInicial, int inscripcionNumero)
        {
            var index = idGradoInicial + inscripcionNumero - 1;
            var value = Grados[index];
            return value.Item1;
        }

        private static int ObtenerGrado(int idGradoInicial, int inscripcionNumero)
        {
            var index = idGradoInicial + inscripcionNumero - 1;
            var value = Grados[index];
            var grado = value.Item2[1..];
            return int.Parse(grado);
        }
    }
}