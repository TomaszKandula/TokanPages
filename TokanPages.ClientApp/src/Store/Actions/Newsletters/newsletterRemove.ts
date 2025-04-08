import { ApplicationAction } from "../../Configuration";
import { RemoveNewsletterDto } from "../../../Api/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, REMOVE_NEWSLETTER } from "../../../Api/Request";

export const CLEAR = "REMOVE_SUBSCRIBER_CLEAR";
export const REMOVE = "REMOVE_SUBSCRIBER_REQUEST";
export const RESPONSE = "REMOVE_SUBSCRIBER_RESPONSE";
interface Clear {
    type: typeof CLEAR;
}
interface Remove {
    type: typeof REMOVE;
}
interface Response {
    type: typeof RESPONSE;
    payload: object;
}
export type TKnownActions = Clear | Remove | Response;

export const NewsletterRemoveAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    remove:
        (payload: RemoveNewsletterDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: REMOVE });

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: REMOVE_NEWSLETTER,
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
