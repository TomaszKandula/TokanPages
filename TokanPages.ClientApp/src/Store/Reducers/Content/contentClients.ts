import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentClients } from "../../States";

import { 
    TKnownActions,
    REQUEST_CLIENTS_CONTENT, 
    RECEIVE_CLIENTS_CONTENT
} from "../../Actions/Content/contentClients";

export const ContentClients: 
    Reducer<IContentClients> = (state: IContentClients | undefined, incomingAction: Action): 
    IContentClients => 
{
    if (state === undefined) return ApplicationDefault.contentClients;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_CLIENTS_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_CLIENTS_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
