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
import useTargets from "@/hooks/useTargets"
import { useParams } from "next/navigation"

export default function AddTargetDialog() {
  const { projectId }: { projectId: string } = useParams()

  const [open, setOpen] = useState(false)
  const [isSaving, setIsSaving] = useState(false)
  const { mutateTargets } = useTargets(projectId)
  const { toast } = useToast()

  const trimString = (u: unknown) => (typeof u === "string" ? u.trim() : u)

  const formSchema = z.object({
    name: z.preprocess(
      trimString,
      z
        .string()
        .min(1, "Target Name is required")
        .max(20)
        .refine(async (value) => {
          const res = await fetch(
            `/backendapi/targets/${projectId}/uniquename?name=${value}&targetId=0`
          )
          return (await res.json()).value
        }, "Another target with this name already exists")
    ),
  })

  type FormSchemaType = z.infer<typeof formSchema>

  const form = useForm<FormSchemaType>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: "",
    },
  })

  const closeForm = () => {
    setOpen(false)
    form.reset()
  }

  const onSubmit: SubmitHandler<FormSchemaType> = async (data) => {
    setIsSaving(true)
    const response = await fetch(`/backendapi/targets/${projectId}`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    })
    if (!response.ok) {
      closeForm()
      toast({
        variant: "destructive",
        title: "Uh oh! Something went wrong.",
        description: "There was a problem when saving new target. Try again!",
      })
    } else {
      mutateTargets()
      closeForm()
      toast({
        title: "Success!",
        description: "New target has been added.",
      })
    }
    setIsSaving(false)
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button>
          <Icons.plus className="mr-2 h-4 w-4" />
          Add Target
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <fieldset className={cn("group")}>
              <DialogHeader>
                <DialogTitle>Add Target</DialogTitle>
              </DialogHeader>
              <div className="grid gap-4 py-4 group-disabled:opacity-50">
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
              <DialogFooter>
                <Button
                  type="button"
                  className="w-24"
                  variant={"secondary"}
                  onClick={closeForm}
                >
                  Cancel
                </Button>
                <Button
                  className={cn(
                    "relative w-24 group-disabled:pointer-events-none"
                  )}
                  type="submit"
                  disabled={isSaving}
                >
                  <Icons.spinner
                    className={cn(
                      "absolute animate-spin text-slate-100 group-enabled:opacity-0  "
                    )}
                  />
                  <span className={cn("group-disabled:opacity-0")}>Save</span>
                </Button>
              </DialogFooter>
            </fieldset>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}
