import { ApplicationAction } from "../../Configuration";
import { ReAuthenticateUserDto } from "../../../Api/Models";
import { UPDATE, TKnownActions as TUpdateActions } from "./userDataStore";
import { REAUTHENTICATE as REAUTHENTICATE_USER, ExecuteStoreActionProps } from "../../../Api";
import { useApiAction } from "../../../Shared/Hooks";

export const REAUTHENTICATE = "REAUTHENTICATE_USER";
export const CLEAR = "REAUTHENTICATE_USER_CLEAR";
export const RESPONSE = "REAUTHENTICATE_USER_RESPONSE";
interface ReAuthenticate {
    type: typeof REAUTHENTICATE;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: object;
}
export type TKnownActions = ReAuthenticate | Clear | Response | TUpdateActions;

export const UserReAuthenticateAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    reAuthenticate:
        (refreshToken: string, userId: string): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: REAUTHENTICATE });
            const payload: ReAuthenticateUserDto = {
                userId: userId,
                refreshToken: refreshToken,
            };

            const actions = useApiAction();
            const input: ExecuteStoreActionProps = {
                url: REAUTHENTICATE_USER,
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
