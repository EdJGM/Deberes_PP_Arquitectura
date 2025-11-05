"use client"

import { useEffect } from "react"
import { useRouter } from "next/navigation"
import { useAuth } from "@/hooks/use-auth"
import { Button } from "@/components/ui/button"
import { Card } from "@/components/ui/card"
import Link from "next/link"

export default function DashboardPage() {
  const { isAuthenticated, username, isLoading, logout } = useAuth()
  const router = useRouter()

  useEffect(() => {
    if (!isLoading && !isAuthenticated) {
      router.push("/")
    }
  }, [isAuthenticated, isLoading, router])

  if (isLoading || !isAuthenticated) {
    return (
      <div className="min-h-screen bg-bg-primary flex items-center justify-center">
        <div className="text-center">
          <div className="w-16 h-16 border-4 border-login border-t-transparent rounded-full animate-spin mx-auto mb-4" />
          <p className="text-text-secondary">Cargando...</p>
        </div>
      </div>
    )
  }

  const conversores = [
    {
      emoji: "🌡️",
      title: "TEMPERATURA",
      description: "Celsius, Fahrenheit, Kelvin",
      href: "/temperatura",
      color: "temperatura",
      bgGradient: "from-temperatura to-temperatura-hover"
    },
    {
      emoji: "📏",
      title: "LONGITUD",
      description: "Metros, Kilómetros, Millas",
      href: "/longitud",
      color: "longitud",
      bgGradient: "from-longitud to-longitud-hover"
    },
    {
      emoji: "⚖️",
      title: "MASA",
      description: "Kilogramos, Gramos, Libras",
      href: "/masa",
      color: "masa",
      bgGradient: "from-masa to-masa-hover"
    },
  ]

  return (
    <div className="min-h-screen bg-bg-primary">
      {/* Header */}
      <header className="bg-bg-secondary border-b border-border-color">
        <div className="container mx-auto px-6 py-6 flex items-center justify-between">
          <div>
            <h1 className="text-3xl font-bold text-text-primary">
              CONVERSOR DE UNIDADES
            </h1>
            <p className="text-text-secondary mt-1">
              Seleccione el tipo de conversión
            </p>
          </div>
          <div className="flex items-center gap-4">
            <span className="text-text-primary font-bold">
              👤 {username}
            </span>
            <Button
              onClick={logout}
              className="bg-error hover:bg-red-700 text-white font-bold"
            >
              🚪 Cerrar Sesión
            </Button>
          </div>
        </div>
      </header>

      {/* Content */}
      <main className="container mx-auto px-6 py-12">
        <div className="max-w-7xl mx-auto">
          {/* Grid de Categorías */}
          <div className="grid lg:grid-cols-3 gap-6 mb-6">
            {conversores.map((conversor) => (
              <Link key={conversor.href} href={conversor.href}>
                <Card
                  className={`min-h-[30rem] h-full bg-bg-card border-2 border-border-color 
                            hover:border-${conversor.color} transition-all duration-300 
                            hover:shadow-2xl hover:scale-[1.02] cursor-pointer group`}
                >
                  <div className="flex flex-col justify-between h-full p-12">
                    {/* Icono */}
                    <div className="text-center mb-6">
                      <div className="text-7xl mb-4 group-hover:scale-110 transition-transform">
                        {conversor.emoji}
                      </div>
                    </div>

                    {/* Contenido */}
                    <div className="text-center space-y-2">
                      <h2 className="text-2xl font-bold text-text-primary">
                        {conversor.title}
                      </h2>
                      <p className="text-text-secondary text-sm">
                        {conversor.description}
                      </p>
                    </div>

                    {/* Barra de color */}
                    <div
                      className={`mt-6 h-1 rounded-full bg-gradient-to-r ${conversor.bgGradient}`}
                    />
                  </div>
                </Card>
              </Link>
            ))}
          </div>
        </div>
      </main>
    </div>
  )
}