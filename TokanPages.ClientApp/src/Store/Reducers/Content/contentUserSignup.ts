import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentUserSignup } from "../../States";

import { 
    TKnownActions, 
    REQUEST, 
    RECEIVE 
} from "../../Actions/Content/contentUserSignup";

export const ContentUserSignup: 
    Reducer<IContentUserSignup> = (state: IContentUserSignup | undefined, incomingAction: Action): 
    IContentUserSignup => 
{
    if (state === undefined) return ApplicationDefault.contentUserSignup;

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
