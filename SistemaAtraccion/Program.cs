using System;
using SistemaAtraccion;

ColaAtraccion atraccion = new ColaAtraccion();
int opcion = 0;

// Configurar codificación para caracteres especiales de consola (como bordes y emojis)
Console.OutputEncoding = System.Text.Encoding.UTF8;

do
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("\n============================================================");
    Console.WriteLine("                 PARQUE DE DIVERSIONES                      ");
    Console.WriteLine("            SISTEMA DE ATRACCION Y ASIENTOS                 ");
    Console.WriteLine("============================================================");
    Console.ResetColor();

    Console.WriteLine(" 1. Registrar visitante en cola de espera");
    Console.WriteLine(" 2. Asignar asiento (Siguiente en cola)");
    Console.WriteLine(" 3. Mostrar cola de espera");
    Console.WriteLine(" 4. Mostrar estado de asientos (Mapa visual)");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(" 5. Buscar visitante (por Nombre o Turno)");
    Console.WriteLine(" 6. Ver historial de visitantes a bordo");
    Console.WriteLine(" 7. Generar reporte estadístico detallado");
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(" 8. Salir del sistema");
    Console.ResetColor();
    Console.WriteLine("------------------------------------------------------------");
    Console.Write(" Seleccione una opcion (1-8): ");

    string inputOpcion = Console.ReadLine() ?? "";
    if (!int.TryParse(inputOpcion, out opcion))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n[Error] Por favor, ingrese un número válido entre 1 y 8.");
        Console.ResetColor();
        continue;
    }

    switch (opcion)
    {
        case 1:
            Console.Write("\nNombre del visitante: ");
            string nombre = Console.ReadLine() ?? "";
            
            Console.Write("Edad del visitante: ");
            string inputEdad = Console.ReadLine() ?? "";
            int edad;
            if (!int.TryParse(inputEdad, out edad))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Error] La edad debe ser un número entero.");
                Console.ResetColor();
                break;
            }

            atraccion.RegistrarVisitante(nombre, edad);
            break;

        case 2:
            atraccion.AsignarAsiento();
            break;

        case 3:
            atraccion.MostrarCola();
            break;

        case 4:
            atraccion.MostrarAsientos();
            break;

        case 5:
            Console.Write("\nIngrese el nombre o número de turno a buscar: ");
            string criterio = Console.ReadLine() ?? "";
            atraccion.BuscarVisitante(criterio);
            break;

        case 6:
            atraccion.MostrarHistorialAbordados();
            break;

        case 7:
            atraccion.MostrarReporteEstadistico();
            break;

        case 8:
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n¡Gracias por utilizar el Sistema de Atracción! Saliendo...");
            Console.ResetColor();
            break;

        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n[Error] Opción incorrecta. Ingrese un valor entre 1 y 8.");
            Console.ResetColor();
            break;
    }

    if (opcion != 8)
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }

} while (opcion != 8);
