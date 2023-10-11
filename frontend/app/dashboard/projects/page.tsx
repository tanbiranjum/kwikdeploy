import { cn } from "@/lib/utils";
import React from "react";
import ProjectCard from "./project-card";
import { Button } from "@/components/ui/button";
import Link from "next/link";
import { Card } from "@/components/ui/card";
type Props = {};

const ProjectsPage = (props: Props) => {
  return (
    <main className={cn("p-5 space-y-7 max-w-[1440] ")}>
      <div className={cn("py-4")}>
        <Link href="/dashboard/targets">
          <Button>Add a project</Button>
        </Link>
      </div>
      <div className={cn("flex flex-wrap gap-3")}>
        <ProjectCard id="test" />
        <ProjectCard id="test" />
        <ProjectCard id="test" />
        <ProjectCard id="test" />
      </div>
    </main>
  );
};

export default ProjectsPage;
