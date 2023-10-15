import React, { useState } from "react"
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "./ui/dialog"
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "./ui/form"
import { Button } from "./ui/button"
import { Icons } from "./icons"
import { Input } from "./ui/input"
import useProjects from "@/hooks/useProjects"
import { SubmitHandler, useForm } from "react-hook-form"
import { z } from "zod"
import { zodResolver } from "@hookform/resolvers/zod"

type Props = {}

const AddProjectDialog = (props: Props) => {
  const [open, setOpen] = useState(false)
  const { mutateProjects } = useProjects()

  const trimString = (u: unknown) => (typeof u === "string" ? u.trim() : u)

  const formSchema = z.object({
    name: z.preprocess(
      trimString,
      z
        .string()
        .min(1, "Project Name is required")
        .max(20)
        .refine(async (value) => {
          const res = await fetch(`/backendapi/projects/uniquename/${value}`)
          return await res.json()
        }, "Another project with this name already exists")
    ),
  })

  type FormSchemaType = z.infer<typeof formSchema>

  const form = useForm<FormSchemaType>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: "",
    },
  })

  const onSubmit: SubmitHandler<FormSchemaType> = async (data) => {
    const response = await fetch(`/backendapi/projects`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    })
    if (response.ok) {
      mutateProjects()
      setOpen(false)
      form.reset()
    }
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button>
          <Icons.plus className="mr-2 h-4 w-4" />
          Add Project
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <DialogHeader>
              <DialogTitle>Add Project</DialogTitle>
            </DialogHeader>
            <div className="grid gap-4 py-4">
              <FormField
                control={form.control}
                name="name"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Project Name</FormLabel>
                    <FormControl>
                      <Input {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <DialogFooter>
              <Button type="submit">Save</Button>
            </DialogFooter>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}

export default AddProjectDialog
