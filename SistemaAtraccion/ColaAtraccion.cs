using System;
using System.Collections.Generic;

namespace SistemaAtraccion
{
    public class ColaAtraccion
    {
        private Queue<Persona> cola = new Queue<Persona>();
        private List<Persona> historialAbordados = new List<Persona>();
        private const int MAX_ASIENTOS = 30;
        private int asientosOcupados = 0;
        private int turnoActual = 1;

        // Registrar un nuevo visitante en la cola de espera
        public void RegistrarVisitante(string nombre, int edad)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[Error] El nombre no puede estar vacío.");
                Console.ResetColor();
                return;
            }

            if (edad < 0 || edad > 120)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[Error] Edad inválida. Ingrese un valor entre 0 y 120.");
                Console.ResetColor();
                return;
            }

            Persona persona = new Persona(turnoActual, nombre, edad);
            cola.Enqueue(persona);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[OK] Visitante registrado correctamente.");
            Console.ResetColor();
            Console.WriteLine($"Turno asignado: {turnoActual}");

            turnoActual++;
        }

        // Asignar asiento
        public void AsignarAsiento()
        {
            if (cola.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[Aviso] No hay personas en la cola de espera.");
                Console.ResetColor();
                return;
            }

            if (asientosOcupados >= MAX_ASIENTOS)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[Límite] Todos los asientos de la atracción están ocupados.");
                Console.ResetColor();
                return;
            }

            Persona persona = cola.Dequeue();
            asientosOcupados++;
            persona.AsientoAsignado = asientosOcupados;
            historialAbordados.Add(persona);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nVisitante sube a la atraccion:");
            Console.ResetColor();
            persona.MostrarDatos();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Asiento asignado: {asientosOcupados}");
            Console.ResetColor();
        }

        // Mostrar cola
        public void MostrarCola()
        {
            if (cola.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[Aviso] La cola de espera está vacía.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n================ COLA DE ESPERA ================");
            Console.ResetColor();
            Console.WriteLine(string.Format("{0,-10} | {1,-25} | {2,-6}", "Turno", "Nombre", "Edad"));
            Console.WriteLine("------------------------------------------------");
            foreach (Persona persona in cola)
            {
                Console.WriteLine(string.Format("{0,-10} | {1,-25} | {2,-6}", persona.Turno, persona.Nombre, persona.Edad));
            }
            Console.WriteLine("------------------------------------------------");
        }

        // Mostrar estado de los asientos
        public void MostrarAsientos()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n================ ESTADO DE LOS ASIENTOS ================");
            Console.ResetColor();
            Console.WriteLine($"Capacidad total      : {MAX_ASIENTOS}");
            Console.WriteLine($"Asientos ocupados    : {asientosOcupados}");
            Console.WriteLine($"Asientos disponibles : {MAX_ASIENTOS - asientosOcupados}");
            
            // Visual representaction of seats
            Console.Write("Mapa de Asientos     : [");
            for (int i = 1; i <= MAX_ASIENTOS; i++)
            {
                if (i <= asientosOcupados)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("X"); // occupied
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("O"); // free
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("]");
            Console.ResetColor();
        }

        // BUSCAR VISITANTE (Reportería)
        public void BuscarVisitante(string criterio)
        {
            if (string.IsNullOrWhiteSpace(criterio))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[Error] El criterio de búsqueda no puede estar vacío.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"\nBuscando visitante: \"{criterio}\"...");
            bool encontrado = false;

            // Buscar en la cola de espera
            int posicionCola = 1;
            foreach (Persona p in cola)
            {
                if (p.Nombre.Contains(criterio, StringComparison.OrdinalIgnoreCase) || p.Turno.ToString() == criterio)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[EN COLA DE ESPERA] Turno: {p.Turno} | Nombre: {p.Nombre} | Edad: {p.Edad} | Posición: {posicionCola}");
                    Console.ResetColor();
                    encontrado = true;
                }
                posicionCola++;
            }

            // Buscar en el historial de abordados
            foreach (Persona p in historialAbordados)
            {
                if (p.Nombre.Contains(criterio, StringComparison.OrdinalIgnoreCase) || p.Turno.ToString() == criterio || p.AsientoAsignado.ToString() == criterio)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[A BORDO] Turno: {p.Turno} | Nombre: {p.Nombre} | Edad: {p.Edad} | Asiento Asignado: {p.AsientoAsignado}");
                    Console.ResetColor();
                    encontrado = true;
                }
            }

            if (!encontrado)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Error] No se encontro ningun visitante que coincida con el criterio.");
                Console.ResetColor();
            }
        }

        // REPORTE ESTADÍSTICO DETALLADO (Reportería)
        public void MostrarReporteEstadistico()
        {
            int enEspera = cola.Count;
            int aBordo = historialAbordados.Count;
            int totalRegistrados = enEspera + aBordo;

            double porcentajeOcupacion = (double)asientosOcupados / MAX_ASIENTOS * 100;

            // Estadísticas demográficas
            int niños = 0;          // 0 - 12
            int adolescentes = 0;   // 13 - 17
            int adultos = 0;        // 18 - 59
            int adultosMayores = 0; // 60+
            double sumaEdades = 0;

            List<Persona> todos = new List<Persona>(cola);
            todos.AddRange(historialAbordados);

            foreach (var p in todos)
            {
                sumaEdades += p.Edad;
                if (p.Edad <= 12) niños++;
                else if (p.Edad <= 17) adolescentes++;
                else if (p.Edad <= 59) adultos++;
                else adultosMayores++;
            }

            double edadPromedio = totalRegistrados > 0 ? Math.Round(sumaEdades / totalRegistrados, 1) : 0;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n============================================================");
            Console.WriteLine("            REPORTE ESTADISTICO DE LA ATRACCION             ");
            Console.WriteLine("============================================================");
            Console.ResetColor();

            Console.WriteLine($" - Total de Visitantes Registrados : {totalRegistrados}");
            Console.WriteLine($" - Visitantes en Cola de Espera  : {enEspera}");
            Console.WriteLine($" - Visitantes actualmente a Bordo: {aBordo}");
            Console.WriteLine($" - Capacidad de la Atraccion     : {asientosOcupados}/{MAX_ASIENTOS} ({porcentajeOcupacion:0.0}%)");
            Console.WriteLine($" - Edad Promedio de Visitantes   : {edadPromedio} anos");
            Console.WriteLine("------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Distribución Demográfica de Visitantes:");
            Console.ResetColor();
            Console.WriteLine($"   - Ninos (0-12 anos)        : {niños}");
            Console.WriteLine($"   - Adolescentes (13-17 anos): {adolescentes}");
            Console.WriteLine($"   - Adultos (18-59 anos)     : {adultos}");
            Console.WriteLine($"   - Adultos Mayores (60+ anos): {adultosMayores}");
            Console.WriteLine("------------------------------------------------------------");
        }

        // HISTORIAL DE VISITANTES A BORDO (Reportería)
        public void MostrarHistorialAbordados()
        {
            if (historialAbordados.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[Aviso] Ningún visitante ha abordado la atracción todavía.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n================ HISTORIAL DE VISITANTES A BORDO ================");
            Console.ResetColor();
            Console.WriteLine(string.Format("{0,-10} | {1,-25} | {2,-6} | {3,-16}", "Turno", "Nombre", "Edad", "Asiento Asignado"));
            Console.WriteLine("-----------------------------------------------------------------");
            foreach (Persona persona in historialAbordados)
            {
                Console.WriteLine(string.Format("{0,-10} | {1,-25} | {2,-6} | {3,-16}", persona.Turno, persona.Nombre, persona.Edad, $"Asiento {persona.AsientoAsignado}"));
            }
            Console.WriteLine("-----------------------------------------------------------------");
        }
    }
}