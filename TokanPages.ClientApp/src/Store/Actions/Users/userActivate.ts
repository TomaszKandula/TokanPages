import { ApplicationAction } from "../../Configuration";
import { ActivateUserDto, ActivateUserResultDto } from "../../../Api/Models";
import { DispatchExecuteAction, ACTIVATE_USER, ExecuteRequest } from "../../../Api";

export const ACTIVATE = "ACTIVATE_ACCOUNT";
export const CLEAR = "ACTIVATE_ACCOUNT_CLEAR";
export const RESPONSE = "ACTIVATE_ACCOUNT_RESPONSE";
interface Activate {
    type: typeof ACTIVATE;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: ActivateUserResultDto;
}
export type TKnownActions = Activate | Clear | Response;

export const UserActivateAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    activate:
        (payload: ActivateUserDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: ACTIVATE });
            const input: ExecuteRequest = {
                url: ACTIVATE_USER,
                dispatch: dispatch,
                state: getState,
                responseType: RESPONSE,
                configuration: {
                    method: "POST",
                    body: payload,
                    hasJsonResponse: true,
                },
            };

            DispatchExecuteAction(input);
        },
};
