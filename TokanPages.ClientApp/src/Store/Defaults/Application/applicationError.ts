import { ApplicationErrorState } from "../../States";
import { DialogType } from "../../../Shared/Enums";
import { NO_ERRORS } from "../../../Shared/constants";

export const ApplicationError: ApplicationErrorState = {
    errorMessage: NO_ERRORS,
    errorDetails: undefined,
    dialogType: DialogType.toast,
};
