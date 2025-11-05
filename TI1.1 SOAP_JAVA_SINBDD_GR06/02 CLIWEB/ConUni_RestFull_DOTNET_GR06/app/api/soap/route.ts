// Next.js API Route (app router) para proxyear llamadas SOAP y evitar CORS en el navegador

export async function POST(req: Request) {
  try {
    const { body } = (await req.json()) as { body?: string }
    if (!body) {
      return new Response("Falta body SOAP", { status: 400 })
    }

    const SOAP_ENDPOINT = process.env.SOAP_API_URL || process.env.NEXT_PUBLIC_API_URL
    if (!SOAP_ENDPOINT) {
      return new Response("Falta configurar SOAP_API_URL o NEXT_PUBLIC_API_URL", { status: 500 })
    }

    const res = await fetch(SOAP_ENDPOINT, {
      method: "POST",
      headers: { "Content-Type": "text/xml; charset=utf-8" },
      body,
      // Puedes agregar cabeceras extra aquí si tu servidor SOAP las requiere
    })

    const text = await res.text()
    if (!res.ok) {
      return new Response(text || "Error HTTP en backend SOAP", { status: res.status })
    }

    // Devolvemos el XML directamente para que el cliente lo parsee
    return new Response(text, {
      status: 200,
      headers: { "Content-Type": "text/xml; charset=utf-8" },
    })
  } catch (err: any) {
    return new Response(err?.message || "Error en proxy SOAP", { status: 500 })
  }
}
