import { IApplicationError } from "../States";
import { DialogType } from "../../Shared/enums";
import { NO_ERRORS } from "../../Shared/constants";

export const ApplicationError: IApplicationError = 
{
    defaultErrorMessage: NO_ERRORS,
    attachedErrorObject: { },
    dialogType: DialogType.toast
}
