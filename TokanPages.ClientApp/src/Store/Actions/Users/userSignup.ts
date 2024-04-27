import { ApplicationAction } from "../../Configuration";
import { AddUserDto } from "../../../Api/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, ADD_USER } from "../../../Api/Request";

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

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: ADD_USER,
                    data: payload,
                },
            };

            const input: ExecuteContract = {
                configuration: GetConfiguration(request),
                dispatch: dispatch,
                state: getState,
                responseType: RESPONSE,
            };

            Execute(input);
        },
};
