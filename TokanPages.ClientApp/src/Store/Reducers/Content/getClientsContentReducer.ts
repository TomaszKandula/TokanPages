import { Action, Reducer } from "redux";
import { CombinedDefaults } from "../../Configuration";
import { IGetClientsContent } from "../../States/Content/getClientsContentState";
import { 
    TKnownActions,
    REQUEST_CLIENTS_CONTENT, 
    RECEIVE_CLIENTS_CONTENT
} from "../../Actions/Content/getClientsContentAction";

export const GetClientsContentReducer: Reducer<IGetClientsContent> = (state: IGetClientsContent | undefined, incomingAction: Action): IGetClientsContent => 
{
    if (state === undefined) return CombinedDefaults.getClientsContent;

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
