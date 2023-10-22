"use client"

import { cn } from "@/lib/utils"
import { Input } from "@/components/ui/input"
import { Icons } from "@/components/icons"
import { Button } from "@/components/ui/button"
import { useState } from "react"
import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import { signIn } from "next-auth/react"

import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form"
import { z } from "zod"
import { useSearchParams } from "next/navigation"

const loginSchema = z.object({
  userName: z.string(),
  password: z.string().min(1).max(100),
})

type LoginFormValues = z.infer<typeof loginSchema>

export default function LoginPage() {
  const searchParams = useSearchParams()
  const [isLoading, setIsLoading] = useState(false)

  const form = useForm<LoginFormValues>({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      userName: "",
      password: "",
    },
  })

  async function onSubmit(data: LoginFormValues) {
    setIsLoading(true)

    let callbackUrl = searchParams.get("callbackUrl")
    if (!callbackUrl) callbackUrl = "/projects"

    const user = await signIn("credentials", {
      userName: data.userName,
      password: data.password,
      callbackUrl,
    })

    setIsLoading(false)
  }

  return (
    <main className="min-h-screen p-24">
      <div className="mx-auto max-w-sm space-y-6">
        <div className="flex flex-col space-y-2 text-center">
          <h1 className="text-2xl font-semibold tracking-tight">
            KwikDeploy Login
          </h1>
        </div>
        <div className={cn("grid gap-6")}>
          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-3">
              <fieldset className={cn("group")} disabled={isLoading}>
                <FormField
                  control={form.control}
                  name="userName"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Username</FormLabel>
                      <FormControl>
                        <Input {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="password"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Password</FormLabel>
                      <FormControl>
                        <Input type="password" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <Button
                  type="submit"
                  className={cn(
                    "relative mt-4 w-full group-disabled:pointer-events-none"
                  )}
                >
                  <Icons.spinner className="absolute animate-spin text-slate-100 group-enabled:opacity-0" />
                  <span className={cn("group-disabled:opacity-0")}>
                    Sign In
                  </span>
                </Button>
              </fieldset>
            </form>
          </Form>
        </div>
      </div>
    </main>
  )
}
