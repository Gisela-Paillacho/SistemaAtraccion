using SistemaAtraccion;

ColaAtraccion atraccion = new ColaAtraccion();

int opcion;

do
{
    Console.WriteLine("\n=================================");
    Console.WriteLine(" PARQUE DE DIVERSIONES ");
    Console.WriteLine(" Sistema de Asignación de Asientos");
    Console.WriteLine("=================================");
    Console.WriteLine("1. Registrar visitante");
    Console.WriteLine("2. Asignar asiento");
    Console.WriteLine("3. Mostrar cola");
    Console.WriteLine("4. Mostrar asientos");
    Console.WriteLine("5. Salir");
    Console.Write("Seleccione una opción: ");

    opcion = Convert.ToInt32(Console.ReadLine());

    switch (opcion)
    {
        case 1:

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Edad: ");
            int edad = Convert.ToInt32(Console.ReadLine());

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

            Console.WriteLine("\nGracias por utilizar el sistema.");

            break;

        default:

            Console.WriteLine("\nOpción incorrecta.");

            break;
    }

} while (opcion != 5);

