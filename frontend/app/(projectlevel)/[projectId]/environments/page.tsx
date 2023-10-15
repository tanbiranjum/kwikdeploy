import AddEnvironmentDialog from "@/components/add-environment-dialog"
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
import React from "react"

type Props = {}

const targetName = [
  {
    name: "dev",
    target: "Server 1",
  },
  {
    name: "test",
    target: "Server 2",
  },
  {
    name: "prod",
    target: "Server 3",
  },
]

const EnvironmentsPage = (props: Props) => {
  return (
    <MainContainer props={{ className: "" }}>
      <AddEnvironmentDialog />
      <Table>
        <TableHeader className="border bg-slate-100">
          <TableRow>
            <TableHead className="w-[200px]">Environment Name</TableHead>
            <TableHead>Target</TableHead>
            <TableHead className="text-right">Action</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {targetName.map((target) => (
            <TableRow key={target.name}>
              <TableCell className="font-medium">{target.name}</TableCell>
              <TableCell>{target.target}</TableCell>
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

export default EnvironmentsPage
