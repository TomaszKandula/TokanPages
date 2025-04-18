import { ApplicationAction } from "../../Configuration";
import { SendMessageDto } from "../../../Api/Models";
import { DispatchExecuteAction, ExecuteRequest, SEND_MESSAGE } from "../../../Api/Request";

export const SEND = "SEND_MESSAGE";
export const CLEAR = "SEND_MESSAGE_CLEAR";
export const RESPONSE = "SEND_MESSAGE_RESPONSE";
interface Send {
    type: typeof SEND;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: object;
}
export type TKnownActions = Send | Clear | Response;

export const ApplicationMessageAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    send:
        (payload: SendMessageDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: SEND });

            const input: ExecuteRequest = {
                url: SEND_MESSAGE,
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
