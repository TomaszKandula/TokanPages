import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { ISendMessage } from "../../Redux/States/sendMessageState";
import { OperationStatus } from "../../Shared/Enums";
import { 
    TKnownActions, 
    API_SEND_MESSAGE, 
    API_SEND_MESSAGE_RESPONSE, 
    API_SEND_MESSAGE_CLEAR, 
    SEND_MESSAGE_ERROR 
} from "../../Redux/Actions/sendMessageAction";

const SendMessageReducer: Reducer<ISendMessage> = (state: ISendMessage | undefined, incomingAction: Action): ISendMessage => 
{
    if (state === undefined) return combinedDefaults.sendMessage;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case API_SEND_MESSAGE_CLEAR:
            return combinedDefaults.sendMessage;
            
        case API_SEND_MESSAGE:
            return { 
                isSendingMessage: OperationStatus.inProgress, 
                hasSentMessage: state.hasSentMessage,
                attachedErrorObject: state.attachedErrorObject 
            };

        case API_SEND_MESSAGE_RESPONSE:
            return { 
                isSendingMessage: OperationStatus.hasFinished, 
                hasSentMessage: action.hasSentMessage, 
                attachedErrorObject: { } 
            };

        case SEND_MESSAGE_ERROR:
            return { 
                isSendingMessage: OperationStatus.hasFailed, 
                hasSentMessage: false, 
                attachedErrorObject: action.errorObject
            };

        default: return state;
    }
};

export default SendMessageReducer;
