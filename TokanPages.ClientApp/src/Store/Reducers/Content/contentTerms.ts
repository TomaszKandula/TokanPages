import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentTerms } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentTerms";

export const ContentTerms: 
    Reducer<IContentTerms> = (state: IContentTerms | undefined, incomingAction: Action): 
    IContentTerms => 
{
    if (state === undefined) return ApplicationDefault.contentTerms;

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
