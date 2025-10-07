export interface GetDefectDTO
{
    id: number,
    title: string,
    description?: string,
    statusId: number,
    statusName: string,
    createdAt: string,
    updatedAt?: string,
    priorityId: number,
    priorityName: string,
    deadline?: string,
    creatorId: number,
    executorId: number,
}