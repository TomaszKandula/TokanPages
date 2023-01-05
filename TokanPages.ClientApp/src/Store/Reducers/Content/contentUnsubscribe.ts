import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentUnsubscribe } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentUnsubscribe";

export const ContentUnsubscribe: 
    Reducer<IContentUnsubscribe> = (state: IContentUnsubscribe | undefined, incomingAction: Action): 
    IContentUnsubscribe => 
{
    if (state === undefined) return ApplicationDefault.contentUnsubscribe;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
