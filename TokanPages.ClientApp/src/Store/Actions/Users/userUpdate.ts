import { ApplicationAction } from "../../Configuration";
import { UpdateUserDto, UpdateUserResultDto } from "../../../Api/Models";
import { DispatchExecuteAction, ExecuteRequest, UPDATE_USER } from "../../../Api";

export const UPDATE = "UPDATE_USER";
export const CLEAR = "UPDATE_USER_CLEAR";
export const RESPONSE = "UPDATE_USER_RESPONSE";
interface Update {
    type: typeof UPDATE;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: UpdateUserResultDto;
}
export type TKnownActions = Update | Clear | Response;

export const UserUpdateAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    update:
        (payload: UpdateUserDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: UPDATE });

            const input: ExecuteRequest = {
                url: UPDATE_USER,
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
