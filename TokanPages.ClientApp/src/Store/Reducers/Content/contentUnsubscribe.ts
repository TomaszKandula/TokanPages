import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentUnsubscribe } from "../../States";
import { 
    TKnownActions,
    RECEIVE_UNSUBSCRIBE_CONTENT, 
    REQUEST_UNSUBSCRIBE_CONTENT
} from "../../Actions/Content/contentUnsubscribe";

export const ContentUnsubscribe: 
    Reducer<IContentUnsubscribe> = (state: IContentUnsubscribe | undefined, incomingAction: Action): 
    IContentUnsubscribe => 
{
    if (state === undefined) return ApplicationDefault.contentUnsubscribe;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_UNSUBSCRIBE_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_UNSUBSCRIBE_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
