import fetcher from "@/lib/fetcher"
import useSWR, { mutate } from "swr"
import { PaginatedListOfAppDefHeadDto } from "@/lib/api/web-api-models"

export default function useAppDefs(projectId: string) {
  const url = `/backendapi/appdefs/${projectId}`

  const { data, error, isLoading } = useSWR<PaginatedListOfAppDefHeadDto>(
    url,
    fetcher
  )

  const mutateAppDefs = async () => {
    mutate(url)
  }

  return {
    isLoading,
    isError: error,
    appDefs: data,
    mutateAppDefs,
  }
}
