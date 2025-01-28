import { UserNoteActionResult } from "../../../Shared/enums";

export interface AddUserNoteResultDto {
    id: string;
    createdBy: string;
    createdAt: string;
    currentNotes: number;
    result?: UserNoteActionResult;
}
