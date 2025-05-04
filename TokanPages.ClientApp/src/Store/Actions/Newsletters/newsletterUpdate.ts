import { ApplicationAction } from "../../Configuration";
import { UpdateNewsletterDto } from "../../../Api/Models";
import { ExecuteStoreActionProps, UPDATE_NEWSLETTER } from "../../../Api";
import { useApiAction } from "../../../Shared/Hooks";

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
            const actions = useApiAction();
            const input: ExecuteStoreActionProps = {
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

            actions.storeAction(input);
        },
};
