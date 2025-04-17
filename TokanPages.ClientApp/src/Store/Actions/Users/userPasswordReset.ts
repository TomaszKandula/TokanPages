import { ApplicationAction } from "../../Configuration";
import { ResetUserPasswordDto } from "../../../Api/Models";
import { Execute, ExecuteRequest, RESET_USER_PASSWORD } from "../../../Api/Request";

export const RESET = "RESET_USER_PASSWORD";
export const CLEAR = "RESET_USER_PASSWORD_CLEAR";
export const RESPONSE = "RESET_USER_PASSWORD_RESPONSE";
interface Reset {
    type: typeof RESET;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: object;
}
export type TKnownActions = Reset | Clear | Response;

export const UserPasswordResetAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    reset:
        (payload: ResetUserPasswordDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: RESET });
            const input: ExecuteRequest = {
                url: RESET_USER_PASSWORD,
                dispatch: dispatch,
                state: getState,
                responseType: RESPONSE,
                configuration: {
                    method: "POST",
                    body: payload,
                },
            };

            Execute(input);
        },
};
