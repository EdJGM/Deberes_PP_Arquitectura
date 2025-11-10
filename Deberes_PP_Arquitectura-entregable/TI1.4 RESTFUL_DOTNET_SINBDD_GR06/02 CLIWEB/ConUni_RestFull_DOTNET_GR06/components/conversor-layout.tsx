"use client"

import { ReactNode } from "react"
import { useRouter } from "next/navigation"
import { useAuth } from "@/hooks/use-auth"
import { Button } from "@/components/ui/button"

interface ConversorLayoutProps {
  emoji: string
  title: string
  description: string
  headerColor: string
  children: ReactNode
}

export function ConversorLayout({
  emoji,
  title,
  description,
  headerColor,
  children
}: ConversorLayoutProps) {
  const router = useRouter()
  const { username, logout } = useAuth()

  return (
    <div className="min-h-screen bg-bg-primary">
      {/* Header con color de categoría */}
      <header
        className={`${headerColor} border-b border-border-color`}
        style={{
          backgroundColor: headerColor.includes('bg-') ? undefined : headerColor
        }}
      >
        <div className="container mx-auto px-6 py-6 flex items-center justify-between">
          <div className="flex items-center gap-6">
            <Button
              onClick={() => router.push("/dashboard")}
              variant="ghost"
              className="text-white hover:bg-white/10 h-10 w-10 p-0"
            >
              ←
            </Button>
            <div className="flex items-center gap-4">
              <span className="text-4xl">{emoji}</span>
              <div>
                <h1 className="text-2xl font-bold text-white">
                  {title}
                </h1>
                <p className="text-white/70 text-sm">
                  {description}
                </p>
              </div>
            </div>
          </div>
          <div className="flex items-center gap-4">
            <span className="text-white font-bold">
              👤 {username}
            </span>
            <Button
              onClick={logout}
              variant="outline"
              className="bg-white/10 hover:bg-white/20 border-white/20 text-white"
            >
              🚪 Salir
            </Button>
          </div>
        </div>
      </header>

      {/* Content */}
      <main className="container mx-auto px-6 py-12">
        <div className="max-w-2xl mx-auto">
          {children}
        </div>
      </main>
    </div>
  )
}