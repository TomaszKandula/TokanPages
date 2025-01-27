import { UserNoteActionResult } from "../../../Shared/enums";

export interface AddUserNoteResultDto {
    currentNotes: number;
    result?: UserNoteActionResult;
}
