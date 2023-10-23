"use client"

import { ColumnDef } from "@tanstack/react-table"
import { IPipelineHeadDto } from "@/lib/api/web-api-models"

export const columns: ColumnDef<IPipelineHeadDto>[] = [
  {
    accessorKey: "id",
  },
  {
    accessorKey: "name",
    header: "Name",
  },
  {
    accessorKey: "targetName",
    header: "Target",
  },
]
