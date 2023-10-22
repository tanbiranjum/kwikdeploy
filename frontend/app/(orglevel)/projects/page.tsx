"use client"

import { cn } from "@/lib/utils"
import React from "react"
import ProjectCard from "./project-card"

import AddProjectDialog from "@/app/(orglevel)/projects/dialogs/add-project-dialog"
import MainContainer from "@/components/main-container"
import useProjects from "@/hooks/useProjects"
import Loading from "@/components/loading"

export default function ProjectsPage() {
  const { projects, isLoading, isError } = useProjects()

  if (isLoading) return <Loading />

  if (isError) {
    return <p>Something went wrong try again!</p>
  }

  return (
    <MainContainer props={{ className: "" }}>
      <AddProjectDialog />
      <div className={cn("flex flex-wrap gap-4")}>
        {projects?.items &&
          projects.items.map((project) => (
            <ProjectCard
              key={project.id}
              projectId={project.id!}
              titleCard={project.name}
              descriptionCard=""
              bodyCard=""
              footerCard=""
            />
          ))}
      </div>
    </MainContainer>
  )
}
