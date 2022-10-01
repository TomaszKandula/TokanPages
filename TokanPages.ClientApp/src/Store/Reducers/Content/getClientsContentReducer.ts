import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentClients } from "../../States";
import { 
    TKnownActions,
    REQUEST_CLIENTS_CONTENT, 
    RECEIVE_CLIENTS_CONTENT
} from "../../Actions/Content/getClientsContentAction";

export const GetClientsContentReducer: 
    Reducer<IContentClients> = (state: IContentClients | undefined, incomingAction: Action): 
    IContentClients => 
{
    if (state === undefined) return ApplicationDefaults.contentClients;

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
