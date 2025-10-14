import { UserNoteActionResult } from "../../../Shared/Enums";

export interface AddUserNoteResultDto {
    id: string;
    createdBy: string;
    createdAt: string;
    currentNotes: number;
    result?: UserNoteActionResult;
}
