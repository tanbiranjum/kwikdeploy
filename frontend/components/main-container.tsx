import { cn } from "@/lib/utils"
import React from "react"

type MainContainerProps = {
  children: React.ReactNode
  props: React.DetailedHTMLProps<React.HTMLAttributes<HTMLElement>, HTMLElement>
}

const MainContainer = ({ children, props }: MainContainerProps) => {
  return (
    <main
      {...props}
      className={cn(
        "mx-auto max-w-[1334px] space-y-9 px-[33px] py-10",
        props.className
      )}
    >
      {children}
    </main>
  )
}

export default MainContainer
