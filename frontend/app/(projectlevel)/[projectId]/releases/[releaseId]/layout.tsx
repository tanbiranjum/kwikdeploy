"use client"

import Loading from "@/components/loading"
import { SidebarNav } from "@/components/nav/sidebar-nav"
import { Separator } from "@/components/ui/separator"

import { useParams } from "next/navigation"
import useRelease from "@/hooks/useRelease"

const sidebarNavItems = [
  {
    title: "Settings",
    href: "/settings",
  },
]

interface ReleaseLayoutProps {
  children: React.ReactNode
}

export default function ReleaseLayout({ children }: ReleaseLayoutProps) {
  const { projectId, releaseId }: { projectId: string; releaseId: string } =
    useParams()
  const { release, isLoading } = useRelease(projectId, releaseId)

  if (isLoading) return <Loading />

  return (
    <>
      <div className="hidden space-y-6 p-10 pb-16 md:block">
        <div className="space-y-0.5">
          <h2 className="text-2xl font-bold tracking-tight">{release?.name}</h2>
        </div>
        <Separator className="my-6" />
        <div className="flex flex-col space-y-8 lg:flex-row lg:space-x-12 lg:space-y-0">
          <aside className="-mx-4 lg:w-1/5">
            <SidebarNav items={sidebarNavItems} />
          </aside>
          <div className="flex-1 lg:max-w-2xl">{children}</div>
        </div>
      </div>
    </>
  )
}
