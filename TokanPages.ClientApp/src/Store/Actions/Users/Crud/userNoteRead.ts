import { ApplicationAction } from "../../../Configuration";
import { UserNoteDto, UserNoteResultDto } from "../../../../Api/Models";
import { DispatchExecuteAction, ExecuteRequest, GET_USER_NOTE } from "../../../../Api";

export const RECEIVE = "GET_USER_NOTE_RECEIVE";
export const RESPONSE = "GET_USER_NOTE_RESPONSE";
export const CLEAR = "GET_USER_NOTE_CLEAR";
interface Receive {
    type: typeof RECEIVE;
}
interface Response {
    type: typeof RESPONSE;
    payload: UserNoteResultDto;
}
interface Clear {
    type: typeof CLEAR;
}
export type TKnownActions = Receive | Response | Clear;

export const UserNoteReadAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    get:
        (payload: UserNoteDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: RECEIVE });

            const baseUrl = GET_USER_NOTE.replace("{id}", payload.id);
            const input: ExecuteRequest = {
                url: `${baseUrl}?noCache=${payload.noCache ?? false}`,
                dispatch: dispatch,
                state: getState,
                responseType: RESPONSE,
                configuration: {
                    method: "GET",
                    hasJsonResponse: true,
                },
            };

            DispatchExecuteAction(input);
        },
};
