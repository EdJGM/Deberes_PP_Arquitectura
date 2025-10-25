namespace ConUni_RestFul_Dotnet_GR06.Servicios
{
    /// <summary>
    /// Servicio de conversión de longitudes
    /// </summary>
    public class ConversorLongitud
    {
        // Metro <-> Kilómetro
        public double MetroAKilometro(double metros)
        {
            return metros / 1000.0;
        }

        public double KilometroAMetro(double kilometros)
        {
            return kilometros * 1000.0;
        }

        // Metro <-> Milla
        public double MetroAMilla(double metros)
        {
            return metros / 1609.344;
        }

        public double MillaAMetro(double millas)
        {
            return millas * 1609.344;
        }

        // Kilómetro <-> Milla
        public double KilometroAMilla(double kilometros)
        {
            return MetroAMilla(KilometroAMetro(kilometros));
        }

        public double MillaAKilometro(double millas)
        {
            return MetroAKilometro(MillaAMetro(millas));
        }
    }
}
