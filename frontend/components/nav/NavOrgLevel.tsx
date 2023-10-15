"use client"

import Link from "next/link"
import { usePathname } from "next/navigation"
import { cn } from "@/lib/utils"
import { UserNav } from "@/components/user-nav"

const navItems = [
  { name: "Projects", href: "/projects" },
  { name: "Settings", href: "/settings" },
  { name: "Sample CRUD", href: "/samplecrud" },
]

export default function NavOrgLevel() {
  const currentRoute = usePathname()

  return (
    <div className="border-b">
      <div className="flex h-16 items-center px-4">
        <nav className={cn("flex items-center space-x-4 lg:space-x-6")}>
          {navItems.map((navItem, i) => (
            <Link
              key={i}
              className={
                currentRoute === navItem.href
                  ? "text-sm font-bold transition-colors hover:text-primary"
                  : "text-sm font-medium transition-colors hover:text-primary"
              }
              href={navItem.href}
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
