"use client"

import React from "react"
import MainContainer from "@/components/main-container"
import { DataTable } from "@/components/ui/data-table"
import { columns } from "./table/columns"
import AddEnvironmentDialog from "./dialogs/add-environment-dialog"
import { useParams, useRouter } from "next/navigation"
import Loading from "@/components/loading"
import useEnvs from "@/hooks/useEnvs"

export default function EnvironmentsPage() {
  const { projectId }: { projectId: string } = useParams()
  const { isLoading, envs } = useEnvs(projectId)
  const router = useRouter()

  if (isLoading) return <Loading />

  if (!envs?.items) return null

  return (
    <MainContainer props={{ className: "" }}>
      <AddEnvironmentDialog />
      <DataTable
        columns={columns}
        data={envs.items}
        onRowClick={(id) =>
          router.push(`/${projectId}/environments/${id}/settings`)
        }
      />
    </MainContainer>
  )
}
