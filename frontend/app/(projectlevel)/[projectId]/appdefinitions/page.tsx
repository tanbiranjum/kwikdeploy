"use client"

import React from "react"
import MainContainer from "@/components/main-container"
import { DataTable } from "@/components/ui/data-table"
import { columns } from "./table/columns"
import { useParams, useRouter } from "next/navigation"
import Loading from "@/components/loading"
import AddAppDefDialog from "./dialogs/add-appdef-dialog"
import useAppDefs from "@/hooks/useAppDefs"

export default function AppDefsPage() {
  const { projectId }: { projectId: string } = useParams()
  const { isLoading, appDefs } = useAppDefs(projectId)
  const router = useRouter()

  if (isLoading) return <Loading />

  if (!appDefs?.items) return null

  return (
    <MainContainer props={{ className: "" }}>
      <AddAppDefDialog />
      <DataTable
        columns={columns}
        data={appDefs.items}
        onRowClick={(id) =>
          router.push(`/${projectId}/appdefinitions/${id}/settings`)
        }
      />
    </MainContainer>
  )
}
