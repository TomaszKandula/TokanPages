import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IApplicationEmail } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions, 
    SEND, 
    RESPONSE, 
    CLEAR
} from "../../Actions/Application/applicationMessage";

export const ApplicationEmail: 
    Reducer<IApplicationEmail> = (state: IApplicationEmail | undefined, incomingAction: Action): 
    IApplicationEmail => 
{
    if (state === undefined) return ApplicationDefault.applicationEmail;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case CLEAR:
            return ApplicationDefault.applicationEmail;
            
        case SEND:
            return { 
                status: OperationStatus.inProgress, 
                response: state.response 
            };

        case RESPONSE:
            return { 
                status: OperationStatus.hasFinished, 
                response: action.payload
            };

        default: return state;
    }
};
