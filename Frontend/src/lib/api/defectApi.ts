import { getData, postData, putData } from "@/lib/api/apiHelpers";
import type { AppointmentDTO } from "@/types/defects/appointmentDTO";
import type { CreateDefectDTO } from "@/types/defects/createDefectDTO";
import type { GetDefectDTO } from "@/types/defects/getDefectDTO";
import type { UpdateDefectDTO } from "@/types/defects/updateDefectDTO";
import type { idDTO } from "@/types/idDTO";

export const defectApi = {

  getByProject: (projectId: number, searchQuery?: string) : Promise<[GetDefectDTO]> =>
    getData<[GetDefectDTO]>(`defects/project/${projectId}`, {
      params: { searchQuery },
    }),

  getById: (defectId: number) : Promise<GetDefectDTO> =>
    getData<GetDefectDTO>(`defects/${defectId}`),

  create: (payload: CreateDefectDTO) : Promise<idDTO> =>
    postData<idDTO, CreateDefectDTO>('defects', payload),

  update: (payload: UpdateDefectDTO) : Promise<idDTO> =>
    putData<idDTO, UpdateDefectDTO>('defects', payload),

  appointment: (payload: AppointmentDTO) : Promise<idDTO> =>
    postData<idDTO, AppointmentDTO>('defects/appointment', payload),
};