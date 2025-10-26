// VIEW: Layout compartido para las páginas de conversión

"use client"

import type { ReactNode } from "react"
import { useRouter } from "next/navigation"
import { useAuth } from "@/hooks/use-auth"
import { Button } from "@/components/ui/button"
import { ArrowLeft, LogOut, type LucideIcon } from "lucide-react"

interface ConversorLayoutProps {
  title: string
  description: string
  icon: LucideIcon
  iconColor: string
  children: ReactNode
}

export function ConversorLayout({ title, description, icon: Icon, iconColor, children }: ConversorLayoutProps) {
  const router = useRouter()
  const { username, logout } = useAuth()

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-50 via-white to-blue-50 dark:from-slate-950 dark:via-slate-900 dark:to-slate-800">
      <header className="border-b border-slate-200 bg-white/80 dark:bg-slate-900/80 backdrop-blur-sm sticky top-0 z-10 shadow-sm">
        <div className="container mx-auto px-4 py-4 flex items-center justify-between">
          <div className="flex items-center gap-4">
            <Button 
              variant="ghost" 
              size="icon" 
              onClick={() => router.push("/dashboard")}
              className="hover:bg-slate-100 dark:hover:bg-slate-800 text-slate-600 dark:text-slate-400 hover:text-slate-900 dark:hover:text-slate-100"
            >
              <ArrowLeft className="w-5 h-5" />
            </Button>
            <div>
              <h1 className="text-2xl font-bold text-slate-900 dark:text-slate-100">{title}</h1>
              <p className="text-sm text-slate-600 dark:text-slate-400">{username}</p>
            </div>
          </div>
          <Button 
            variant="outline" 
            onClick={logout} 
            className="gap-2 bg-white dark:bg-slate-800 hover:bg-red-50 dark:hover:bg-red-900/20 border-slate-300 dark:border-slate-600 text-slate-700 dark:text-slate-300 hover:text-red-600 dark:hover:text-red-400 hover:border-red-300 dark:hover:border-red-600"
          >
            <LogOut className="w-4 h-4" />
            Salir
          </Button>
        </div>
      </header>

      <main className="container mx-auto px-4 py-12">
        <div className="max-w-3xl mx-auto">
          <div className="text-center mb-12">
            <div
              className={`w-24 h-24 rounded-3xl bg-gradient-to-br ${iconColor} flex items-center justify-center mx-auto mb-6 shadow-xl`}
            >
              <Icon className="w-12 h-12 text-white drop-shadow-sm" />
            </div>
            <h2 className="text-4xl font-bold mb-4 text-slate-900 dark:text-slate-100 text-balance">{title}</h2>
            <p className="text-lg text-slate-600 dark:text-slate-400 text-balance max-w-2xl mx-auto">{description}</p>
          </div>

          <div className="bg-white dark:bg-slate-800 rounded-2xl shadow-xl border border-slate-200 dark:border-slate-700 p-8">
            {children}
          </div>
        </div>
      </main>
    </div>
  )
}
