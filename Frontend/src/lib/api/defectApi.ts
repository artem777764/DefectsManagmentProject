import { getData } from "@/lib/api/apiHelpers";
import type { GetDefectDTO } from "@/types/defects/getDefectDTO";

export const defectApi = {

  getByProject: (projectId: number, searchQuery?: string) : Promise<[GetDefectDTO]> =>
    getData<[GetDefectDTO]>(`defects/project/${projectId}`, {
      params: { searchQuery },
    }),
};