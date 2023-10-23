"use client"

import { Icons } from "@/components/icons"
import MainContainer from "@/components/main-container"
import { Button } from "@/components/ui/button"
import { Checkbox } from "@/components/ui/checkbox"
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table"

const envMap: { [id: string]: string } = {
  "1": "QA",
  "2": "UAT",
  "3": "Production",
}

interface IVariable {
  id: number
  name: string
  secret: boolean
  values: { [id: string]: string }
}

const variables: IVariable[] = [
  {
    id: 1,
    name: "ENV",
    secret: true,
    values: {
      "1": "QA",
      "2": "UAT",
      "3": "Prod",
    },
  },
  {
    id: 2,
    name: "LOGGING_ENABLED",
    secret: false,
    values: {
      "1": "true",
      "2": "false",
      "3": "true",
    },
  },
]

export default function VariablesPages() {
  return (
    <MainContainer props={{ className: "" }}>
      <Button>
        <Icons.plus className="mr-2 h-4 w-4" />
        Add Variable
      </Button>
      <div className="rounded-md border shadow-md">
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead className="w-[300px]">Variable Name</TableHead>
              <TableHead className="w-[100px]">Secret</TableHead>
              <TableHead>Values</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {variables.map((variable) => (
              <TableRow key={variable.id}>
                <TableCell className="align-top">{variable.name}</TableCell>
                <TableCell className="align-top">
                  <Checkbox checked={variable.secret} />
                </TableCell>
                <TableCell>
                  {Object.keys(variable.values).map((envId) => (
                    <span
                      key={envId}
                      className="mr-4 inline-flex items-center rounded-md text-xs font-medium text-gray-600 ring-1 ring-inset ring-gray-500/40"
                    >
                      <span className="rounded-l-md bg-gray-100 px-3 py-2 font-bold ring-1 ring-inset ring-gray-500/40">
                        {envMap[envId]}
                      </span>
                      <span className="px-3">
                        {variable.secret ? "***" : variable.values[envId]}
                      </span>
                    </span>
                  ))}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>
    </MainContainer>
  )
}
