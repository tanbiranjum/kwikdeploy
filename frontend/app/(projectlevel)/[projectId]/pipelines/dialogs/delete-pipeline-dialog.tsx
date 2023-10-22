"use client"

import React, { useState } from "react"
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog"
import { Button } from "@/components/ui/button"
import { Icons } from "@/components/icons"
import { cn } from "@/lib/utils"
import { useParams, useRouter } from "next/navigation"
import usePipelines from "@/hooks/usePipelines"

export default function DeletePipelineDialog() {
  const [open, setOpen] = useState(false)
  const [isSaving, setIsSaving] = useState(false)

  const router = useRouter()
  const { projectId, pipelineId }: { projectId: string; pipelineId: string } =
    useParams()
  const { mutatePipelines } = usePipelines(projectId)

  const handleDeletePipeline = async () => {
    setIsSaving(true)

    const response = await fetch(
      `/backendapi/pipelines/${projectId}/${pipelineId}`,
      {
        method: "DELETE",
        headers: { "Content-Type": "application/json" },
      }
    )
    if (!response.ok) {
      setOpen(false)
    } else {
      mutatePipelines()
      setTimeout(() => {
        setOpen(false)
        router.push(`/${projectId}/pipelines`)
      }, 500)
    }
    setIsSaving(false)
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button variant={"destructive"} className="w-24">
          Delete
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Delete Pipeline</DialogTitle>
        </DialogHeader>
        <div className="grid gap-4 py-4 group-disabled:opacity-50">
          <p>Are you sure you want to delete this pipeline?</p>
        </div>
        <DialogFooter>
          <fieldset disabled={isSaving} className="group">
            <Button
              variant={"destructive"}
              className={cn("relative w-24 group-disabled:pointer-events-none")}
              disabled={isSaving}
              onClick={() => handleDeletePipeline()}
            >
              <Icons.spinner
                className={cn(
                  "absolute animate-spin text-slate-100 group-enabled:opacity-0"
                )}
              />
              <span className={cn("group-disabled:opacity-0")}>Delete</span>
            </Button>
            <Button
              variant={"secondary"}
              className="ml-2 w-24"
              onClick={() => setOpen(false)}
            >
              Cancel
            </Button>
          </fieldset>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}
