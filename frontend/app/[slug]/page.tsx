import { cn } from "@/lib/utils";

interface Iparams {
  slug: string;
}

const ProjectDetails = ({ params }: { params: Iparams }) => {
  return (
    <main className={cn("")}>
      Project details <p>{params.slug}</p>
    </main>
  );
};

export default ProjectDetails;
