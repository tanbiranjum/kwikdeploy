import { MainNav } from "@/app/dashboard/components/main-nav";
import { UserNav } from "@/app/dashboard/components/user-nav";
import React from "react";

type Props = {};

const ManiNavContainer = (props: Props) => {
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
