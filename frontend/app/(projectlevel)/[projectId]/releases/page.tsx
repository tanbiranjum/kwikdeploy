"use client"

import React from "react"
import MainContainer from "@/components/main-container"
import { DataTable } from "@/components/ui/data-table"
import { columns } from "./table/columns"
import { useParams, useRouter } from "next/navigation"
import Loading from "@/components/loading"
import useReleases from "@/hooks/useReleases"
import AddReleaseDialog from "./dialogs/add-release-dialog"

export default function ReleasesPage() {
  const { projectId }: { projectId: string } = useParams()
  const { isLoading, releases } = useReleases(projectId)
  const router = useRouter()

  if (isLoading) return <Loading />

  if (!releases?.items) return null

  return (
    <MainContainer props={{ className: "" }}>
      <AddReleaseDialog />
      <DataTable
        columns={columns}
        data={releases.items}
        onRowClick={(id) =>
          router.push(`/${projectId}/releases/${id}/settings`)
        }
      />
    </MainContainer>
  )
}
