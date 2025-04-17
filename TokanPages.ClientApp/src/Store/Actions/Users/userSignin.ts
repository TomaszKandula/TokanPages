import { ApplicationAction } from "../../Configuration";
import { AuthenticateUserDto } from "../../../Api/Models";
import { UPDATE, TKnownActions as TUpdateActions } from "./userDataStore";
import { AUTHENTICATE as AUTHENTICATE_USER, Execute, ExecuteRequest } from "../../../Api/Request";

export const SIGNIN = "SIGNIN_USER";
export const CLEAR = "SIGNIN_USER_CLEAR";
export const RESPONSE = "SIGNIN_USER_RESPONSE";
interface Signin {
    type: typeof SIGNIN;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: object;
}
export type TKnownActions = Signin | Clear | Response | TUpdateActions;

export const UserSigninAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    signin:
        (payload: AuthenticateUserDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: SIGNIN });
            const input: ExecuteRequest = {
                url: AUTHENTICATE_USER,
                dispatch: dispatch,
                state: getState,
                responseType: [RESPONSE, UPDATE],
                configuration: {
                    method: "POST",
                    body: payload,
                },
            };

            Execute(input);
        },
};
