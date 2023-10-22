"use client"

import { ColumnDef } from "@tanstack/react-table"
import { IReleaseHeadDto } from "@/lib/api/web-api-models"

export const columns: ColumnDef<IReleaseHeadDto>[] = [
  {
    accessorKey: "id",
  },
  {
    accessorKey: "name",
    header: "Name",
  },
]
