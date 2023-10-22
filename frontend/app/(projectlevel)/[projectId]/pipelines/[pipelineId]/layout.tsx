"use client"

import Loading from "@/components/loading"
import { SidebarNav } from "@/components/nav/sidebar-nav"
import { Separator } from "@/components/ui/separator"

import { useParams } from "next/navigation"
import usePipeline from "@/hooks/usePipeline"

const sidebarNavItems = [
  {
    title: "Settings",
    href: "/settings",
  },
]

interface PipelineLayoutProps {
  children: React.ReactNode
}

export default function PipelineLayout({ children }: PipelineLayoutProps) {
  const { projectId, pipelineId }: { projectId: string; pipelineId: string } =
    useParams()
  const { pipeline, isLoading } = usePipeline(projectId, pipelineId)

  if (isLoading) return <Loading />

  return (
    <>
      <div className="hidden space-y-6 p-10 pb-16 md:block">
        <div className="space-y-0.5">
          <h2 className="text-2xl font-bold tracking-tight">
            {pipeline?.name}
          </h2>
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
