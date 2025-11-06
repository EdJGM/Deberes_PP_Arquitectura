"use client"

import { useState } from "react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { Card, CardContent } from "@/components/ui/card"

interface Unidad {
  value: string
  label: string
}

interface ConversorFormProps {
  unidades: Unidad[]
  onConvert: (from: string, to: string, value: number) => Promise<number>
  defaultFrom: string
  defaultTo: string
  resultColor: string
}

export function ConversorForm({
  unidades,
  onConvert,
  defaultFrom,
  defaultTo,
  resultColor
}: ConversorFormProps) {
  const [valor, setValor] = useState("")
  const [from, setFrom] = useState(defaultFrom)
  const [to, setTo] = useState(defaultTo)
  const [resultado, setResultado] = useState<number | null>(null)
  const [error, setError] = useState("")
  const [isLoading, setIsLoading] = useState(false)

  const handleConvert = async () => {
    setError("")

    if (!valor.trim()) {
      setError("Por favor ingrese un valor")
      return
    }

    const valorNumerico = parseFloat(valor)
    if (isNaN(valorNumerico)) {
      setError("Por favor ingrese un valor numérico válido")
      return
    }

    setIsLoading(true)

    try {
      const result = await onConvert(from, to, valorNumerico)
      setResultado(result)
    } catch (err) {
      setError("Error al realizar la conversión")
      console.error(err)
    } finally {
      setIsLoading(false)
    }
  }

  const handleLimpiar = () => {
    setValor("")
    setResultado(null)
    setError("")
    setFrom(defaultFrom)
    setTo(defaultTo)
  }

  const getUnidadLabel = (value: string) => {
    return unidades.find(u => u.value === value)?.label || ""
  }

  return (
    <Card className="bg-bg-card border-2 border-border-color">
      <CardContent className="p-8 space-y-6">
        {/* Valor */}
        <div className="space-y-2">
          <label className="text-sm font-bold text-text-secondary">
            Valor a convertir
          </label>
          <Input
            type="number"
            step="any"
            placeholder="Ingrese valor"
            value={valor}
            onChange={(e) => setValor(e.target.value)}
            className="h-12 bg-bg-secondary border-2 border-border-color text-text-primary text-base"
          />
        </div>

        {/* Unidad Origen */}
        <div className="space-y-2">
          <label className="text-sm font-bold text-text-secondary">
            Unidad de origen
          </label>
          <Select value={from} onValueChange={setFrom}>
            <SelectTrigger className="w-full h-12 bg-bg-secondary border-2 border-border-color text-text-primary">
              <SelectValue />
            </SelectTrigger>
            <SelectContent className="bg-bg-card border-2 border-border-color">
              {unidades.map((unidad) => (
                <SelectItem
                  key={unidad.value}
                  value={unidad.value}
                  className="text-text-primary hover:bg-bg-secondary"
                >
                  {unidad.label}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </div>

        {/* Flecha */}
        <div className="text-center">
          <div
            className="text-5xl"
            style={{ color: resultColor }}
          >
            ⬇️
          </div>
        </div>

        {/* Unidad Destino */}
        <div className="space-y-2">
          <label className="text-sm font-bold text-text-secondary">
            Unidad de destino
          </label>
          <Select value={to} onValueChange={setTo}>
            <SelectTrigger className="w-full h-12 bg-bg-secondary border-2 border-border-color text-text-primary">
              <SelectValue />
            </SelectTrigger>
            <SelectContent className="bg-bg-card border-2 border-border-color">
              {unidades.map((unidad) => (
                <SelectItem
                  key={unidad.value}
                  value={unidad.value}
                  className="text-text-primary hover:bg-bg-secondary"
                >
                  {unidad.label}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </div>

        {/* Separador */}
        <div className="border-t border-border-color" />

        {/* Resultado */}
        {resultado !== null && (
          <Card
            className="bg-bg-secondary border-2"
            style={{ borderColor: resultColor }}
          >
            <CardContent className="p-6 text-center">
              <p className="text-xs font-bold text-text-secondary mb-2">
                RESULTADO
              </p>
              <p
                className="text-4xl font-bold"
                style={{ color: resultColor }}
              >
                {resultado.toFixed(4)} {getUnidadLabel(to).match(/\(([^)]+)\)/)?.[1] || ""}
              </p>
            </CardContent>
          </Card>
        )}

        {/* Error */}
        {error && (
          <div className="bg-error/10 border-2 border-error text-error px-4 py-3 rounded-lg text-sm">
            {error}
          </div>
        )}

        {/* Botones */}
        <div className="flex gap-4">
          <Button
            onClick={handleConvert}
            disabled={isLoading}
            className="flex-1 h-12 text-white font-bold"
            style={{ backgroundColor: resultColor }}
          >
            {isLoading ? "Convirtiendo..." : "Convertir"}
          </Button>
          <Button
            onClick={handleLimpiar}
            variant="outline"
            className="flex-1 h-12 bg-bg-secondary border-2 border-border-color text-text-primary 
                     hover:bg-border-color font-semibold"
          >
            Limpiar
          </Button>

          {/* Botón Volver */}
          <Button
            onClick={() => window.history.back()}
            variant="outline"
            className="flex-1 h-12 bg-bg-secondary border-2 border-border-color text-text-primary 
                   hover:bg-border-color font-semibold"
          >
            ← Volver
          </Button>
        </div>
      </CardContent>
    </Card>
  )
}