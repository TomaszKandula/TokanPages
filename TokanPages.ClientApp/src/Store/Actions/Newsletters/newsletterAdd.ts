import { ApplicationAction } from "../../Configuration";
import { AddNewsletterDto } from "../../../Api/Models";
import { ADD_NEWSLETTER, ExecuteStoreActionProps } from "../../../Api";
import { useApiAction } from "../../../Shared/Hooks";

export const ADD = "ADD_SUBSCRIBER";
export const CLEAR = "ADD_SUBSCRIBER_CLEAR";
export const RESPONSE = "ADD_SUBSCRIBER_RESPONSE";
interface Add {
    type: typeof ADD;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: object;
}
export type TKnownActions = Add | Clear | Response;

export const NewsletterAddAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    add:
        (payload: AddNewsletterDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: ADD });
            const actions = useApiAction();
            const input: ExecuteStoreActionProps = {
                url: ADD_NEWSLETTER,
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
