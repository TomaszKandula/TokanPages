import { Action, Reducer } from "redux";
import { SendMessageDefaultValues } from "../../Redux/Defaults/sendMessageDefault";
import { ISendMessage } from "../../Redux/States/sendMessageState";
import { TKnownActions, API_SEND_MESSAGE, API_SEND_MESSAGE_RESPONSE } from "../../Redux/Actions/sendMessageAction";

const SendMessageReducer: Reducer<ISendMessage> = (state: ISendMessage | undefined, incomingAction: Action): ISendMessage => 
{
    if (state === undefined) return SendMessageDefaultValues;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case API_SEND_MESSAGE:
            return { isSendingMessage: true, hasSentMessage: state.hasSentMessage };

        case API_SEND_MESSAGE_RESPONSE:
            return { isSendingMessage: false, hasSentMessage: action.hasSentMessage };

        default: return state;
    }
};

export default SendMessageReducer;
