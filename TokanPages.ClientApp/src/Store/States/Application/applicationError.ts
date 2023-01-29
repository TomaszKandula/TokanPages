import { DialogType } from "../../../Shared/enums";

export interface ApplicationErrorState
{
    errorMessage: string;
    errorDetails: any;
    dialogType: DialogType;
}
