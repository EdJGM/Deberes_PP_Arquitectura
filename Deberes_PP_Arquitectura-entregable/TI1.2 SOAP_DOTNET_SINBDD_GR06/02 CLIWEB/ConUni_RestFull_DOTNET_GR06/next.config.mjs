/** @type {import('next').NextConfig} */
const nextConfig = {
  typescript: {
    ignoreBuildErrors: true,
  },
  images: {
    unoptimized: true,
  },
  // Eliminar logs de desarrollo y telemetría
  reactStrictMode: false,
  logging: {
    fetches: {
      fullUrl: false,
    },
  },
}

export default nextConfig
