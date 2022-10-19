import { DialogType } from "../../../Shared/enums";

export interface IApplicationError 
{
    errorMessage: string;
    errorDetails: any;
    dialogType: DialogType;
}
