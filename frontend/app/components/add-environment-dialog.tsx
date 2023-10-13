import React from "react";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "./ui/dialog";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "./ui/select";
import { Button } from "./ui/button";
import { Icons } from "./icons";
import { Label } from "./ui/label";
import { Input } from "./ui/input";

type Props = {};

const AddEnvironmentDialog = (props: Props) => {
  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button>
          <Icons.plus className="mr-2 h-4 w-4" />
          Add Environments
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Add Environment</DialogTitle>
          <DialogDescription>
            Add a new environment to your project
          </DialogDescription>
        </DialogHeader>
        <div className="grid gap-4 py-4">
          <div className="flex gap-3 items-center">
            <Label htmlFor="name" className="w-[50px]">
              Name
            </Label>
            <Input id="name" defaultValue="server 1"/>
          </div>
          <div className="flex gap-3 items-center">
            <Label htmlFor="target" className="w-[50px]">
              Target
            </Label>
            <Select>
              <SelectTrigger>
                <SelectValue placeholder="Add Target" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="target 1">Target 1</SelectItem>
                <SelectItem value="target 2">Target 2</SelectItem>
                <SelectItem value="target 3">Target 3</SelectItem>
              </SelectContent>
            </Select>
          </div>
        </div>
        <DialogFooter>
          <Button type="submit">Save</Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};

export default AddEnvironmentDialog;
