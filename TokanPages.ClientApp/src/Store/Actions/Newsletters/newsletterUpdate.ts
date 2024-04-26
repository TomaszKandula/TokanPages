import { ApplicationAction } from "../../Configuration";
import { UpdateNewsletterDto } from "../../../Api/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, UPDATE_NEWSLETTER } from "../../../Api/Request";

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
        dispatch => {
            dispatch({ type: UPDATE });

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: UPDATE_NEWSLETTER,
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
