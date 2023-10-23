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
    secret: false,
    values: {
      "1": "QA",
      "2": "UAT",
      "3": "This is such a big value for a variable",
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
              <TableHead className="w-[10px]"></TableHead>
              <TableHead className="w-[300px]">Variable Name</TableHead>
              <TableHead className="w-[100px]">Secret</TableHead>
              <TableHead>Values</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {variables.map((variable) => (
              <TableRow key={variable.id}>
                <TableCell className="align-top">
                  <Icons.trash className="h-4 w-4 cursor-pointer" />
                </TableCell>
                <TableCell className="align-top">
                  <span className="cursor-pointer hover:underline">
                    {variable.name}
                  </span>
                </TableCell>
                <TableCell className="align-top">
                  <Checkbox checked={variable.secret} />
                </TableCell>
                <TableCell>
                  {Object.keys(variable.values).map((envId) => (
                    <VariableValue
                      envName={envMap[envId]}
                      isSecret={variable.secret}
                      value={variable.values[envId]}
                    />
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

function truncateVariableValue(value: string, chars = 20) {
  if (value && value.length > chars) {
    return value.substring(0, chars) + " ..."
  }

  return value
}

function VariableValue({
  envName,
  value,
  isSecret,
}: {
  envName: string
  value: string
  isSecret: boolean
}) {
  return (
    <span
      key={envName}
      className="mr-4 inline-flex cursor-pointer items-center rounded-md text-xs font-medium text-gray-600 ring-1 ring-inset ring-gray-500/40 hover:shadow-md"
    >
      <span className="rounded-l-md bg-gray-100 px-3 py-2 font-bold ring-1 ring-inset ring-gray-500/40">
        {envName}
      </span>
      <span className="px-3">
        {isSecret ? "***" : truncateVariableValue(value)}
      </span>
    </span>
  )
}
