import fetcher from "@/lib/fetcher"
import useSWR, { mutate } from "swr"
import { IAppDefDto } from "@/lib/api/web-api-models"

export default function useAppDef(projectId: string, appDefId: string) {
  const url = `/backendapi/appdefs/${projectId}/${appDefId}`

  const { data, error, isLoading } = useSWR<IAppDefDto>(url, fetcher)

  const mutateAppDef = async () => {
    mutate(url)
  }

  return {
    isLoading,
    isError: error,
    appDef: data,
    mutateAppDef,
  }
}
