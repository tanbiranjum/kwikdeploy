import { cn } from "@/lib/utils";
import React from "react";
import ProjectCard from "./project-card";

import Link from "next/link";
import MainContainer from "./components/main-container";
import { Button } from "./components/ui/button";

type Props = {};

const ProjectsPage = (props: Props) => {
  return (
    <MainContainer props={{ className: "" }}>
      <div className={cn("")}>
        <Link href="/targets">
          <Button>Add a project</Button>
        </Link>
      </div>
      <div className={cn("flex justify-center items-center flex-wrap gap-4")}>
        <ProjectCard
          id="123"
          titleCard="Project Title"
          descriptionCard="description"
          bodyCard="Content"
          footerCard=""
        />
        <ProjectCard
          id="123"
          titleCard="Project Title"
          descriptionCard="description"
          bodyCard="Content"
          footerCard=""
        />
        <ProjectCard
          id="123"
          titleCard="Project Title"
          descriptionCard="description"
          bodyCard="Content"
          footerCard=""
        />
        <ProjectCard
          id="123"
          titleCard="Project Title"
          descriptionCard="description"
          bodyCard="Content"
          footerCard=""
        />
        <ProjectCard
          id="123"
          titleCard="Project Title"
          descriptionCard="description"
          bodyCard="Content"
          footerCard=""
        />
      </div>
    </MainContainer>
  );
};

export default ProjectsPage;
