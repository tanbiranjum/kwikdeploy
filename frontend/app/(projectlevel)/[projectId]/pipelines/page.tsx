"use client"

import React from "react"
import MainContainer from "@/components/main-container"
import { DataTable } from "@/components/ui/data-table"
import { columns } from "./table/columns"
import AddPipelineDialog from "./dialogs/add-pipeline-dialog"
import { useParams, useRouter } from "next/navigation"
import Loading from "@/components/loading"
import usePipelines from "@/hooks/usePipelines"

export default function PipelinesPage() {
  const { projectId }: { projectId: string } = useParams()
  const { isLoading, pipelines } = usePipelines(projectId)
  const router = useRouter()

  if (isLoading) return <Loading />

  if (!pipelines?.items) return null

  return (
    <MainContainer props={{ className: "" }}>
      <AddPipelineDialog />
      <DataTable
        columns={columns}
        data={pipelines.items}
        onRowClick={(id) =>
          router.push(`/${projectId}/pipelines/${id}/settings`)
        }
      />
    </MainContainer>
  )
}
