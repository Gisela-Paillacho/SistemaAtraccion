namespace SistemaAtraccion
{
    public class Persona
    {
        // Propiedades
        public int Turno { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }

        // Constructor
        public Persona(int turno, string nombre, int edad)
        {
            Turno = turno;
            Nombre = nombre;
            Edad = edad;
        }

        // Método para mostrar los datos
        public void MostrarDatos()
        {
            Console.WriteLine($"Turno: {Turno} | Nombre: {Nombre} | Edad: {Edad}");
        }
    }
}
