import { Icons } from "@/components/icons";
import { Button } from "@/components/ui/button";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import React from "react";

type Props = {};

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
];

const EnvironmentsPage = (props: Props) => {
  return (
    <div className="space-y-6 py-8">
      <Button>
        <Icons.plus className="mr-2 h-4 w-4" />
        Add Environments
      </Button>
      <Table>
        <TableHeader className="bg-slate-100 border">
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
                <div className="flex gap-3 justify-end">
                  <Button variant="outline" size="icon">
                    <Icons.pencil className="w-4 h-4" />
                  </Button>
                  <Button variant="outline" size="icon">
                    <Icons.trash className="w-4 h-4 text-red-600" />
                  </Button>
                </div>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
};

export default EnvironmentsPage;
