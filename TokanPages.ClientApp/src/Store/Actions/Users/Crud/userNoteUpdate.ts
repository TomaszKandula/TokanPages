import { ApplicationAction } from "../../../Configuration";
import { UpdateUserNoteDto, UpdateUserNoteResultDto } from "../../../../Api/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, UPDATE_USER_NOTE } from "../../../../Api/Request";

export const UPDATE = "UPDATE_USER_NOTE";
export const CLEAR = "UPDATE_USER_NOTE_CLEAR";
export const RESPONSE = "UPDATE_USER_NOTE_RESPONSE";
interface Update {
    type: typeof UPDATE;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: UpdateUserNoteResultDto;
}
export type TKnownActions = Update | Clear | Response;

export const UserNoteUpdateAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    update:
        (payload: UpdateUserNoteDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: UPDATE });

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: UPDATE_USER_NOTE,
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
