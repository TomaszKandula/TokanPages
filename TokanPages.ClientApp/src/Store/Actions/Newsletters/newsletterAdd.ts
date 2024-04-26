import { ApplicationAction } from "../../Configuration";
import { AddNewsletterDto } from "../../../Api/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, ADD_NEWSLETTER } from "../../../Api/Request";

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
        dispatch => {
            dispatch({ type: ADD });

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: ADD_NEWSLETTER,
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
