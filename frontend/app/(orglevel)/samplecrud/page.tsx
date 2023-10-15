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
    name: "target 1",
    status: "Online",
  },
  {
    name: "target 2",
    status: "Online",
  },
  {
    name: "target 3",
    status: "Online",
  },
  {
    name: "target 4",
    status: "Online",
  },
]

const SampleCrudPage = (props: Props) => {
  return (
    <MainContainer props={{ className: "" }}>
      <Button>
        <Icons.plus className="mr-2 h-4 w-4" />
        Add Target
      </Button>
      <Table>
        <TableHeader className="border bg-slate-100">
          <TableRow>
            <TableHead className="w-[200px]">Target Name</TableHead>
            <TableHead>Status</TableHead>
            <TableHead className="text-right">Action</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {targetName.map((target) => (
            <TableRow key={target.name}>
              <TableCell className="font-medium">{target.name}</TableCell>
              <TableCell>{target.status}</TableCell>
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
