using System;
using System.Collections.Generic;

namespace SistemaAtraccion
{
    public class ColaAtraccion
    {
        private Queue<Persona> cola = new Queue<Persona>();
        private const int MAX_ASIENTOS = 30;
        private int asientosOcupados = 0;
        private int turnoActual = 1;

        // Registrar un nuevo visitante en la cola de espera
        public void RegistrarVisitante(string nombre, int edad)
        {
            Persona persona = new Persona(turnoActual, nombre, edad);
            cola.Enqueue(persona);

            Console.WriteLine($"\nVisitante registrado correctamente.");
            Console.WriteLine($"Turno asignado: {turnoActual}");

            turnoActual++;
        }

        // Asignar asiento
        public void AsignarAsiento()
        {
            if (cola.Count == 0)
            {
                Console.WriteLine("\nNo hay personas en la cola.");
                return;
            }

            if (asientosOcupados >= MAX_ASIENTOS)
            {
                Console.WriteLine("\nTodos los asientos están ocupados.");
                return;
            }

            Persona persona = cola.Dequeue();

            asientosOcupados++;

            Console.WriteLine($"\nSube a la atracción:");
            persona.MostrarDatos();

            Console.WriteLine($"Asiento asignado: {asientosOcupados}");
        }

        // Mostrar cola
        public void MostrarCola()
        {
            if (cola.Count == 0)
            {
                Console.WriteLine("\nLa cola está vacía.");
                return;
            }

            Console.WriteLine("\n===== COLA DE ESPERA =====");

            foreach (Persona persona in cola)
            {
                persona.MostrarDatos();
            }
        }

        // Mostrar estado de los asientos
        public void MostrarAsientos()
        {
            Console.WriteLine("\n===== ESTADO DE LOS ASIENTOS =====");
            Console.WriteLine($"Capacidad total: {MAX_ASIENTOS}");
            Console.WriteLine($"Asientos ocupados: {asientosOcupados}");
            Console.WriteLine($"Asientos disponibles: {MAX_ASIENTOS - asientosOcupados}");
        }
    }
}