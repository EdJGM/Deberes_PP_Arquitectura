"use client"

import { useEffect } from "react"
import { useRouter } from "next/navigation"
import { useAuth } from "@/hooks/use-auth"
import { ApiClient } from "@/lib/api-client"
import { ConversorLayout } from "@/components/conversor-layout"
import { ConversorForm } from "@/components/conversor-form"

const unidades = [
  { value: "metro", label: "Metro (m)" },
  { value: "kilometro", label: "Kilómetro (km)" },
  { value: "milla", label: "Milla (mi)" },
]

const conversiones: Record<string, string> = {
  "metro-kilometro": "metro-kilometro",
  "kilometro-metro": "kilometro-metro",
  "metro-milla": "metro-milla",
  "milla-metro": "milla-metro",
  "kilometro-milla": "kilometro-milla",
  "milla-kilometro": "milla-kilometro",
}

export default function LongitudPage() {
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
    return await ApiClient.convertirLongitud(tipo, value)
  }

  if (isLoading || !isAuthenticated) {
    return (
      <div className="min-h-screen bg-bg-primary flex items-center justify-center">
        <div className="text-center">
          <div className="w-16 h-16 border-4 border-longitud border-t-transparent rounded-full animate-spin mx-auto mb-4" />
          <p className="text-text-secondary">Cargando...</p>
        </div>
      </div>
    )
  }

  return (
    <ConversorLayout
      emoji="📏"
      title="CONVERSIÓN DE LONGITUD"
      description="Metros, Kilómetros, Millas"
      headerColor="#3b82f6"
    >
      <ConversorForm
        unidades={unidades}
        onConvert={handleConvert}
        defaultFrom="metro"
        defaultTo="kilometro"
        resultColor="#3b82f6"
      />
    </ConversorLayout>
  )
}