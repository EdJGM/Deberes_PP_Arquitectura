using ConUni_Soap_Dotnet_GR06.Modelos;
using ConUni_Soap_Dotnet_GR06.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ConUni_Soap_Dotnet_GR06.WebServices
{
    /// <summary>
    /// Implementación del Web Service
    /// </summary>
    public class ConversorUnidadesWS : IConversorUnidadesWS
    {
        private readonly ConversorTemperatura _conversorTemp;
        private readonly ConversorLongitud _conversorLong;
        private readonly ConversorMasa _conversorMasa;

        public ConversorUnidadesWS()
        {
            _conversorTemp = new ConversorTemperatura();
            _conversorLong = new ConversorLongitud();
            _conversorMasa = new ConversorMasa();
        }

        public RespuestaAutenticacion Login(string username, string password)
        {
            var usuario = AutenticacionService.Autenticar(username, password);

            if (usuario != null)
            {
                return new RespuestaAutenticacion(true, "Login exitoso", usuario);
            }
            else
            {
                return new RespuestaAutenticacion(false, "Credenciales inválidas");
            }
        }

        // ==================== TEMPERATURA ====================
        public double CelsiusAFahrenheit(double celsius) =>
            _conversorTemp.CelsiusAFahrenheit(celsius);

        public double FahrenheitACelsius(double fahrenheit) =>
            _conversorTemp.FahrenheitACelsius(fahrenheit);

        public double CelsiusAKelvin(double celsius) =>
            _conversorTemp.CelsiusAKelvin(celsius);

        public double KelvinACelsius(double kelvin) =>
            _conversorTemp.KelvinACelsius(kelvin);

        public double FahrenheitAKelvin(double fahrenheit) =>
            _conversorTemp.FahrenheitAKelvin(fahrenheit);

        public double KelvinAFahrenheit(double kelvin) =>
            _conversorTemp.KelvinAFahrenheit(kelvin);

        // ==================== LONGITUD ====================
        public double MetroAKilometro(double metros) =>
            _conversorLong.MetroAKilometro(metros);

        public double KilometroAMetro(double kilometros) =>
            _conversorLong.KilometroAMetro(kilometros);

        public double MetroAMilla(double metros) =>
            _conversorLong.MetroAMilla(metros);

        public double MillaAMetro(double millas) =>
            _conversorLong.MillaAMetro(millas);

        public double KilometroAMilla(double kilometros) =>
            _conversorLong.KilometroAMilla(kilometros);

        public double MillaAKilometro(double millas) =>
            _conversorLong.MillaAKilometro(millas);

        // ==================== MASA ====================
        public double KilogramoAGramo(double kilogramos) =>
            _conversorMasa.KilogramoAGramo(kilogramos);

        public double GramoAKilogramo(double gramos) =>
            _conversorMasa.GramoAKilogramo(gramos);

        public double KilogramoALibra(double kilogramos) =>
            _conversorMasa.KilogramoALibra(kilogramos);

        public double LibraAKilogramo(double libras) =>
            _conversorMasa.LibraAKilogramo(libras);

        public double GramoALibra(double gramos) =>
            _conversorMasa.GramoALibra(gramos);

        public double LibraAGramo(double libras) =>
            _conversorMasa.LibraAGramo(libras);
    }
}
