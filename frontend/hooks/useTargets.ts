import fetcher from "@/lib/fetcher"
import useSWR, { mutate } from "swr"
import { PaginatedListOfTargetHeadDto } from "@/lib/api/web-api-models"

export default function useTargets(projectId: string) {
  const url = `/backendapi/targets/${projectId}`

  const { data, error, isLoading } = useSWR<PaginatedListOfTargetHeadDto>(
    url,
    fetcher
  )

  const mutateTargets = async () => {
    mutate(url)
  }

  return {
    isLoading,
    isError: error,
    targets: data,
    mutateTargets,
  }
}
