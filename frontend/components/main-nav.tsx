import Link from "next/link";

import { cn } from "@/lib/utils";

export function MainNav({
  className,
  ...props
}: React.HTMLAttributes<HTMLElement>) {
  return (
    <nav
      className={cn("flex items-center space-x-4 lg:space-x-6", className)}
      {...props}
    >
      <Link
        href="/projects"
        className="text-sm font-medium transition-colors hover:text-primary"
      >
        Projects
      </Link>
      <Link
        href="/targets"
        className="text-sm font-medium text-muted-foreground transition-colors hover:text-primary"
      >
        Targets
      </Link>
      <Link
        href="/environments"
        className="text-sm font-medium text-muted-foreground transition-colors hover:text-primary"
      >
        Environments
      </Link>
      <Link
        href="/app-definitions"
        className="text-sm font-medium text-muted-foreground transition-colors hover:text-primary"
      >
        App Definitions
      </Link>
      <Link
        href="/releases"
        className="text-sm font-medium text-muted-foreground transition-colors hover:text-primary"
      >
        Releases
      </Link>
      <Link
        href="/settings"
        className="text-sm font-medium text-muted-foreground transition-colors hover:text-primary"
      >
        Settings
      </Link>
    </nav>
  );
}
