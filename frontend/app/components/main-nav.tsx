"use client";
import { usePathname } from "next/navigation";
import Link from "next/link";

import { cn } from "@/lib/utils";

export function MainNav({
  className,
  ...props
}: React.HTMLAttributes<HTMLElement>) {
  const pathname = usePathname();
  if (pathname === "/login") return null;

  if (pathname === "/")
    return (
      <nav
        className={cn("flex items-center space-x-4 lg:space-x-6", className)}
        {...props}
      >
        <Link
          href="/"
          className="text-sm font-medium transition-colors hover:text-primary"
        >
          Projects
        </Link>
        <Link
          href="/settings"
          className="text-sm font-medium text-muted-foreground transition-colors hover:text-primary"
        >
          Settings
        </Link>
      </nav>
    );

  return (
    <nav
      className={cn("flex items-center space-x-4 lg:space-x-6", className)}
      {...props}
    >
      <Link
        href="/"
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
