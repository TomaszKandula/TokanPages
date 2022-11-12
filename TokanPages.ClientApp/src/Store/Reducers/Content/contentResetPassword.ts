import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentResetPassword } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentResetPassword";

export const ContentResetPassword: 
    Reducer<IContentResetPassword> = (state: IContentResetPassword | undefined, incomingAction: Action): 
    IContentResetPassword => 
{
    if (state === undefined) return ApplicationDefault.contentResetPassword;

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
