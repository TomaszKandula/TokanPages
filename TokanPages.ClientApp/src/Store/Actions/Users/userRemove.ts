import { ApplicationAction } from "../../Configuration";
import { RemoveUserDto } from "../../../Api/Models";
import { DispatchExecuteAction, ExecuteRequest, REMOVE_USER } from "../../../Api/Request";

export const REMOVE = "REMOVE_ACCOUNT";
export const CLEAR = "REMOVE_ACCOUNT_CLEAR";
export const RESPONSE = "REMOVE_ACCOUNT_RESPONSE";
interface Remove {
    type: typeof REMOVE;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: object;
}
export type TKnownActions = Remove | Clear | Response;

export const UserRemoveAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    remove:
        (payload: RemoveUserDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: REMOVE });
            const input: ExecuteRequest = {
                url: REMOVE_USER,
                dispatch: dispatch,
                state: getState,
                responseType: RESPONSE,
                configuration: {
                    method: "POST",
                    body: payload,
                    hasJsonResponse: true,
                },
            };

            DispatchExecuteAction(input);
        },
};
