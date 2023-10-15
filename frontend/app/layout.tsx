import "./globals.css"
import type { Metadata } from "next"
import { Inter } from "next/font/google"
import ClientOnly from "@/components/client-only"

const inter = Inter({ subsets: ["latin"] })

export const metadata: Metadata = {
  title: "KwikDeploy",
  description: "Simplified Container Deployments and Management",
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en">
      <body className={inter.className}>
        <ClientOnly>{children}</ClientOnly>
      </body>
    </html>
  )
}
