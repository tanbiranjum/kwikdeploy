import fetcher from "@/lib/fetcher"
import useSWR, { mutate } from "swr"
import { PaginatedListOfProjectHeadDto } from "@/lib/api/web-api-models"

export default function useProjects() {
  const url = "/backendapi/projects"

  const { data, error, isLoading } = useSWR<PaginatedListOfProjectHeadDto>(
    url,
    fetcher
  )

  const mutateProjects = async () => {
    mutate(url)
  }

  return {
    isLoading,
    isError: error,
    projects: data,
    mutateProjects,
  }
}
