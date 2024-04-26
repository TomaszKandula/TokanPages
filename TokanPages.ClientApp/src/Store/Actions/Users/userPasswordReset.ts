import { ApplicationAction } from "../../Configuration";
import { ResetUserPasswordDto } from "../../../Api/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, RESET_USER_PASSWORD } from "../../../Api/Request";

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
        dispatch => {
            dispatch({ type: RESET });

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: RESET_USER_PASSWORD,
                    data: payload,
                },
            };

            const input: ExecuteContract = {
                configuration: GetConfiguration(request),
                dispatch: dispatch,
                responseType: RESPONSE,
            };

            Execute(input);
        },
};
