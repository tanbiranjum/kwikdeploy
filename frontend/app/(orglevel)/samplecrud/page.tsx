"use client"

import AddProjectDialog from "@/components/app-project-dialog"
import { Icons } from "@/components/icons"
import MainContainer from "@/components/main-container"
import { Button } from "@/components/ui/button"
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table"
import useProjects from "@/hooks/useProjects"
import React from "react"

const SampleCrudPage = () => {
  const { projects } = useProjects()

  if (!projects?.items) return null

  return (
    <MainContainer props={{ className: "" }}>
      <AddProjectDialog />
      <Table>
        <TableHeader className="border bg-slate-100">
          <TableRow>
            <TableHead className="w-[200px]">Project Name</TableHead>
            <TableHead className="text-right">Action</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {projects.items.map((item) => (
            <TableRow key={item.name}>
              <TableCell className="font-medium">{item.name}</TableCell>
              <TableCell className="text-right">
                <div className="flex justify-end gap-3">
                  <Button variant="outline" size="icon">
                    <Icons.pencil className="h-4 w-4" />
                  </Button>
                  <Button variant="outline" size="icon">
                    <Icons.trash className="h-4 w-4 text-red-600" />
                  </Button>
                </div>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </MainContainer>
  )
}

export default SampleCrudPage
