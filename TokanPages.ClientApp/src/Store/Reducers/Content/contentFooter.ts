import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentFooter } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentFooter";

export const ContentFooter: 
    Reducer<IContentFooter> = (state: IContentFooter | undefined, incomingAction: Action): 
    IContentFooter => 
{
    if (state === undefined) return ApplicationDefault.contentFooter;

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
