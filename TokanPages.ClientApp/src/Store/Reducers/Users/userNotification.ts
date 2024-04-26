import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserNotificationState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { TKnownActions, NOTIFY, CLEAR, NOTIFIED } from "../../Actions/Users/userNotification";

export const UserNotification: Reducer<UserNotificationState> = (
    state: UserNotificationState | undefined,
    incomingAction: Action
): UserNotificationState => {
    if (state === undefined) return ApplicationDefault.userNotification;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR: {
            return {
                status: OperationStatus.notStarted,
                response: {
                    userId: "",
                    handler: "",
                    payload: { },
                },
            };
        }

        case NOTIFY: {
            return {
                status: OperationStatus.inProgress,
                response: state.response,
            };
        }

        case NOTIFIED: {
            return {
                status: OperationStatus.hasFinished,
                response: action.payload,
            };
        }

        default:
            return state;
    }
};
