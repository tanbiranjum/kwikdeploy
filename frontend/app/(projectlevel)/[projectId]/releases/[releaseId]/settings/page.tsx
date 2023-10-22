"use client"

import React, { useEffect, useMemo, useState } from "react"
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form"
import { Button } from "@/components/ui/button"
import { Icons } from "@/components/icons"
import { Input } from "@/components/ui/input"
import { SubmitHandler, useForm } from "react-hook-form"
import { z } from "zod"
import { zodResolver } from "@hookform/resolvers/zod"
import { useToast } from "@/components/ui/use-toast"
import { cn } from "@/lib/utils"
import { useParams } from "next/navigation"
import { Separator } from "@/components/ui/separator"
import DeletePipelineDialog from "../../dialogs/delete-release-dialog"
import useRelease from "@/hooks/useRelease"
import useReleases from "@/hooks/useReleases"

export default function ReleaseSettings() {
  const { projectId, releaseId }: { projectId: string; releaseId: string } =
    useParams()

  const { release, mutateRelease } = useRelease(projectId, releaseId)
  const { mutateReleases } = useReleases(projectId)
  const [isSaving, setIsSaving] = useState(false)
  const { toast } = useToast()

  const trimString = (u: unknown) => (typeof u === "string" ? u.trim() : u)

  const formSchema = z.object({
    name: z.preprocess(
      trimString,
      z
        .string()
        .min(1, "Name is required")
        .max(20)
        .refine(async (value) => {
          const res = await fetch(
            `/backendapi/releases/${projectId}/uniquename?name=${value}&releaseId=${releaseId}`
          )
          return (await res.json()).value
        }, "Another release with this name already exists")
    ),
  })

  type FormSchemaType = z.infer<typeof formSchema>

  const form = useForm<FormSchemaType>({
    resolver: zodResolver(formSchema),
    defaultValues: useMemo(() => release, [release]),
  })

  const onSubmit: SubmitHandler<FormSchemaType> = async (data) => {
    setIsSaving(true)
    const response = await fetch(
      `/backendapi/releases/${projectId}/${releaseId}`,
      {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
      }
    )
    if (!response.ok) {
      toast({
        variant: "destructive",
        title: "Uh oh! Something went wrong.",
        description: "There was a problem when saving the release. Try again!",
      })
    } else {
      mutateRelease()
      mutateReleases()
      toast({
        title: "Success!",
        description: "Release has been saved.",
      })
    }
    setIsSaving(false)
  }

  useEffect(() => {
    form.reset(release)
  }, [release, form])

  return (
    <>
      <div className="space-y-6">
        <h3 className="text-lg font-medium">Release Details</h3>
        <Separator />
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <fieldset className={cn("group")} disabled={isSaving}>
              <div className="grid gap-4 pb-4 group-disabled:opacity-50">
                <FormField
                  control={form.control}
                  name="name"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Name</FormLabel>
                      <FormControl>
                        <Input {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
              <div>
                <Button
                  className={cn(
                    "relative w-24 group-disabled:pointer-events-none"
                  )}
                  type="submit"
                >
                  <Icons.spinner
                    className={cn(
                      "absolute animate-spin text-slate-100 group-enabled:opacity-0"
                    )}
                  />
                  <span className={cn("group-disabled:opacity-0")}>Save</span>
                </Button>
              </div>
            </fieldset>
          </form>
        </Form>
      </div>
      <div className="mt-12 space-y-6">
        <h3 className="text-lg font-medium text-rose-500">Danger Zone</h3>
        <Separator />
        <DeletePipelineDialog />
      </div>
    </>
  )
}
