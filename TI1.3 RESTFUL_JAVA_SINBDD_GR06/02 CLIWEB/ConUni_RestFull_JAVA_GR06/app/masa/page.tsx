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



export default function MasaPage() {
  const { isAuthenticated, isLoading } = useAuth()
  const router = useRouter()

  useEffect(() => {
    if (!isLoading && !isAuthenticated) {
      router.push("/")
    }
  }, [isAuthenticated, isLoading, router])

  const handleConvert = async (from: string, to: string, value: number) => {
  if (from === "kilogramo" && to === "gramo") {
    return await ApiClient.kilogramoAGramo(value)
  } else if (from === "gramo" && to === "kilogramo") {
    return await ApiClient.gramoAKilogramo(value)
  }
  // ... 6 conversiones
  else if (from === "kilogramo" && to === "libra") {
    return await ApiClient.kilogramoALibra(value)
  } else if (from === "libra" && to === "kilogramo") {
    return await ApiClient.libraAKilogramo(value)
  } else if (from === "gramo" && to === "libra") {
    return await ApiClient.gramoALibra(value)
  } else if (from === "libra" && to === "gramo") {
    return await ApiClient.libraAGramo(value)
  } else {
    throw new Error("Conversión no soportada")
  }
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