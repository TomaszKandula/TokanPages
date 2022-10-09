import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IApplicationMessage } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions, 
    SEND_MESSAGE, 
    SEND_MESSAGE_RESPONSE, 
    SEND_MESSAGE_CLEAR
} from "../../Actions/Application/applicationMessage";

export const ApplicationMessage: 
    Reducer<IApplicationMessage> = (state: IApplicationMessage | undefined, incomingAction: Action): 
    IApplicationMessage => 
{
    if (state === undefined) return ApplicationDefault.applicationMessage;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case SEND_MESSAGE_CLEAR:
            return ApplicationDefault.applicationMessage;
            
        case SEND_MESSAGE:
            return { 
                operationStatus: OperationStatus.inProgress, 
                attachedErrorObject: state.attachedErrorObject 
            };

        case SEND_MESSAGE_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished, 
                attachedErrorObject: { } 
            };

        default: return state;
    }
};
