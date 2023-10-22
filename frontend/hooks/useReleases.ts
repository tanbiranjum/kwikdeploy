import fetcher from "@/lib/fetcher"
import useSWR, { mutate } from "swr"
import { PaginatedListOfReleaseHeadDto } from "@/lib/api/web-api-models"

export default function useReleases(projectId: string) {
  const url = `/backendapi/releases/${projectId}`

  const { data, error, isLoading } = useSWR<PaginatedListOfReleaseHeadDto>(
    url,
    fetcher
  )

  const mutateReleases = async () => {
    mutate(url)
  }

  return {
    isLoading,
    isError: error,
    releases: data,
    mutateReleases,
  }
}
