import { DialogType } from "../../../Shared/Enums";

export interface ApplicationErrorState {
    errorMessage: string;
    errorDetails?: string;
    dialogType: DialogType;
}
