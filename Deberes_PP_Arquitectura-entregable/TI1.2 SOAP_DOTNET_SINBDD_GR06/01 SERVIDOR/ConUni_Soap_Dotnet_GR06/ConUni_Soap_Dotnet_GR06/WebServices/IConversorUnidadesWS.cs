using ConUni_Soap_Dotnet_GR06.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ConUni_Soap_Dotnet_GR06.WebServices
{
    /// <summary>
    /// Web Service SOAP para conversión de unidades
    /// </summary>
    [ServiceContract]
    public interface IConversorUnidadesWS
    {
        // ==================== AUTENTICACIÓN ====================
        [OperationContract]
        RespuestaAutenticacion Login(string username, string password);

        // ==================== TEMPERATURA ====================
        [OperationContract]
        double CelsiusAFahrenheit(double celsius);

        [OperationContract]
        double FahrenheitACelsius(double fahrenheit);

        [OperationContract]
        double CelsiusAKelvin(double celsius);

        [OperationContract]
        double KelvinACelsius(double kelvin);

        [OperationContract]
        double FahrenheitAKelvin(double fahrenheit);

        [OperationContract]
        double KelvinAFahrenheit(double kelvin);

        // ==================== LONGITUD ====================
        [OperationContract]
        double MetroAKilometro(double metros);

        [OperationContract]
        double KilometroAMetro(double kilometros);

        [OperationContract]
        double MetroAMilla(double metros);

        [OperationContract]
        double MillaAMetro(double millas);

        [OperationContract]
        double KilometroAMilla(double kilometros);

        [OperationContract]
        double MillaAKilometro(double millas);

        // ==================== MASA ====================
        [OperationContract]
        double KilogramoAGramo(double kilogramos);

        [OperationContract]
        double GramoAKilogramo(double gramos);

        [OperationContract]
        double KilogramoALibra(double kilogramos);

        [OperationContract]
        double LibraAKilogramo(double libras);

        [OperationContract]
        double GramoALibra(double gramos);

        [OperationContract]
        double LibraAGramo(double libras);
    }
}
