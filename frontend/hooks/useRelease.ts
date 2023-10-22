import fetcher from "@/lib/fetcher"
import useSWR, { mutate } from "swr"
import { IReleaseDto } from "@/lib/api/web-api-models"

export default function useRelease(projectId: string, releaseId: string) {
  const url = `/backendapi/releases/${projectId}/${releaseId}`

  const { data, error, isLoading } = useSWR<IReleaseDto>(url, fetcher)

  const mutateRelease = async () => {
    mutate(url)
  }

  return {
    isLoading,
    isError: error,
    release: data,
    mutateRelease,
  }
}
