import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog"
import { Button, buttonVariants } from "@/components/ui/button"
import { useRouter } from "next/navigation"
import { Input } from "@/components/ui/input"
import { toast } from "@/components/ui/use-toast"
import useProjects from "@/hooks/useProjects"
import { cn } from "@/lib/utils"
import { zodResolver } from "@hookform/resolvers/zod"

import { useState } from "react"
import { useForm } from "react-hook-form"
import z from "zod"

interface AlertProps {
  projectName?: string
  projectId: string
}

export default function AlertComponet({
  projectName,
  projectId: id,
}: AlertProps) {
  const router = useRouter()
  const { mutateProjects } = useProjects()

  const trimString = (u: unknown) => (typeof u === "string" ? u.trim() : u)

  const [isDisabled, setIsDisabled] = useState(true)

  const formSchema = z.object({
    name: z.preprocess(
      trimString,
      z.string().refine((value) => {
        return value === projectName
      })
    ),
  })

  type FormSchemaType = z.infer<typeof formSchema>
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(formSchema),
  })

  const handleOnChangeMatcher = (e: any) => {
    e.preventDefault()

    const input = { name: e.target.value }
    const validation = formSchema.safeParse(input)

    setIsDisabled(!validation.success)
  }

  const handleOnDelete = async () => {
    const response = await fetch(`/backendapi/projects/${id}`, {
      method: "DELETE",
    })
    if (!response.ok) {
      toast({
        variant: "destructive",
        title: "Uh oh! Something went wrong.",
        description:
          "There was a problem when deleting the project. Try again!",
      })
    } else {
      mutateProjects()
      toast({
        title: "Success!",
        description: "Project deleted successfully.",
        color: "red",
      })
      setTimeout(() => {
        router.push("/projects")
      }, 500)
    }
  }

  return (
    <AlertDialog>
      <AlertDialogTrigger asChild>
        <Button variant="destructive" className="w-24">
          Delete
        </Button>
      </AlertDialogTrigger>
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>Are you absolutely sure?</AlertDialogTitle>
          <AlertDialogDescription className={cn("leading-6")}>
            This will permanently delete the project {projectName}
            <br />
            To confirm, type
            <span className="font-bold text-black"> {projectName}</span> in the
            box below
          </AlertDialogDescription>
          <Input
            className={cn("mt-16", {
              "focus-visible:ring-red-600": isDisabled,
            })}
            onChange={handleOnChangeMatcher}
          />
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogAction
            className={cn(buttonVariants({ variant: "destructive" }))}
            disabled={isDisabled}
            onClick={handleOnDelete}
          >
            Delete
          </AlertDialogAction>
          <AlertDialogCancel>Cancel</AlertDialogCancel>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  )
}
