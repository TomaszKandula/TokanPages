import { ApplicationAction } from "../../../Configuration";
import { AddUserNoteDto, AddUserNoteResultDto } from "../../../../Api/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, ADD_USER_NOTE } from "../../../../Api/Request";

export const ADD = "ADD_USER_NOTE";
export const CLEAR = "ADD_USER_NOTE_CLEAR";
export const RESPONSE = "ADD_USER_NOTE_RESPONSE";
interface Add {
    type: typeof ADD;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: AddUserNoteResultDto;
}
export type TKnownActions = Add | Clear | Response;

export const UserNoteCreateAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    add:
        (payload: AddUserNoteDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: ADD });

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: ADD_USER_NOTE,
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
