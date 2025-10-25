namespace ConUni_RestFul_Dotnet_GR06.Servicios
{
    /// <summary>
    /// Servicio de conversión de masa
    /// </summary>
    public class ConversorMasa
    {
        // Kilogramo <-> Gramo
        public double KilogramoAGramo(double kilogramos)
        {
            return kilogramos * 1000.0;
        }

        public double GramoAKilogramo(double gramos)
        {
            return gramos / 1000.0;
        }

        // Kilogramo <-> Libra
        public double KilogramoALibra(double kilogramos)
        {
            return kilogramos * 2.20462;
        }

        public double LibraAKilogramo(double libras)
        {
            return libras / 2.20462;
        }

        // Gramo <-> Libra
        public double GramoALibra(double gramos)
        {
            return KilogramoALibra(GramoAKilogramo(gramos));
        }

        public double LibraAGramo(double libras)
        {
            return KilogramoAGramo(LibraAKilogramo(libras));
        }
    }
}
