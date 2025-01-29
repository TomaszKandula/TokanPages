export interface UserNoteResultDto {
    id: string;
    note: string;
    createdBy: string;
    createdAt: string;
    modifiedBy?: string;
    modifiedAt?: string;
}
