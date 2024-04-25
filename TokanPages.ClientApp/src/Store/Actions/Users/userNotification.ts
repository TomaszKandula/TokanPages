import { ApplicationAction } from "../../Configuration";
import { NOTIFY_WEB_URL, RequestContract, ExecuteContract, Execute, GetConfiguration } from "../../../Api/Request";
import { NotificationData, NotificationRequest } from "../../../Api/Models";

export const CLEAR = "NOTIFICATION_CLEAR";
export const NOTIFY = "NOTIFICATION_NOTIFY";
export const NOTIFIED = "NOTIFICATION_NOTIFIED";
interface IClear {
    type: typeof CLEAR;
}
interface INotify {
    type: typeof NOTIFY;
}
interface INotified {
    type: typeof NOTIFIED;
    payload: NotificationData;
}
export type TKnownActions = IClear | INotify | INotified;

export const UserNotificationAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    notify:
        (payload: NotificationRequest): ApplicationAction<TKnownActions> =>
        (dispatch) => {
            dispatch({ type: NOTIFY });

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: NOTIFY_WEB_URL,
                    data: payload,
                },
            };

            const input: ExecuteContract = {
                configuration: GetConfiguration(request),
                dispatch: dispatch,
                //state: getState,//TODO: get template from state
                responseType: NOTIFIED,
            };

            Execute(input);
        },
};
