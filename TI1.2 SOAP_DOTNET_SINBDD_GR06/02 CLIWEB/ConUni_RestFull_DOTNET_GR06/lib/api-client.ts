// MODEL: Cliente SOAP para comunicarse con el servicio ConversorUnidadesWS (.NET WCF)

// Apunta esta URL al endpoint SOAP sin "?wsdl", por ejemplo (.NET WCF):
// NEXT_PUBLIC_API_URL=http://localhost:61088/WebServices/ConversorUnidadesWS.svc
const SOAP_ENDPOINT = process.env.NEXT_PUBLIC_API_URL
const SOAPENV = "http://schemas.xmlsoap.org/soap/envelope/"
// Para el servicio WCF indicado por el WSDL, el targetNamespace es tempuri.org
const NS = "http://tempuri.org/"
// Prefijo completo del Action en WCF según portType y operation
const SOAP_ACTION_PREFIX = "http://tempuri.org/IConversorUnidadesWS/"

export interface Usuario {
  username: string
  password: string
}

export interface RespuestaAutenticacion {
  exitoso: boolean
  mensaje: string
  usuario?: Usuario
}

function xmlEscape(value: string): string {
  return value
    .replace(/&/g, "&amp;")
    .replace(/</g, "&lt;")
    .replace(/>/g, "&gt;")
    .replace(/"/g, "&quot;")
    .replace(/'/g, "&apos;")
}

function buildSoapEnvelope(method: string, paramsXml: string): string {
  return `<?xml version="1.0" encoding="UTF-8"?>
<soapenv:Envelope xmlns:soapenv="${SOAPENV}" xmlns:mon="${NS}">
  <soapenv:Header/>
  <soapenv:Body>
    <mon:${method}>
      ${paramsXml}
    </mon:${method}>
  </soapenv:Body>
 </soapenv:Envelope>`
}

async function postSoap(method: string, params: Record<string, string | number>): Promise<string> {
  // En el navegador, usa un proxy Next.js para evitar CORS; en servidor puede ir directo
  const isBrowser = typeof window !== "undefined"

  const paramsXml = Object.entries(params)
    .map(([k, v]) => `<mon:${k}>${typeof v === "number" ? v : xmlEscape(v)}</mon:${k}>`)
    .join("")
  const body = buildSoapEnvelope(method, paramsXml)
  // En WCF, SOAPAction debe coincidir con el operation action: http://tempuri.org/IConversorUnidadesWS/{Operation}
  const soapAction = `${SOAP_ACTION_PREFIX}${method}`
  const soapActionHeader = `"${soapAction}"`

  if (isBrowser) {
    const res = await fetch("/api/soap", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ body, soapAction: soapActionHeader }),
    })
    const text = await res.text()
    if (!res.ok) throw new Error(text || "Error HTTP en proxy SOAP")
    return text
  }

  if (!SOAP_ENDPOINT) throw new Error("Falta configurar NEXT_PUBLIC_API_URL con el endpoint SOAP")
  const res = await fetch(SOAP_ENDPOINT, {
    method: "POST",
    headers: { "Content-Type": "text/xml; charset=utf-8", SOAPAction: soapActionHeader },
    body,
  })
  const text = await res.text()
  if (!res.ok) throw new Error("Error HTTP en la comunicación SOAP")
  return text
}

function parseXml(xml: string): Document {
  // DOMParser solo está disponible en el navegador (los componentes que usan ApiClient son client-side)
  const parser = new DOMParser()
  const doc = parser.parseFromString(xml, "text/xml")
  // Buscar fault en el namespace SOAP
  const fault = (doc.getElementsByTagNameNS(SOAPENV, "Fault")[0]
    || doc.getElementsByTagName("Fault")[0])
  if (fault) {
    const faultString = (fault.getElementsByTagName("faultstring")[0]?.textContent
      || fault.getElementsByTagNameNS("*", "faultstring")[0]?.textContent
      || "Fault SOAP")
    throw new Error(faultString)
  }
  return doc
}

function extractFirstReturnNumber(xml: string, resultTag: string): number {
  const doc = parseXml(xml)
  // En WCF el resultado viene como <OperationResponse><OperationResult>...</OperationResult></OperationResponse>
  // Buscamos el primer tag con el nombre proporcionado
  const ret = (doc.getElementsByTagNameNS("*", resultTag)[0]
    || doc.getElementsByTagName(resultTag)[0])
  if (!ret || ret.textContent == null) throw new Error(`Respuesta SOAP inválida: sin <${resultTag}>`)
  const num = parseFloat(ret.textContent)
  if (Number.isNaN(num)) throw new Error(`Respuesta SOAP inválida: <${resultTag}> no numérico`)
  return num
}

