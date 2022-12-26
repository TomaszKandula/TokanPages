import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentUserSignin } from "../../States";

import { 
    TKnownActions, 
    REQUEST, 
    RECEIVE 
} from "../../Actions/Content/contentUserSignin";

export const ContentUserSignin: 
    Reducer<IContentUserSignin> = (state: IContentUserSignin | undefined, incomingAction: Action): 
    IContentUserSignin => 
{
    if (state === undefined) return ApplicationDefault.contentUserSignin;

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
