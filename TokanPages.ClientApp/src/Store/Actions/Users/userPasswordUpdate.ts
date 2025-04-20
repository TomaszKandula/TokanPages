import { ApplicationAction } from "../../Configuration";
import { UpdateUserPasswordDto } from "../../../Api/Models";
import { ExecuteStoreAction, ExecuteStoreActionProps, UPDATE_USER_PASSWORD } from "../../../Api";

export const UPDATE = "UPDATE_USER_PASSWORD";
export const CLEAR = "UPDATE_USER_PASSWORD_CLEAR";
export const RESPONSE = "UPDATE_USER_PASSWORD_RESPONSE";
interface Update {
    type: typeof UPDATE;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: object;
}
export type TKnownActions = Update | Clear | Response;

export const UserPasswordUpdateAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    update:
        (payload: UpdateUserPasswordDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: UPDATE });
            const input: ExecuteStoreActionProps = {
                url: UPDATE_USER_PASSWORD,
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
