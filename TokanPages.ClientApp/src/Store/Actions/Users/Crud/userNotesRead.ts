import { ApplicationAction } from "../../../Configuration";
import { UserNotesDto, UserNotesResultDto } from "../../../../Api/Models";
import { ExecuteStoreAction, ExecuteStoreActionProps, GET_USER_NOTES } from "../../../../Api";

export const RECEIVE = "GET_USER_NOTES_RECEIVE";
export const RESPONSE = "GET_USER_NOTES_RESPONSE";
export const CLEAR = "GET_USER_NOTES_CLEAR";
interface Receive {
    type: typeof RECEIVE;
}
interface Response {
    type: typeof RESPONSE;
    payload: UserNotesResultDto;
}
interface Clear {
    type: typeof CLEAR;
}
export type TKnownActions = Receive | Response | Clear;

export const UserNotesReadAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    get:
        (payload: UserNotesDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: RECEIVE });
            const input: ExecuteStoreActionProps = {
                url: `${GET_USER_NOTES}?noCache=${payload.noCache ?? false}`,
                dispatch: dispatch,
                state: getState,
                responseType: RESPONSE,
                configuration: {
                    method: "GET",
                    hasJsonResponse: true,
                },
            };

            ExecuteStoreAction(input);
        },
};
