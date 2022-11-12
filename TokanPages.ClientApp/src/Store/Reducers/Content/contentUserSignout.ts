import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentUserSignout } from "../../States";

import { 
    TKnownActions, 
    REQUEST, 
    RECEIVE 
} from "../../Actions/Content/contentUserSignout";

export const ContentUserSignout: 
    Reducer<IContentUserSignout> = (state: IContentUserSignout | undefined, incomingAction: Action): 
    IContentUserSignout => 
{
    if (state === undefined) return ApplicationDefault.contentUserSignout;

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
