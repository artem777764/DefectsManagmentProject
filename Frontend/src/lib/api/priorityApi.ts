import { getData } from "@/lib/api/apiHelpers";
import type { Option } from "@/types/option";

export const priorityApi = {

  getPriorities: (): Promise<Option[]> =>
    getData<Option[]>('priorities'),
};