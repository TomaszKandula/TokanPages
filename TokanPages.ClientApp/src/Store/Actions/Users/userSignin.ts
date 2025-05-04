import { ApplicationAction } from "../../Configuration";
import { AuthenticateUserDto } from "../../../Api/Models";
import { UPDATE, TKnownActions as TUpdateActions } from "./userDataStore";
import { AUTHENTICATE as AUTHENTICATE_USER, ExecuteStoreActionProps } from "../../../Api";
import { useApiAction } from "../../../Shared/Hooks";

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
            const actions = useApiAction();
            const input: ExecuteStoreActionProps = {
                url: AUTHENTICATE_USER,
                dispatch: dispatch,
                state: getState,
                responseType: [RESPONSE, UPDATE],
                configuration: {
                    method: "POST",
                    body: payload,
                    hasJsonResponse: true,
                },
            };

            actions.storeAction(input);
        },
};
