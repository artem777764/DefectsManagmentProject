export interface GetDefectDTO
{
    id: number,
    title: string,
    description?: string,
    statusName: string,
    createdAt: string,
    updatedAt?: string,
    priorityName: string,
    deadline?: string,
    creatorId: number,
    executorId: number,
}