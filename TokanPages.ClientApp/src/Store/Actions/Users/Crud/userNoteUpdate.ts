import { ApplicationAction } from "../../../Configuration";
import { UpdateUserNoteDto, UpdateUserNoteResultDto } from "../../../../Api/Models";
import { ExecuteStoreActionProps, UPDATE_USER_NOTE } from "../../../../Api";
import { useApiAction } from "../../../../Shared/Hooks";

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
            const actions = useApiAction();
            const input: ExecuteStoreActionProps = {
                url: UPDATE_USER_NOTE,
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
