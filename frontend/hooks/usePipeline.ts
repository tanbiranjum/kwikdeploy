import fetcher from "@/lib/fetcher"
import useSWR, { mutate } from "swr"
import { IPipelineDto } from "@/lib/api/web-api-models"

export default function usePipeline(projectId: string, pipelineId: string) {
  const url = `/backendapi/pipelines/${projectId}/${pipelineId}`

  const { data, error, isLoading } = useSWR<IPipelineDto>(url, fetcher)

  const mutatePipeline = async () => {
    mutate(url)
  }

  return {
    isLoading,
    isError: error,
    pipeline: data,
    mutatePipeline,
  }
}
