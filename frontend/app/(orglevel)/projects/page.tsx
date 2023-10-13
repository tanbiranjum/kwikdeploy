import { cn } from "@/lib/utils";
import React from "react";
import ProjectCard from "./project-card";

import Link from "next/link";
import MainContainer from "../../components/main-container";
import { Button } from "../../components/ui/button";
import { Icons } from "../../components/icons";

const projects = [
  { id: 1, name: 'My Project' },
  { id: 2, name: 'Second Project' },
  { id: 3, name: 'Another Project' },
  { id: 4, name: 'Yet Another Project' },
  { id: 5, name: 'Brand New Project' },
]

type Props = {};

const ProjectsPage = (props: Props) => {
  return (
    <MainContainer props={{ className: "" }}>
      <div className={cn("")}>
        <Link href="/targets">
          <Button>
            <Icons.plus className="mr-2 h-4 w-4" />
            Add Project
          </Button>
        </Link>
      </div>
      <div className={cn("flex flex-wrap gap-4")}>
        {projects.map((project) => (
          <ProjectCard
            projectId={project.id}
            titleCard={project.name}
            descriptionCard=""
            bodyCard=""
            footerCard=""
          />
        ))}
      </div>
    </MainContainer>
  );
};

export default ProjectsPage;
