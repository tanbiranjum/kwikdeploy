import { cn } from "@/lib/utils";
import React from "react";

type MainContainerProps = {
  children: React.ReactNode;
  props: React.DetailedHTMLProps<
    React.HTMLAttributes<HTMLElement>,
    HTMLElement
  >;
  title: string;
};

const MainContainer = ({ children, props }: MainContainerProps) => {
  return (
    <main
      {...props}
      className={cn(
        "max-w-[1334px] mx-auto py-10 px-[33px] space-y-9",
        props.className
      )}
    >
      {children}
    </main>
  );
};

export default MainContainer;
