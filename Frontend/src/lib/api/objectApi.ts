import { getData } from "@/lib/api/apiHelpers";
import type { GetObjectDTO } from "@/types/objects/getObjectDTO";

export const objectApi = {

  getAll: (searchQuery?: string) : Promise<[GetObjectDTO]> =>
    getData<[GetObjectDTO]>('projects', {
      params: { searchQuery }
    }),
};