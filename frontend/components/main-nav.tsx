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
        href="/dashboard/projects"
        className="text-sm font-medium transition-colors hover:text-primary"
      >
        Projects
      </Link>
      <Link
        href="/dashboard/targets"
        className="text-sm font-medium text-muted-foreground transition-colors hover:text-primary"
      >
        Targets
      </Link>
      <Link
        href="/dashboard/environments"
        className="text-sm font-medium text-muted-foreground transition-colors hover:text-primary"
      >
        Environments
      </Link>
      <Link
        href="/dashboard/app-definitions"
        className="text-sm font-medium text-muted-foreground transition-colors hover:text-primary"
      >
        App Definitions
      </Link>
      <Link
        href="/dashboard/releases"
        className="text-sm font-medium text-muted-foreground transition-colors hover:text-primary"
      >
        Releases
      </Link>
      <Link
        href="/dashboard/settings"
        className="text-sm font-medium text-muted-foreground transition-colors hover:text-primary"
      >
        Settings
      </Link>
    </nav>
  );
}
