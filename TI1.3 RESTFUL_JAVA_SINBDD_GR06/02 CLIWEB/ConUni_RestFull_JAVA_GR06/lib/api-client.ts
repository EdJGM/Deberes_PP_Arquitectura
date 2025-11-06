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

   // 18 métodos específicos (6 por categoría)
  
  // TEMPERATURA (6 métodos)
  static async celsiusAFahrenheit(celsius: number): Promise<number> {
    const response = await fetch(`${API_BASE_URL}/celsiusAFahrenheit/${celsius}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }

  static async fahrenheitACelsius(fahrenheit: number): Promise<number> {
    const response = await fetch(`${API_BASE_URL}/fahrenheitACelsius/${fahrenheit}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }

  static async celsiusAKelvin(celsius: number): Promise<number> {
    const response = await fetch(`${API_BASE_URL}/celsiusAKelvin/${celsius}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }

  static async kelvinACelsius(kelvin: number): Promise<number> {
    const response = await fetch(`${API_BASE_URL}/kelvinACelsius/${kelvin}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }

  static async fahrenheitAKelvin(fahrenheit: number): Promise<number> {
    const response = await fetch(`${API_BASE_URL}/fahrenheitAKelvin/${fahrenheit}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }

  static async kelvinAFahrenheit(kelvin: number): Promise<number> {
    const response = await fetch(`${API_BASE_URL}/kelvinAFahrenheit/${kelvin}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }
  // LONGITUD (6 métodos)
   static async metroAKilometro(metros: number): Promise<number> { 
    const response = await fetch(`${API_BASE_URL}/metroAKilometro/${metros}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
   }
  static async kilometroAMetro(kilometros: number): Promise<number> { 
    const response = await fetch(`${API_BASE_URL}/kilometroAMetro/${kilometros}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
   }
  static async metroAMilla(metros: number): Promise<number> { 
    const response = await fetch(`${API_BASE_URL}/metroAMilla/${metros}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
   }
  static async millaAMetro(millas: number): Promise<number> { 
    const response = await fetch(`${API_BASE_URL}/millaAMetro/${millas}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()

  }
  static async kilometroAMilla(kilometros: number): Promise<number> { 
    const response = await fetch(`${API_BASE_URL}/kilometroAMilla/${kilometros}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }
  static async millaAKilometro(millas: number): Promise<number> { 
    const response = await fetch(`${API_BASE_URL}/millaAKilometro/${millas}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }

    // MASA (6 métodos)
  static async kilogramoAGramo(kilogramos: number): Promise<number> { 
    const response = await fetch(`${API_BASE_URL}/kilogramoAGramo/${kilogramos}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
   }
  static async gramoAKilogramo(gramos: number): Promise<number> { 
    const response = await fetch(`${API_BASE_URL}/gramoAKilogramo/${gramos}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
   }
  static async kilogramoALibra(kilogramos: number): Promise<number> { 
    const response = await fetch(`${API_BASE_URL}/kilogramoALibra/${kilogramos}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }
  static async libraAKilogramo(libras: number): Promise<number> { 
    const response = await fetch(`${API_BASE_URL}/libraAKilogramo/${libras}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }
  static async gramoALibra(gramos: number): Promise<number> { 
    const response = await fetch(`${API_BASE_URL}/gramoALibra/${gramos}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
   }
  static async libraAGramo(libras: number): Promise<number> { 
    const response = await fetch(`${API_BASE_URL}/libraAGramo/${libras}`)
    if (!response.ok) throw new Error("Error en la conversión")
    return await response.json()
  }
}
