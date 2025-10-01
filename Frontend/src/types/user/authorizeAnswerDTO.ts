export interface AuthorizeAnswerDTO {
    successful: boolean,
    hasData: boolean,
    userId?: number,
    roleId?: number,
    jwtToken?: string,
    jwtCookieName?: string,
    expireHours?: number,
    message: string,
}