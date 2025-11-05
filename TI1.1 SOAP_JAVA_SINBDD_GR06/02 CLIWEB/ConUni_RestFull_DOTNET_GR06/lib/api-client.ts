// MODEL: Cliente SOAP para comunicarse con el servicio ConversorUnidadesWS

// Apunta esta URL al endpoint SOAP sin "?wsdl", por ejemplo:
// NEXT_PUBLIC_API_URL=http://localhost:8080/<context>/ConversorUnidadesWS
const SOAP_ENDPOINT = process.env.NEXT_PUBLIC_API_URL
const SOAPENV = "http://schemas.xmlsoap.org/soap/envelope/"
const NS = "http://ws.monster.edu.ec/" // Namespace del servicio según README

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
    .map(([k, v]) => `<${k}>${typeof v === "number" ? v : xmlEscape(v)}</${k}>`)
    .join("")
  const body = buildSoapEnvelope(method, paramsXml)

  if (isBrowser) {
    const res = await fetch("/api/soap", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ body }),
    })
    const text = await res.text()
    if (!res.ok) throw new Error(text || "Error HTTP en proxy SOAP")
    return text
  }

  if (!SOAP_ENDPOINT) throw new Error("Falta configurar NEXT_PUBLIC_API_URL con el endpoint SOAP")
  const res = await fetch(SOAP_ENDPOINT, {
    method: "POST",
    headers: { "Content-Type": "text/xml; charset=utf-8" },
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
  const fault = doc.getElementsByTagName("Fault")[0]
  if (fault) {
    const faultString = fault.getElementsByTagName("faultstring")[0]?.textContent || "Fault SOAP"
    throw new Error(faultString)
  }
  return doc
}

function extractFirstReturnNumber(xml: string): number {
  const doc = parseXml(xml)
  const ret = doc.getElementsByTagName("return")[0]
  if (!ret || ret.textContent == null) throw new Error("Respuesta SOAP inválida: sin <return>")
  const num = parseFloat(ret.textContent)
  if (Number.isNaN(num)) throw new Error("Respuesta SOAP inválida: <return> no numérico")
  return num
}

function extractLoginResponse(xml: string): RespuestaAutenticacion {
  const doc = parseXml(xml)
  const ret = doc.getElementsByTagName("return")[0]
  if (!ret) return { exitoso: false, mensaje: "Respuesta SOAP inválida" }

  const get = (tag: string) => ret.getElementsByTagName(tag)[0]?.textContent || ""
  const exitosoStr = get("exitoso").trim().toLowerCase()
  const exitoso = exitosoStr === "true"
  const mensaje = get("mensaje")
  const username = get("username")

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
      const xml = await postSoap("login", { username, password })
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
      "celsius-fahrenheit": { method: "celsiusAFahrenheit", param: "celsius" },
      "fahrenheit-celsius": { method: "fahrenheitACelsius", param: "fahrenheit" },
      "celsius-kelvin": { method: "celsiusAKelvin", param: "celsius" },
      "kelvin-celsius": { method: "kelvinACelsius", param: "kelvin" },
      "fahrenheit-kelvin": { method: "fahrenheitAKelvin", param: "fahrenheit" },
      "kelvin-fahrenheit": { method: "kelvinAFahrenheit", param: "kelvin" },
    }
    const cfg = map[tipo]
    if (!cfg) throw new Error("Conversión de temperatura no soportada")

    const xml = await postSoap(cfg.method, { [cfg.param]: valor })
    return extractFirstReturnNumber(xml)
  }

  // Conversiones de Longitud
  static async convertirLongitud(tipo: string, valor: number): Promise<number> {
    const map: Record<string, { method: string; param: string }> = {
      "metro-kilometro": { method: "metroAKilometro", param: "metros" },
      "kilometro-metro": { method: "kilometroAMetro", param: "kilometros" },
      "metro-milla": { method: "metroAMilla", param: "metros" },
      "milla-metro": { method: "millaAMetro", param: "millas" },
      "kilometro-milla": { method: "kilometroAMilla", param: "kilometros" },
      "milla-kilometro": { method: "millaAKilometro", param: "millas" },
    }
    const cfg = map[tipo]
    if (!cfg) throw new Error("Conversión de longitud no soportada")

    const xml = await postSoap(cfg.method, { [cfg.param]: valor })
    return extractFirstReturnNumber(xml)
  }

  // Conversiones de Masa
  static async convertirMasa(tipo: string, valor: number): Promise<number> {
    const map: Record<string, { method: string; param: string }> = {
      "kilogramo-gramo": { method: "kilogramoAGramo", param: "kilogramos" },
      "gramo-kilogramo": { method: "gramoAKilogramo", param: "gramos" },
      "kilogramo-libra": { method: "kilogramoALibra", param: "kilogramos" },
      "libra-kilogramo": { method: "libraAKilogramo", param: "libras" },
      "gramo-libra": { method: "gramoALibra", param: "gramos" },
      "libra-gramo": { method: "libraAGramo", param: "libras" },
    }
    const cfg = map[tipo]
    if (!cfg) throw new Error("Conversión de masa no soportada")

    const xml = await postSoap(cfg.method, { [cfg.param]: valor })
    return extractFirstReturnNumber(xml)
  }
}
