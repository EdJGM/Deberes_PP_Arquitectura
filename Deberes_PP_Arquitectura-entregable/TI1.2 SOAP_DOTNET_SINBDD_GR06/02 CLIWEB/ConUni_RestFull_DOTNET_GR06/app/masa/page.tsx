"use client"

import { useEffect } from "react"
import { useRouter } from "next/navigation"
import { useAuth } from "@/hooks/use-auth"
import { ApiClient } from "@/lib/api-client"
import { ConversorLayout } from "@/components/conversor-layout"
import { ConversorForm } from "@/components/conversor-form"

const unidades = [
  { value: "kilogramo", label: "Kilogramo (kg)" },
  { value: "gramo", label: "Gramo (g)" },
  { value: "libra", label: "Libra (lb)" },
]

const conversiones: Record<string, string> = {
  "kilogramo-gramo": "kilogramo-gramo",
  "gramo-kilogramo": "gramo-kilogramo",
  "kilogramo-libra": "kilogramo-libra",
  "libra-kilogramo": "libra-kilogramo",
  "gramo-libra": "gramo-libra",
  "libra-gramo": "libra-gramo",
}

export default function MasaPage() {
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
    return await ApiClient.convertirMasa(tipo, value)
  }

  if (isLoading || !isAuthenticated) {
    return (
      <div className="min-h-screen bg-bg-primary flex items-center justify-center">
        <div className="text-center">
          <div className="w-16 h-16 border-4 border-masa border-t-transparent rounded-full animate-spin mx-auto mb-4" />
          <p className="text-text-secondary">Cargando...</p>
        </div>
      </div>
    )
  }

  return (
    <ConversorLayout
      emoji="⚖️"
      title="CONVERSIÓN DE MASA"
      description="Kilogramos, Gramos, Libras"
      headerColor="#10b981"
    >
      <ConversorForm
        unidades={unidades}
        onConvert={handleConvert}
        defaultFrom="kilogramo"
        defaultTo="gramo"
        resultColor="#10b981"
      />
    </ConversorLayout>
  )
}