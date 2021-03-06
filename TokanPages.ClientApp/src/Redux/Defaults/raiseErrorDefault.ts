import { IRaiseError } from "../States/raiseErrorState";
import { DialogType } from "../../Shared/enums";
import { NO_ERRORS } from "../../Shared/constants";

export const RaiseErrorDefault: IRaiseError = 
{
    defaultErrorMessage: NO_ERRORS,
    attachedErrorObject: { },
    dialogType: DialogType.toast
}
