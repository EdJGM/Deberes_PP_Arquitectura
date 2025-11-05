"use client"

import { useState } from "react"
import { useRouter } from "next/navigation"
import { useAuth } from "@/hooks/use-auth"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Card, CardContent } from "@/components/ui/card"
import Image from "next/image"

export default function LoginPage() {
  const [username, setUsername] = useState("")
  const [password, setPassword] = useState("")
  const [error, setError] = useState("")
  const [isLoading, setIsLoading] = useState(false)
  const { login } = useAuth()
  const router = useRouter()

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setError("")

    if (!username.trim() || !password.trim()) {
      setError("Por favor ingrese usuario y contraseña")
      return
    }

    setIsLoading(true)

    const result = await login(username, password)

    if (result.exitoso) {
      setTimeout(() => {
        router.push("/dashboard")
      }, 100)
    } else {
      setError(result.mensaje || "Error de autenticación")
    }

    setIsLoading(false)
  }

  return (
    <div className="min-h-screen bg-bg-primary flex items-center justify-center p-8">
      <div className="w-full max-w-6xl">
        {/* Header */}
        <div className="text-center mb-6">
          <div className="text-6xl mb-4">🔐</div>
          <h1 className="text-4xl font-bold text-text-primary mb-2">
            CONVERSOR DE UNIDADES
          </h1>
          <p className="text-text-secondary text-lg">
            Sistema de Conversión - ESPE
          </p>
        </div>

        <div className="flex items-center justify-center gap-12">
          {/* Imagen - Izquierda */}
          <div className="hidden lg:block">
            <div className="w-full h-auto relative rounded-2xl overflow-hidden border-2 border-border-color">
              <Image
                src="/img.png"
                alt="ESPE Monster"
                width={600}
                height={600}
                className="object-cover"
                priority
              />
            </div>
          </div>

          {/* Formulario - Derecha */}
          <Card className="w-full max-w-md bg-bg-card border-2 border-border-color shadow-2xl">
            <CardContent className="p-8 space-y-6">
              {/* Usuario */}
              <div className="space-y-2">
                <label className="text-sm font-semibold text-text-secondary">
                  Usuario
                </label>
                <Input
                  type="text"
                  placeholder="Ingrese su usuario"
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                  className="h-12 bg-bg-secondary border-2 border-border-color text-text-primary 
                           placeholder:text-text-secondary focus:border-login focus:ring-login"
                />
              </div>

              {/* Contraseña */}
              <div className="space-y-2">
                <label className="text-sm font-semibold text-text-secondary">
                  Contraseña
                </label>
                <Input
                  type="password"
                  placeholder="Ingrese su contraseña"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  onKeyDown={(e) => e.key === 'Enter' && handleSubmit(e)}
                  className="h-12 bg-bg-secondary border-2 border-border-color text-text-primary 
                           placeholder:text-text-secondary focus:border-login focus:ring-login"
                />
              </div>

              {/* Error */}
              {error && (
                <div className="bg-error/10 border-2 border-error text-error px-4 py-3 rounded-lg text-sm">
                  {error}
                </div>
              )}

              {/* Botones */}
              <div className="space-y-3">
                <Button
                  onClick={handleSubmit}
                  disabled={isLoading}
                  className="w-full h-12 bg-login hover:bg-login-hover text-white font-bold 
                           text-base transition-colors"
                >
                  {isLoading ? "Iniciando sesión..." : "Iniciar Sesión"}
                </Button>

                <Button
                  type="button"
                  onClick={() => {
                    setUsername("")
                    setPassword("")
                    setError("")
                  }}
                  variant="outline"
                  className="w-full h-12 bg-bg-secondary border-2 border-border-color 
                           text-text-primary hover:bg-border-color font-semibold"
                >
                  Limpiar
                </Button>
              </div>
            </CardContent>
          </Card>
        </div>

        {/* Info Credenciales */}
        <div className="mt-8 max-w-2xl mx-auto">
          <div className="bg-bg-secondary border-2 border-border-color rounded-xl p-4 text-center">
            <p className="text-text-secondary text-sm">
              💡 <span className="font-semibold">Credenciales por defecto</span>
              <br />
              <span className="font-mono">Usuario: </span>
              <span className="font-mono font-bold text-text-primary">MONSTER</span>
              {" | "}
              <span className="font-mono">Contraseña: </span>
              <span className="font-mono font-bold text-text-primary">MONSTER9</span>
            </p>
          </div>
        </div>
      </div>
    </div>
  )
}