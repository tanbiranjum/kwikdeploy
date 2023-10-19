"use client"

import { cn } from "@/lib/utils"
import React from "react"
import ProjectCard from "./project-card"

import AddProjectDialog from "@/components/app-project-dialog"
import MainContainer from "@/components/main-container"
import useProjects from "@/hooks/useProjects"

type Props = {}

const ProjectsPage = (props: Props) => {
  const { projects, isError } = useProjects()

  if (isError) {
    return <p>Something whent wrong try again!</p>
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

export default ProjectsPage
