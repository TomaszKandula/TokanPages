import { ApplicationAction } from "../../Configuration";
import { AddUserDto } from "../../../Api/Models";
import { ExecuteStoreAction, ADD_USER, ExecuteStoreActionProps } from "../../../Api";

export const SIGNUP = "SIGNUP_USER";
export const CLEAR = "SIGNUP_USER_CLEAR";
export const RESPONSE = "SIGNUP_USER_RESPONSE";
interface Signup {
    type: typeof SIGNUP;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: object;
}
export type TKnownActions = Signup | Clear | Response;

export const UserSignupAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    signup:
        (payload: AddUserDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: SIGNUP });

            const input: ExecuteStoreActionProps = {
                url: ADD_USER,
                dispatch: dispatch,
                state: getState,
                responseType: RESPONSE,
                configuration: {
                    method: "POST",
                    body: payload,
                    hasJsonResponse: true,
                },
            };

            ExecuteStoreAction(input);
        },
};
