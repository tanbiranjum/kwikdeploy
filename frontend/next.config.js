/** @type {import('next').NextConfig} */

const nextConfig = {
  output: "standalone",
  async redirects() {
    return [
      {
        source: "/",
        destination: "/projects",
        permanent: true,
      },
    ]
  },
}

module.exports = nextConfig
