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
import DeletePipelineDialog from "../../dialogs/delete-pipeline-dialog"
import usePipeline from "@/hooks/usePipeline"
import usePipelines from "@/hooks/usePipelines"

export default function PipelineSettings() {
  const {
    projectId,
    pipelineId: pipelineId,
  }: { projectId: string; pipelineId: string } = useParams()

  const { pipeline } = usePipeline(projectId, pipelineId)
  const { mutatePipelines } = usePipelines(projectId)
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
            `/backendapi/pipelines/${projectId}/uniquename?name=${value}&pipelineId=${pipelineId}`
          )
          return (await res.json()).value
        }, "Another pipeline with this name already exists")
    ),
  })

  type FormSchemaType = z.infer<typeof formSchema>

  const form = useForm<FormSchemaType>({
    resolver: zodResolver(formSchema),
    defaultValues: useMemo(() => pipeline, [pipeline]),
  })

  const onSubmit: SubmitHandler<FormSchemaType> = async (data) => {
    setIsSaving(true)
    const response = await fetch(
      `/backendapi/pipelines/${projectId}/${pipelineId}`,
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
        description: "There was a problem when saving the pipeline. Try again!",
      })
    } else {
      mutatePipelines()
      toast({
        title: "Success!",
        description: "Pipeline has been saved.",
      })
    }
    setIsSaving(false)
  }

  useEffect(() => {
    form.reset(pipeline)
  }, [pipeline, form])

  return (
    <>
      <div className="space-y-6">
        <h3 className="text-lg font-medium">Pipeline Details</h3>
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
