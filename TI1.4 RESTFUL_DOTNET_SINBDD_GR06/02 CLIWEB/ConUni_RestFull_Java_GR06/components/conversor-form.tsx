// VIEW: Formulario reutilizable para conversiones

"use client"

import { useState } from "react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Card, CardContent } from "@/components/ui/card"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { ArrowRight, ArrowLeftRight } from "lucide-react"

interface Unidad {
  value: string
  label: string
}

interface ConversorFormProps {
  unidades: Unidad[]
  onConvert: (from: string, to: string, value: number) => Promise<number>
  defaultFrom: string
  defaultTo: string
}

export function ConversorForm({ unidades, onConvert, defaultFrom, defaultTo }: ConversorFormProps) {
  const [valorEntrada, setValorEntrada] = useState("")
  const [valorSalida, setValorSalida] = useState("")
  const [unidadOrigen, setUnidadOrigen] = useState(defaultFrom)
  const [unidadDestino, setUnidadDestino] = useState(defaultTo)
  const [isLoading, setIsLoading] = useState(false)
  const [error, setError] = useState("")

  const handleConvert = async () => {
    if (!valorEntrada || isNaN(Number(valorEntrada))) {
      setError("Por favor ingresa un valor numérico válido")
      return
    }

    if (unidadOrigen === unidadDestino) {
      setValorSalida(valorEntrada)
      setError("")
      return
    }

    setIsLoading(true)
    setError("")

    try {
      const resultado = await onConvert(unidadOrigen, unidadDestino, Number(valorEntrada))
      setValorSalida(resultado.toFixed(4))
    } catch (err) {
      setError("Error al realizar la conversión. Verifica tu conexión.")
      setValorSalida("")
    } finally {
      setIsLoading(false)
    }
  }

  const handleSwap = () => {
    setUnidadOrigen(unidadDestino)
    setUnidadDestino(unidadOrigen)
    setValorEntrada(valorSalida)
    setValorSalida(valorEntrada)
  }

  return (
    <Card className="shadow-xl border-border/50">
      <CardContent className="p-6 space-y-6">
        {/* Unidad de Origen */}
        <div className="space-y-2">
          <label className="text-sm font-medium text-foreground">Desde</label>
          <Select value={unidadOrigen} onValueChange={setUnidadOrigen}>
            <SelectTrigger className="h-12">
              <SelectValue />
            </SelectTrigger>
            <SelectContent>
              {unidades.map((unidad) => (
                <SelectItem key={unidad.value} value={unidad.value}>
                  {unidad.label}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
          <Input
            type="number"
            placeholder="Ingresa el valor"
            value={valorEntrada}
            onChange={(e) => setValorEntrada(e.target.value)}
            className="h-14 text-lg"
            step="any"
          />
        </div>

        {/* Botón de Intercambio */}
        <div className="flex justify-center">
          <Button
            type="button"
            variant="outline"
            size="icon"
            onClick={handleSwap}
            className="rounded-full w-12 h-12 hover:rotate-180 transition-transform duration-300 bg-transparent"
          >
            <ArrowLeftRight className="w-5 h-5" />
          </Button>
        </div>

        {/* Unidad de Destino */}
        <div className="space-y-2">
          <label className="text-sm font-medium text-foreground">Hasta</label>
          <Select value={unidadDestino} onValueChange={setUnidadDestino}>
            <SelectTrigger className="h-12">
              <SelectValue />
            </SelectTrigger>
            <SelectContent>
              {unidades.map((unidad) => (
                <SelectItem key={unidad.value} value={unidad.value}>
                  {unidad.label}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
          <div className="h-14 bg-muted rounded-lg flex items-center px-4 text-lg font-semibold text-foreground">
            {valorSalida || "0"}
          </div>
        </div>

        {error && (
          <div className="bg-destructive/10 text-destructive text-sm p-3 rounded-lg border border-destructive/20">
            {error}
          </div>
        )}

        <Button onClick={handleConvert} className="w-full h-12 text-base font-semibold gap-2" disabled={isLoading}>
          {isLoading ? (
            "Convirtiendo..."
          ) : (
            <>
              Convertir
              <ArrowRight className="w-4 h-4" />
            </>
          )}
        </Button>
      </CardContent>
    </Card>
  )
}
