// VIEW: Dashboard/Menú Principal

"use client"

import { useEffect } from "react"
import { useRouter } from "next/navigation"
import { useAuth } from "@/hooks/use-auth"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Thermometer, Ruler, Weight, LogOut, ArrowRight } from "lucide-react"
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
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-center">
          <div className="w-16 h-16 border-4 border-primary border-t-transparent rounded-full animate-spin mx-auto mb-4" />
          <p className="text-muted-foreground">Cargando...</p>
        </div>
      </div>
    )
  }

  const conversores = [
    {
      title: "Temperatura",
      description: "Convierte entre Celsius, Fahrenheit y Kelvin",
      icon: Thermometer,
      href: "/temperatura",
      color: "from-red-500 to-orange-500",
    },
    {
      title: "Longitud",
      description: "Convierte entre Metros, Kilómetros y Millas",
      icon: Ruler,
      href: "/longitud",
      color: "from-blue-500 to-cyan-500",
    },
    {
      title: "Masa",
      description: "Convierte entre Kilogramos, Gramos y Libras",
      icon: Weight,
      href: "/masa",
      color: "from-green-500 to-emerald-500",
    },
  ]

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-50 via-white to-blue-50 dark:from-slate-950 dark:via-slate-900 dark:to-slate-800">
      <header className="border-b border-slate-200 bg-white/80 dark:bg-slate-900/80 backdrop-blur-sm sticky top-0 z-10 shadow-sm">
        <div className="container mx-auto px-4 py-6 flex items-center justify-between">
          <div>
            <h1 className="text-3xl font-bold text-slate-900 dark:text-slate-100">Conversor de Unidades</h1>
            <p className="text-sm text-slate-600 dark:text-slate-400 mt-1">
              Bienvenido, <span className="font-semibold text-blue-600 dark:text-blue-400">{username}</span>
            </p>
          </div>
          <Button 
            variant="outline" 
            onClick={logout} 
            className="gap-2 bg-white dark:bg-slate-800 hover:bg-red-50 dark:hover:bg-red-900/20 border-slate-300 dark:border-slate-600 text-slate-700 dark:text-slate-300 hover:text-red-600 dark:hover:text-red-400 hover:border-red-300 dark:hover:border-red-600"
          >
            <LogOut className="w-4 h-4" />
            Cerrar Sesión
          </Button>
        </div>
      </header>

      <main className="container mx-auto px-4 py-12">
        <div className="max-w-6xl mx-auto">
          <div className="text-center mb-16">
            <h2 className="text-5xl font-bold mb-6 text-slate-900 dark:text-slate-100 text-balance">
              Selecciona un Tipo de Conversión
            </h2>
            <p className="text-xl text-slate-600 dark:text-slate-400 text-balance max-w-2xl mx-auto">
              Elige el conversor que necesitas para realizar tus cálculos de manera rápida y precisa
            </p>
          </div>

          <div className="grid lg:grid-cols-3 md:grid-cols-2 gap-8">
            {conversores.map((conversor) => {
              const Icon = conversor.icon
              return (
                <Link key={conversor.href} href={conversor.href}>
                  <Card className="h-full hover:shadow-2xl transition-all duration-300 hover:-translate-y-2 cursor-pointer group border-slate-200 dark:border-slate-700 bg-white dark:bg-slate-800/50 hover:bg-gradient-to-br hover:from-white hover:to-slate-50 dark:hover:from-slate-800 dark:hover:to-slate-700">
                    <CardHeader className="pb-4">
                      <div
                        className={`w-20 h-20 rounded-3xl bg-gradient-to-br ${conversor.color} flex items-center justify-center mb-6 group-hover:scale-110 transition-all duration-300 shadow-lg group-hover:shadow-xl`}
                      >
                        <Icon className="w-10 h-10 text-white drop-shadow-sm" />
                      </div>
                      <CardTitle className="text-2xl font-bold text-slate-900 dark:text-slate-100 mb-2">
                        {conversor.title}
                      </CardTitle>
                      <CardDescription className="text-lg text-slate-600 dark:text-slate-400 leading-relaxed">
                        {conversor.description}
                      </CardDescription>
                    </CardHeader>
                    <CardContent className="pt-0">
                      <div className="flex items-center justify-between">
                        <div className="flex items-center text-blue-600 dark:text-blue-400 font-semibold text-lg group-hover:gap-2 transition-all">
                          Abrir conversor
                          <ArrowRight className="w-5 h-5 ml-2 group-hover:translate-x-1 transition-transform" />
                        </div>
                        <div className="w-2 h-2 rounded-full bg-green-500 group-hover:bg-green-400 transition-colors shadow-sm"></div>
                      </div>
                    </CardContent>
                  </Card>
                </Link>
              )
            })}
          </div>

          {/* Sección adicional de información */}
          <div className="mt-16 text-center">
            <div className="bg-white dark:bg-slate-800 rounded-2xl p-8 shadow-lg border border-slate-200 dark:border-slate-700">
              <h3 className="text-2xl font-bold text-slate-900 dark:text-slate-100 mb-4">
                ¿Necesitas ayuda?
              </h3>
              <p className="text-slate-600 dark:text-slate-400 max-w-2xl mx-auto">
                Nuestros conversores están diseñados para ser precisos y fáciles de usar. 
                Selecciona cualquier categoria arriba para comenzar a convertir unidades al instante.
              </p>
            </div>
          </div>
        </div>
      </main>
    </div>
  )
}
