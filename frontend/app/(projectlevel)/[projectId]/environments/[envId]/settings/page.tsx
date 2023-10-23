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
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
} from "@/components/ui/command"
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover"
import { Check, ChevronsUpDown } from "lucide-react"
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
import DeleteEnvironmentDialog from "../../dialogs/delete-environment-dialog"
import useEnv from "@/hooks/useEnv"
import useEnvs from "@/hooks/useEnvs"
import useTargets from "@/hooks/useTargets"

export default function EnviornmentSettings() {
  const { projectId, envId }: { projectId: string; envId: string } = useParams()

  const { env, mutateEnv } = useEnv(projectId, envId)
  const { mutateEnvs } = useEnvs(projectId)
  const [isSaving, setIsSaving] = useState(false)
  const [targetsComboOpen, setTargetsComboOpen] = useState(false)
  const { targets } = useTargets(projectId)
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
            `/backendapi/envs/${projectId}/uniquename?name=${value}&envId=${envId}`
          )
          return (await res.json()).value
        }, "Another environment with this name already exists")
    ),
    targetId: z.number(),
  })

  type FormSchemaType = z.infer<typeof formSchema>

  const form = useForm<FormSchemaType>({
    resolver: zodResolver(formSchema),
    defaultValues: useMemo(() => env, [env]),
  })

  const onSubmit: SubmitHandler<FormSchemaType> = async (data) => {
    setIsSaving(true)
    const response = await fetch(`/backendapi/envs/${projectId}/${envId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    })
    if (!response.ok) {
      toast({
        variant: "destructive",
        title: "Uh oh! Something went wrong.",
        description:
          "There was a problem when saving the environment. Try again!",
      })
    } else {
      mutateEnv()
      mutateEnvs()
      toast({
        title: "Success!",
        description: "Environment has been saved.",
      })
    }
    setIsSaving(false)
  }

  useEffect(() => {
    form.reset(env)
  }, [env, form])

  return (
    <>
      <div className="space-y-6">
        <h3 className="text-lg font-medium">Environment Details</h3>
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
                <FormField
                  control={form.control}
                  name="targetId"
                  render={({ field }) => (
                    <FormItem className="flex flex-col">
                      <FormLabel>Target</FormLabel>
                      <Popover
                        open={targetsComboOpen}
                        onOpenChange={setTargetsComboOpen}
                      >
                        <PopoverTrigger asChild>
                          <FormControl>
                            <Button
                              variant="outline"
                              role="combobox"
                              className={cn(
                                "w-[200px] justify-between",
                                !field.value && "text-muted-foreground"
                              )}
                            >
                              {field.value
                                ? targets?.items?.find(
                                    (item) => item.id == field.value
                                  )?.name
                                : "Select target"}
                              <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                            </Button>
                          </FormControl>
                        </PopoverTrigger>
                        <PopoverContent className="w-[200px] p-0">
                          <Command>
                            <CommandInput placeholder="Search target..." />
                            <CommandEmpty>No target found.</CommandEmpty>
                            <CommandGroup>
                              {targets?.items?.map((item) => (
                                <CommandItem
                                  value={item.name}
                                  key={item.id}
                                  onSelect={() => {
                                    form.setValue("targetId", item.id!)
                                    setTargetsComboOpen(false)
                                  }}
                                >
                                  <Check
                                    className={cn(
                                      "mr-2 h-4 w-4",
                                      item.id === field.value
                                        ? "opacity-100"
                                        : "opacity-0"
                                    )}
                                  />
                                  {item.name}
                                </CommandItem>
                              ))}
                            </CommandGroup>
                          </Command>
                        </PopoverContent>
                      </Popover>
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
        <DeleteEnvironmentDialog />
      </div>
    </>
  )
}