function extractLoginResponse(xml: string): RespuestaAutenticacion {
  const doc = parseXml(xml)
  // WCF devuelve <LoginResponse><LoginResult><Exitoso/><Mensaje/><Username/></LoginResult></LoginResponse>
  const ret = (doc.getElementsByTagNameNS("*", "LoginResult")[0]
    || doc.getElementsByTagName("LoginResult")[0])
  if (!ret) return { exitoso: false, mensaje: "Respuesta SOAP inválida" }

  const get = (tag: string) => (ret.getElementsByTagNameNS("*", tag)[0]?.textContent
    || ret.getElementsByTagName(tag)[0]?.textContent
    || "")
  const exitosoStr = get("Exitoso").trim().toLowerCase()
  const exitoso = exitosoStr === "true"
  const mensaje = get("Mensaje")
  const username = get("Username")

  const data: RespuestaAutenticacion = { exitoso, mensaje }
  if (exitoso && username) {
    data.usuario = { username, password: "" }
  }
  return data
}

export class ApiClient {
  private static token: string | null = null

  static setToken(token: string) {
    this.token = token
    if (typeof window !== "undefined") {
      localStorage.setItem("auth_token", token)
    }
  }

  static getToken(): string | null {
    if (typeof window !== "undefined" && !this.token) {
      this.token = localStorage.getItem("auth_token")
    }
    return this.token
  }

  static clearToken() {
    this.token = null
    if (typeof window !== "undefined") {
      localStorage.removeItem("auth_token")
      localStorage.removeItem("username")
    }
  }

  // Autenticación contra SOAP: mon:login(username, password)
  static async login(username: string, password: string): Promise<RespuestaAutenticacion> {
    try {
      // Operación en WCF es Login con mayúscula
      const xml = await postSoap("Login", { username, password })
      const data = extractLoginResponse(xml)
      if (data.exitoso) {
        this.setToken(username)
      }
      return data
    } catch (error: any) {
      return {
        exitoso: false,
        mensaje: error?.message || "Error de conexión con el servidor",
      }
    }
  }

  // Conversiones de Temperatura
  static async convertirTemperatura(tipo: string, valor: number): Promise<number> {
    // Mapear tipo (UI) -> método SOAP y nombre de parámetro
    const map: Record<string, { method: string; param: string }> = {
      "celsius-fahrenheit": { method: "CelsiusAFahrenheit", param: "celsius" },
      "fahrenheit-celsius": { method: "FahrenheitACelsius", param: "fahrenheit" },
      "celsius-kelvin": { method: "CelsiusAKelvin", param: "celsius" },
      "kelvin-celsius": { method: "KelvinACelsius", param: "kelvin" },
      "fahrenheit-kelvin": { method: "FahrenheitAKelvin", param: "fahrenheit" },
      "kelvin-fahrenheit": { method: "KelvinAFahrenheit", param: "kelvin" },
    }
    const cfg = map[tipo]
    if (!cfg) throw new Error("Conversión de temperatura no soportada")

    const xml = await postSoap(cfg.method, { [cfg.param]: valor })
    return extractFirstReturnNumber(xml, `${cfg.method}Result`)
  }

  // Conversiones de Longitud
  static async convertirLongitud(tipo: string, valor: number): Promise<number> {
    const map: Record<string, { method: string; param: string }> = {
      "metro-kilometro": { method: "MetroAKilometro", param: "metros" },
      "kilometro-metro": { method: "KilometroAMetro", param: "kilometros" },
      "metro-milla": { method: "MetroAMilla", param: "metros" },
      "milla-metro": { method: "MillaAMetro", param: "millas" },
      "kilometro-milla": { method: "KilometroAMilla", param: "kilometros" },
      "milla-kilometro": { method: "MillaAKilometro", param: "millas" },
    }
    const cfg = map[tipo]
    if (!cfg) throw new Error("Conversión de longitud no soportada")

    const xml = await postSoap(cfg.method, { [cfg.param]: valor })
    return extractFirstReturnNumber(xml, `${cfg.method}Result`)
  }

  // Conversiones de Masa
  static async convertirMasa(tipo: string, valor: number): Promise<number> {
    const map: Record<string, { method: string; param: string }> = {
      "kilogramo-gramo": { method: "KilogramoAGramo", param: "kilogramos" },
      "gramo-kilogramo": { method: "GramoAKilogramo", param: "gramos" },
      "kilogramo-libra": { method: "KilogramoALibra", param: "kilogramos" },
      "libra-kilogramo": { method: "LibraAKilogramo", param: "libras" },
      "gramo-libra": { method: "GramoALibra", param: "gramos" },
      "libra-gramo": { method: "LibraAGramo", param: "libras" },
    }
    const cfg = map[tipo]
    if (!cfg) throw new Error("Conversión de masa no soportada")

    const xml = await postSoap(cfg.method, { [cfg.param]: valor })
    return extractFirstReturnNumber(xml, `${cfg.method}Result`)
  }
}
