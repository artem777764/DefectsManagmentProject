import { postData, putData } from "@/lib/apiHelpers";
import type { AuthorizeAnswerDTO } from "@/types/user/authorizeAnswerDTO";
import type { AuthorizeDTO } from "@/types/user/authorizeDTO";
import type { CreateUserDataDTO } from "@/types/user/createUserDataDTO";
import type { CreateUserDTO } from "@/types/user/createUserDTO";
import type { RegisterAnswerDTO } from "@/types/user/registerAnswerDTO";
import type { UpdateDataAnswerDTO } from "@/types/user/updateDataAnswerDTO";

export const userApi = {

  create: (payload: CreateUserDTO) : Promise<RegisterAnswerDTO> =>
    postData<RegisterAnswerDTO, CreateUserDTO>('users/register', payload),

  login: (payload: AuthorizeDTO) : Promise<AuthorizeAnswerDTO> =>
    postData<AuthorizeAnswerDTO, AuthorizeDTO>('users/login', payload),

  updateData: (payload: CreateUserDataDTO) : Promise<UpdateDataAnswerDTO> =>
    putData<UpdateDataAnswerDTO, CreateUserDataDTO>('users/data', payload),

  logout: (): Promise<void> =>
    postData<void, undefined>('users/logout'),
};