import NavOrgLevel from "@/components/nav/NavOrgLevel"

export default function OrgLevelLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <>
      <NavOrgLevel />
      {children}
    </>
  )
}
