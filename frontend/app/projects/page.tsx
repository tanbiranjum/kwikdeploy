import { cn } from "@/lib/utils";
import React from "react";
import ProjectCard from "./project-card";
import { Button } from "@/components/ui/button";
import Link from "next/link";
import MainContainer from "../components/main-container";

type Props = {};

const ProjectsPage = (props: Props) => {
  return (
    <MainContainer>
      <div className={cn("")}>
        <Link href="/dashboard/targets">
          <Button>Add a project</Button>
        </Link>
      </div>
      <div className={cn("flex justify-center items-center flex-wrap gap-4")}>
        <ProjectCard id="test" />
        <ProjectCard id="test" />
        <ProjectCard id="test" />
        <ProjectCard id="test" />
      </div>
    </MainContainer>
  );
};

export default ProjectsPage;
