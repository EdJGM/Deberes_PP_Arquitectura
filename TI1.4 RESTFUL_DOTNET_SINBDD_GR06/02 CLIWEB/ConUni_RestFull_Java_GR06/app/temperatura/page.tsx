// VIEW: Página de Conversión de Temperatura

"use client"

import { useEffect } from "react"
import { useRouter } from "next/navigation"
import { useAuth } from "@/hooks/use-auth"
import { ApiClient } from "@/lib/api-client"
import { ConversorLayout } from "@/components/conversor-layout"
import { ConversorForm } from "@/components/conversor-form"
import { Thermometer } from "lucide-react"

const unidades = [
  { value: "celsius", label: "Celsius (°C)" },
  { value: "fahrenheit", label: "Fahrenheit (°F)" },
  { value: "kelvin", label: "Kelvin (K)" },
]

const conversiones: Record<string, string> = {
  "celsius-fahrenheit": "celsius-fahrenheit",
  "fahrenheit-celsius": "fahrenheit-celsius",
  "celsius-kelvin": "celsius-kelvin",
  "kelvin-celsius": "kelvin-celsius",
  "fahrenheit-kelvin": "fahrenheit-kelvin",
  "kelvin-fahrenheit": "kelvin-fahrenheit",
}

export default function TemperaturaPage() {
  const { isAuthenticated, isLoading } = useAuth()
  const router = useRouter()

  useEffect(() => {
    if (!isLoading && !isAuthenticated) {
      router.push("/")
    }
  }, [isAuthenticated, isLoading, router])

  const handleConvert = async (from: string, to: string, value: number): Promise<number> => {
    const tipo = conversiones[`${from}-${to}`]
    if (!tipo) throw new Error("Conversión no soportada")
    return await ApiClient.convertirTemperatura(tipo, value)
  }

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

  return (
    <ConversorLayout
      title="Conversor de Temperatura"
      description="Convierte entre Celsius, Fahrenheit y Kelvin"
      icon={Thermometer}
      iconColor="from-red-500 to-orange-500"
    >
      <ConversorForm unidades={unidades} onConvert={handleConvert} defaultFrom="celsius" defaultTo="fahrenheit" />
    </ConversorLayout>
  )
}
