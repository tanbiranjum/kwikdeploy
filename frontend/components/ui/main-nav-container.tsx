"use client";

import { MainNav } from "@/app/components/main-nav";
import { UserNav } from "@/app/components/user-nav";
import React from "react";
import { usePathname } from "next/navigation";

type Props = {};

const ManiNavContainer = (props: Props) => {
  const pathname = usePathname();

  if (pathname === "/login") return null;
  return (
    <div className="border-b">
      <div className="flex h-16 items-center px-4">
        <MainNav className="mx-6" />
        <div className="ml-auto flex items-center space-x-4">
          <UserNav />
        </div>
      </div>
    </div>
  );
};

export default ManiNavContainer;
