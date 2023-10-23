"use client"

import Link from "next/link"
import { usePathname } from "next/navigation"
import { cn } from "@/lib/utils"
import { UserNav } from "@/components/user-nav"

const navItems = [
  { name: "Targets", href: "/targets" },
  { name: "Environments", href: "/environments" },
  { name: "Variables", href: "/variables" },
  { name: "App Definitions", href: "/appdefinitions" },
  { name: "Releases", href: "/releases" },
  { name: "Settings", href: "/settings" },
]

export default function NavProjectLevel({
  params,
}: {
  params: { projectId: number }
}) {
  const currentRoute = usePathname()

  return (
    <div className="border-b bg-primary text-gray-200">
      <div className="flex h-16 items-center px-4">
        <nav className={cn("flex items-center space-x-4 lg:space-x-6")}>
          <Link
            className="text-sm font-medium transition-colors"
            href={`/projects`}
          >
            &lt;- Home
          </Link>
          {navItems.map((navItem, i) => (
            <Link
              key={i}
              className={
                currentRoute.match(new RegExp(`^/[\\d]+${navItem.href}`))
                  ? "bg-nav rounded-md bg-blue-500 px-3 py-2 text-sm font-medium text-white"
                  : "rounded-md px-3 py-2 text-sm font-medium text-white hover:bg-blue-500 hover:bg-opacity-75"
              }
              href={`/${params.projectId}${navItem.href}`}
            >
              {navItem.name}
            </Link>
          ))}
        </nav>
        <div className="ml-auto flex items-center space-x-4">
          <UserNav />
        </div>
      </div>
    </div>
  )
}
