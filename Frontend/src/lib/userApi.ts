import { postData } from "@/lib/apiHelpers";
import type { CreateUserDTO } from "@/types/user/createUserDTO";
import type { RegisterAnswerDTO } from "@/types/user/registerAnswerDTO";

export const userApi = {
  create: (payload: CreateUserDTO) : Promise<RegisterAnswerDTO> =>
    postData<RegisterAnswerDTO, CreateUserDTO>('users/register', payload)
};