import { ApplicationAction } from "../../Configuration";
import { UpdateSubscriberDto } from "../../../Api/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, UPDATE_SUBSCRIBER } from "../../../Api/Request";

export const UPDATE = "UPDATE_SUBSCRIBER";
export const RESPONSE = "UPDATE_SUBSCRIBER_RESPONSE";
interface Update {
    type: typeof UPDATE;
}
interface Response {
    type: typeof RESPONSE;
    payload: any;
}
export type TKnownActions = Update | Response;

export const SubscriberUpdateAction = {
    update:
        (payload: UpdateSubscriberDto): ApplicationAction<TKnownActions> =>
        dispatch => {
            dispatch({ type: UPDATE });

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: UPDATE_SUBSCRIBER,
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
