import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { cn } from "@/lib/utils";
import Link from "next/link";

type CardProps = React.ComponentProps<typeof Card>;

const ProjectCard = ({ className, ...props }: CardProps) => {
  return (
    <Link href={`/dashboard/projects/${props.id}`}>
      <Card className={cn("w-[380px] text-center", className)} {...props}>
        <CardHeader>
          <CardTitle>Title </CardTitle>
          <CardDescription>description </CardDescription>
        </CardHeader>
        <CardContent className="grid gap-4">
          <div>Content</div>
        </CardContent>
        <CardFooter>footer</CardFooter>
      </Card>
    </Link>
  );
};

export default ProjectCard;
