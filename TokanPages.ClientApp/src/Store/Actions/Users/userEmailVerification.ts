import { ApplicationAction } from "../../Configuration";
import { VerifyUserEmailDto } from "../../../Api/Models";
import { VERIFY_USER_EMAIL, DispatchExecuteAction, ExecuteRequest } from "../../../Api/Request";

export const VERIFY = "VERIFY_USER_EMAIL";
export const CLEAR = "VERIFY_USER_EMAIL_CLEAR";
export const RESPONSE = "VERIFY_USER_EMAIL_RESPONSE";
interface IVerify {
    type: typeof VERIFY;
}
interface IClear {
    type: typeof CLEAR;
}
interface IResponse {
    type: typeof RESPONSE;
    payload: any;
}
export type TKnownActions = IVerify | IClear | IResponse;

export const UserEmailVerificationAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    verify:
        (payload: VerifyUserEmailDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: VERIFY });
            const input: ExecuteRequest = {
                url: VERIFY_USER_EMAIL,
                dispatch: dispatch,
                state: getState,
                responseType: RESPONSE,
                configuration: {
                    method: "POST",
                    hasJsonResponse: true,
                    body: { 
                        emailAddress: payload.emailAddress 
                    },
                },
            };

            DispatchExecuteAction(input);
        },
};
