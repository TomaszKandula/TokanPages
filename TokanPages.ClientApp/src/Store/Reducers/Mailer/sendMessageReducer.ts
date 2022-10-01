import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { ISendMessage } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions, 
    API_SEND_MESSAGE, 
    API_SEND_MESSAGE_RESPONSE, 
    API_SEND_MESSAGE_CLEAR
} from "../../Actions/Mailer/sendMessageAction";

export const SendMessageReducer: 
    Reducer<ISendMessage> = (state: ISendMessage | undefined, incomingAction: Action): 
    ISendMessage => 
{
    if (state === undefined) return ApplicationDefaults.applicationMessage;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case API_SEND_MESSAGE_CLEAR:
            return ApplicationDefaults.applicationMessage;
            
        case API_SEND_MESSAGE:
            return { 
                operationStatus: OperationStatus.inProgress, 
                attachedErrorObject: state.attachedErrorObject 
            };

        case API_SEND_MESSAGE_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished, 
                attachedErrorObject: { } 
            };

        default: return state;
    }
};