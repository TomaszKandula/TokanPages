import { ApplicationAction } from "../../Configuration";
import { RemoveNewsletterDto } from "../../../Api/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, REMOVE_NEWSLETTER } from "../../../Api/Request";

export const REMOVE = "REMOVE_SUBSCRIBER";
export const RESPONSE = "REMOVE_SUBSCRIBER_RESPONSE";
interface Remove {
    type: typeof REMOVE;
}
interface Response {
    type: typeof RESPONSE;
    payload: any;
}
export type TKnownActions = Remove | Response;

export const NewsletterRemoveAction = {
    remove:
        (payload: RemoveNewsletterDto): ApplicationAction<TKnownActions> =>
        dispatch => {
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
                responseType: RESPONSE,
            };

            Execute(input);
        },
};
