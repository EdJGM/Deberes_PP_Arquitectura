// MODEL: Cliente API para comunicarse con el backend RESTful

const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL 

export interface Usuario {
  username: string
  password: string
}

export interface RespuestaAutenticacion {
  exitoso: boolean  // Cambiar de 'exito' a 'exitoso' para coincidir con el backend
  mensaje: string
  usuario?: Usuario
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

  // Autenticación
  static async login(username: string, password: string): Promise<RespuestaAutenticacion> {
    try {
      const response = await fetch(`${API_BASE_URL}/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ username, password }),
      })

      const data = await response.json()

      if (response.ok && data.exitoso) {  // Cambiar de 'exito' a 'exitoso'
        this.setToken(username) // Usamos el username como token simple
        // No duplicar localStorage - se maneja en useAuth
      }

      return data
    } catch (error) {
      return {
        exitoso: false,  // Cambiar de 'exito' a 'exitoso'
        mensaje: "Error de conexión con el servidor",
      }
    }
  }

  // Conversiones de Temperatura
  static async convertirTemperatura(tipo: string, valor: number): Promise<number> {
    const response = await fetch(`${API_BASE_URL}/temperatura/${tipo}/${valor}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }

  // Conversiones de Longitud
  static async convertirLongitud(tipo: string, valor: number): Promise<number> {
    const response = await fetch(`${API_BASE_URL}/longitud/${tipo}/${valor}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }

  // Conversiones de Masa
  static async convertirMasa(tipo: string, valor: number): Promise<number> {
    const response = await fetch(`${API_BASE_URL}/masa/${tipo}/${valor}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }
}
