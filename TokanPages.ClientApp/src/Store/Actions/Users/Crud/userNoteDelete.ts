import { ApplicationAction } from "../../../Configuration";
import { RemoveUserNoteDto, RemoveUserNoteResultDto } from "../../../../Api/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, REMOVE_USER_NOTE } from "../../../../Api/Request";

export const DELETE = "DELETE_USER_NOTE";
export const CLEAR = "DELETE_USER_NOTE_CLEAR";
export const RESPONSE = "DELETE_USER_NOTE_RESPONSE";
interface Delete {
    type: typeof DELETE;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: RemoveUserNoteResultDto;
}
export type TKnownActions = Delete | Clear | Response;

export const UserNoteDeleteAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    delete:
        (payload: RemoveUserNoteDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: DELETE });

            const request: RequestContract = {
                configuration: {
                    method: "DELETE",
                    url: REMOVE_USER_NOTE,
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
