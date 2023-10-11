import { cn } from "@/lib/utils";
import React from "react";

type Props = {
  children: React.ReactNode;
};

const MainContainer = ({ children }: Props) => {
  return (
    <main className={cn("max-w-[1334px] mx-auto py-10 px-[33px] space-y-9")}>
      {children}
    </main>
  );
};

export default MainContainer;
