"use client"

import CardWithForm from "@/app/(projectlevel)/[projectId]/settings/update-project"
import { Icons } from "@/components/icons"
import MainContainer from "@/components/main-container"
import useProjects from "@/hooks/useProjects"

import { useParams } from "next/navigation"

type ProjectParams = {
  projectId: string
}
export default function ProjectSettingsPage() {
  const { projectId } = useParams<ProjectParams>()
  const { isLoading, projects, isError } = useProjects()

  const project = projects?.items?.find(
    (item) => item.id === parseInt(projectId) || { id: -1 }
  )

  if (isError) {
    return <p>Something whent wrong try again!</p>
  }

  if (project?.id === undefined) return null

  return (
    <MainContainer props={{ className: "flex justify-center" }}>
      {isLoading ? (
        <Icons.spinner className="mr-2 h-10 w-10 animate-spin" />
      ) : (
        <CardWithForm />
      )}
    </MainContainer>
  )
}
