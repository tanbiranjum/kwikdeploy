import * as React from "react";

import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "../../../components/ui/card";
import { Icons } from "../../../components/icons";
import { Button } from "../../../components/ui/button";

import MainContainer from "@/app/components/main-container";
import Link from "next/link";
import { cn } from "@/lib/utils";

export default function AppDefinitionsPage() {
  return (
    <MainContainer props={{ className: "" }}>
      <div className={cn("")}>
        <Link href="/targets">
          <Button>
            <Icons.plus className="mr-2 h-4 w-4" /> Add App Definition
          </Button>
        </Link>
      </div>
      <div className="flex justify-evenly">
        <Card className="w-[350px]">
          <CardHeader>
            <CardTitle>My Frontend</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="flex w-full justify-between ">
              <div className="flex gap-1 ">
                <span>Image:</span>
                my-frontend
              </div>
              <div>
                <span>Tag: v1</span>
              </div>
            </div>
          </CardContent>
          <CardFooter className="flex justify-between">
            <Button>Variables</Button>
            <Button>Mount</Button>
          </CardFooter>
        </Card>
        <Card className="w-[350px]">
          <CardHeader>
            <CardTitle>My Backend</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="flex justify-between w-full">
              <div className="flex gap-1">
                <span>Image:</span>
                my-backend
              </div>
              <div>
                <span>Tag: v1</span>
              </div>
            </div>
          </CardContent>
          <CardFooter className="flex justify-between">
            <Button>Variables</Button>
            <Button>Mount</Button>
          </CardFooter>
        </Card>
      </div>
    </MainContainer>
  );
}
