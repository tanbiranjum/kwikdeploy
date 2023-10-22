"use client"

import { ColumnDef } from "@tanstack/react-table"
import { IAppDefHeadDto } from "@/lib/api/web-api-models"

export const columns: ColumnDef<IAppDefHeadDto>[] = [
  {
    accessorKey: "id",
  },
  {
    accessorKey: "name",
    header: "Name",
  },
  {
    accessorKey: "imageName",
    header: "Image",
  },
  {
    accessorKey: "tag",
    header: "Tag",
  },
]
