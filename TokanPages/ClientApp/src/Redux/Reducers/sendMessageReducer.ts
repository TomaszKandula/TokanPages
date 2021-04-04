import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { ISendMessage } from "../../Redux/States/sendMessageState";
import { TKnownActions, API_SEND_MESSAGE, API_SEND_MESSAGE_RESPONSE, API_SEND_MESSAGE_CLEAR } from "../../Redux/Actions/sendMessageAction";
import { OperationStatuses } from "../../Shared/Enums";

const SendMessageReducer: Reducer<ISendMessage> = (state: ISendMessage | undefined, incomingAction: Action): ISendMessage => 
{
    if (state === undefined) return combinedDefaults.sendMessage;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case API_SEND_MESSAGE_CLEAR:
            return combinedDefaults.sendMessage;
            
        case API_SEND_MESSAGE:
            return { isSendingMessage: OperationStatuses.inProgress, hasSentMessage: state.hasSentMessage };

        case API_SEND_MESSAGE_RESPONSE:
            return { isSendingMessage: OperationStatuses.hasFinished, hasSentMessage: action.hasSentMessage };

        default: return state;
    }
};

export default SendMessageReducer;
