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
import useReleases from "@/hooks/useReleases"

export default function DeleteReleaseDialog() {
  const [open, setOpen] = useState(false)
  const [isSaving, setIsSaving] = useState(false)

  const router = useRouter()
  const { projectId, releaseId }: { projectId: string; releaseId: string } =
    useParams()
  const { mutateReleases } = useReleases(projectId)

  const handleDeleteRelease = async () => {
    setIsSaving(true)

    const response = await fetch(
      `/backendapi/releases/${projectId}/${releaseId}`,
      {
        method: "DELETE",
        headers: { "Content-Type": "application/json" },
      }
    )
    if (!response.ok) {
      setOpen(false)
    } else {
      mutateReleases()
      setTimeout(() => {
        setOpen(false)
        router.push(`/${projectId}/releases`)
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
          <DialogTitle>Delete Release</DialogTitle>
        </DialogHeader>
        <div className="grid gap-4 py-4 group-disabled:opacity-50">
          <p>Are you sure you want to delete this release?</p>
        </div>
        <DialogFooter>
          <fieldset disabled={isSaving} className="group">
            <Button
              variant={"destructive"}
              className={cn("relative w-24 group-disabled:pointer-events-none")}
              disabled={isSaving}
              onClick={() => handleDeleteRelease()}
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
