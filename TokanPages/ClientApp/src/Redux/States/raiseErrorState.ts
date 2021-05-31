import { DialogType } from "../../Shared/enums";

export interface IRaiseError 
{
    defaultErrorMessage: string;
    attachedErrorObject: any;
    dialogType: DialogType;
}
