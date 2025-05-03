import { ApplicationAction } from "../../Configuration";
import { UpdateUserPasswordDto } from "../../../Api/Models";
import { ExecuteStoreActionProps, UPDATE_USER_PASSWORD } from "../../../Api";
import { useApiAction } from "../../../Shared/Hooks";

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
            const actions = useApiAction();
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

            actions.storeAction(input);
        },
};
