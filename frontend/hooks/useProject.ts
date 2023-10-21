import fetcher from "@/lib/fetcher"
import useSWR, { mutate } from "swr"
import { IProjectDto, ITargetDto } from "@/lib/api/web-api-models"

export default function useProject(projectId: string) {
  const url = `/backendapi/projects/${projectId}`

  const { data, error, isLoading } = useSWR<IProjectDto>(url, fetcher)

  const mutateProject = async () => {
    mutate(url)
  }

  return {
    isLoading,
    isError: error,
    project: data,
    mutateTarget: mutateProject,
  }
}
