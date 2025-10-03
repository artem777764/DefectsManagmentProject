import { getData } from "@/lib/api/apiHelpers";
import type { GetObjectDTO } from "@/types/user/getObjectDTO";

export const objectApi = {

  getAll: (searchQuery?: string) : Promise<[GetObjectDTO]> =>
    getData<[GetObjectDTO]>('projects', {
      params: { searchQuery }
    }),
};