import NextAuth from "next-auth"
import JWT from "next-auth/jwt"

declare module "next-auth/jwt" {
  interface JWT {
    token: string
  }
}

declare module "next-auth" {
  interface User {
    id: string
    token: string & DefaultSession["user"]
  }

  interface Session {
    user: User
  }
}
