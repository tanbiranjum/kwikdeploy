import fetcher from "@/lib/fetcher"
import useSWR, { mutate } from "swr"
import { IEnvDto } from "@/lib/api/web-api-models"

export default function useEnv(projectId: string, envId: string) {
  const url = `/backendapi/envs/${projectId}/${envId}`

  const { data, error, isLoading } = useSWR<IEnvDto>(url, fetcher)

  const mutateEnv = async () => {
    mutate(url)
  }

  return {
    isLoading,
    isError: error,
    env: data,
    mutateEnv,
  }
}
