import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { SubscriberUpdateState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { TKnownActions, UPDATE, RESPONSE } from "../../Actions/Subscribers/subscriberUpdate";

export const SubscriberUpdate: Reducer<SubscriberUpdateState> = (
    state: SubscriberUpdateState | undefined,
    incomingAction: Action
): SubscriberUpdateState => {
    if (state === undefined) return ApplicationDefault.subscriberUpdate;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case UPDATE:
            return {
                status: OperationStatus.notStarted,
                response: state.response,
            };

        case RESPONSE:
            return {
                status: OperationStatus.hasFinished,
                response: action.payload,
            };

        default:
            return state;
    }
};
