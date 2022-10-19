import { IApplicationError } from "../../States";
import { DialogType } from "../../../Shared/enums";
import { NO_ERRORS } from "../../../Shared/constants";

export const ApplicationError: IApplicationError = 
{
    errorMessage: NO_ERRORS,
    errorDetails: { },
    dialogType: DialogType.toast
}
