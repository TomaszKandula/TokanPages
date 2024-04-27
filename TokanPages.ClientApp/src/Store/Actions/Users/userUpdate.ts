import { ApplicationAction } from "../../Configuration";
import { UpdateUserDto } from "../../../Api/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, UPDATE_USER } from "../../../Api/Request";

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
    payload: object;
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

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: UPDATE_USER,
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
