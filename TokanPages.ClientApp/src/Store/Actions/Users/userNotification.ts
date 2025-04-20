import { ApplicationAction } from "../../Configuration";
import { NotificationData, NotificationRequest } from "../../../Api/Models";
import { NOTIFY_WEB_URL, ExecuteStoreAction, ExecuteStoreActionProps } from "../../../Api";

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
        (dispatch, getState) => {
            dispatch({ type: NOTIFY });
            const input: ExecuteStoreActionProps = {
                url: NOTIFY_WEB_URL,
                dispatch: dispatch,
                state: getState,
                responseType: NOTIFIED,
                configuration: {
                    method: "POST",
                    body: payload,
                    hasJsonResponse: true,
                },
            };

            ExecuteStoreAction(input);
        },
};
