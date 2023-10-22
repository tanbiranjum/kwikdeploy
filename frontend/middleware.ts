import { NextResponse } from "next/server"
import type { NextRequest } from "next/server"
import micromatch from "micromatch"
import { getToken } from "next-auth/jwt"

const noAuthRoutes = ["/api/auth/**", "/login"]

const noAuthBackendApiRoutes = ["/backendapi/accounts/login"]

export async function middleware(request: NextRequest) {
  const { pathname } = request.nextUrl

  if (micromatch.isMatch(pathname, noAuthRoutes)) {
    return NextResponse.next()
  }

  const token = await getToken({ req: request })

  if (pathname.startsWith("/backendapi")) {
    if (!token && !micromatch.isMatch(pathname, noAuthBackendApiRoutes)) {
      return NextResponse.json(
        { message: "Authentication required" },
        { status: 401 }
      )
    }

    const scheme = process.env.BACKENDAPI_SCHEME!
    const hostname = process.env.BACKENDAPI_HOST!
    const port = process.env.BACKENDAPI_PORT!

    const requestHeaders = new Headers(request.headers)
    requestHeaders.set("host", hostname)
    if (token) {
      requestHeaders.set("Authorization", `Bearer ${token.apiToken}`)
    }

    let url = request.nextUrl.clone()
    url.protocol = scheme
    url.hostname = hostname
    url.port = port
    url.pathname = url.pathname.replace(/^\/backendapi/, "")

    return NextResponse.rewrite(url, {
      headers: requestHeaders,
    })
  }

  if (!token) {
    const url = new URL(`/api/auth/signin`)

    // TODO: Ensure request.url has original hostname and not 0.0.0.0
    //       Below adds 0.0.0.0 and causes Google Chrome to flag site as deceptive
    // url.searchParams.set("callbackUrl", encodeURI(request.url))

    return NextResponse.redirect(url)
  }

  return NextResponse.next()
}

export const config = {
  matcher: ["/((?!_next/static|_next/image|favicon.ico).*)"],
}
