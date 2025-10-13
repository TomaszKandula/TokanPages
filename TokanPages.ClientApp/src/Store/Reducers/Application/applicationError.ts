import { Action, Reducer } from "redux";
import { ApplicationErrorState } from "../../States";
import { ApplicationDefault } from "../../Configuration";
import { DialogType } from "../../../Shared/Enums";

import { CLEAR, RAISE, TErrorActions } from "../../Actions/Application/applicationError";

import { NO_ERRORS, RECEIVED_ERROR_MESSAGE } from "../../../Shared/ConstantsTemp";

export const ApplicationError: Reducer<ApplicationErrorState> = (
    state: ApplicationErrorState | undefined,
    incomingAction: Action
): ApplicationErrorState => {
    if (state === undefined) return ApplicationDefault.applicationError;

    const action = incomingAction as TErrorActions;
    switch (action.type) {
        case CLEAR:
            return {
                errorMessage: NO_ERRORS,
                errorDetails: undefined,
                dialogType: DialogType.toast,
            };
        case RAISE:
            return {
                errorMessage: RECEIVED_ERROR_MESSAGE,
                errorDetails: action.errorDetails,
                dialogType: action.dialogType ?? DialogType.toast,
            };
        default:
            return state;
    }
};
