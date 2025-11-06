// CONTROLLER: Hook personalizado para manejar la autenticación

"use client"

import { useState, useEffect } from "react"
import { ApiClient } from "@/lib/api-client"

export function useAuth() {
  const [isAuthenticated, setIsAuthenticated] = useState(false)
  const [username, setUsername] = useState<string | null>(null)
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    const token = ApiClient.getToken()
    const storedUsername = typeof window !== "undefined" ? localStorage.getItem("username") : null

    if (token && storedUsername) {
      setIsAuthenticated(true)
      setUsername(storedUsername)
    }
    setIsLoading(false)
  }, [])

  const login = async (username: string, password: string) => {
    const result = await ApiClient.login(username, password)
    if (result.exitoso) {  // Cambiar de 'exito' a 'exitoso'
      setIsAuthenticated(true)
      setUsername(username)
      if (typeof window !== "undefined") {
        localStorage.setItem("username", username)
      }
    }
    return result
  }

  const logout = () => {
    ApiClient.clearToken()
    setIsAuthenticated(false)
    setUsername(null)
    if (typeof window !== "undefined") {
      localStorage.removeItem("username")
    }
  }

  return { isAuthenticated, username, isLoading, login, logout }
}
