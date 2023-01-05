import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentWrongPagePrompt } from "../../States";

import { 
    TKnownActions, 
    REQUEST, 
    RECEIVE 
} from "../../Actions/Content/contentWrongPagePrompt";

export const ContentWrongPagePrompt: 
    Reducer<IContentWrongPagePrompt> = (state: IContentWrongPagePrompt | undefined, incomingAction: Action): 
    IContentWrongPagePrompt => 
{
    if (state === undefined) return ApplicationDefault.contentWrongPagePrompt;

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
