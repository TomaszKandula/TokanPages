import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentNavigation } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentNavigation";

export const ContentNavigation: 
    Reducer<IContentNavigation> = (state: IContentNavigation | undefined, incomingAction: Action): 
    IContentNavigation => 
{
    if (state === undefined) return ApplicationDefault.contentNavigation;

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
