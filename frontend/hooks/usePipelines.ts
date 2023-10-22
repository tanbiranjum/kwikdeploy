import fetcher from "@/lib/fetcher"
import useSWR, { mutate } from "swr"
import { PaginatedListOfPipelineHeadDto } from "@/lib/api/web-api-models"

export default function usePipelines(projectId: string) {
  const url = `/backendapi/pipelines/${projectId}`

  const { data, error, isLoading } = useSWR<PaginatedListOfPipelineHeadDto>(
    url,
    fetcher
  )

  const mutatePipelines = async () => {
    mutate(url)
  }

  return {
    isLoading,
    isError: error,
    pipelines: data,
    mutatePipelines,
  }
}
