import fetcher from "@/lib/fetcher"
import useSWR, { mutate } from "swr"
import { PaginatedListOfEnvHeadDto } from "@/lib/api/web-api-models"

export default function useEnvs(projectId: string) {
  const url = `/backendapi/envs/${projectId}`

  const { data, error, isLoading } = useSWR<PaginatedListOfEnvHeadDto>(
    url,
    fetcher
  )

  const mutateEnvs = async () => {
    mutate(url)
  }

  return {
    isLoading,
    isError: error,
    envs: data,
    mutateEnvs,
  }
}
