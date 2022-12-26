import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentNewsletter } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentNewsletter";

export const ContentNewsletter: 
    Reducer<IContentNewsletter> = (state: IContentNewsletter | undefined, incomingAction: Action): 
    IContentNewsletter => 
{
    if (state === undefined) return ApplicationDefault.contentNewsletter;

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
