using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConUni_Soap_Dotnet_GR06.Servicios
{
    /// <summary>
    /// Servicio de conversión de temperaturas
    /// </summary>
    public class ConversorTemperatura
    {
        public double CelsiusAFahrenheit(double celsius)
        {
            return (celsius * 9.0 / 5.0) + 32;
        }

        public double FahrenheitACelsius(double fahrenheit)
        {
            return (fahrenheit - 32) * 5.0 / 9.0;
        }

        public double CelsiusAKelvin(double celsius)
        {
            return celsius + 273.15;
        }

        public double KelvinACelsius(double kelvin)
        {
            return kelvin - 273.15;
        }

        public double FahrenheitAKelvin(double fahrenheit)
        {
            return CelsiusAKelvin(FahrenheitACelsius(fahrenheit));
        }

        public double KelvinAFahrenheit(double kelvin)
        {
            return CelsiusAFahrenheit(KelvinACelsius(kelvin));
        }
    }
}