export interface CreateDefectDTO
{
    title: string,
    description?: string
    projectId: number,
    priorityId: number,
    deadline?: string,
}