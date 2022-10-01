import { DialogType } from "../../Shared/enums";

export interface IApplicationError 
{
    defaultErrorMessage: string;
    attachedErrorObject: any;
    dialogType: DialogType;
}
