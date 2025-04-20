import { ApplicationAction } from "../../Configuration";
import { UpdateNewsletterDto } from "../../../Api/Models";
import { DispatchExecuteAction, ExecuteRequest, UPDATE_NEWSLETTER } from "../../../Api";

export const UPDATE = "UPDATE_SUBSCRIBER";
export const RESPONSE = "UPDATE_SUBSCRIBER_RESPONSE";
interface Update {
    type: typeof UPDATE;
}
interface Response {
    type: typeof RESPONSE;
    payload: object;
}
export type TKnownActions = Update | Response;

export const NewsletterUpdateAction = {
    update:
        (payload: UpdateNewsletterDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: UPDATE });
            const input: ExecuteRequest = {
                url: UPDATE_NEWSLETTER,
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
