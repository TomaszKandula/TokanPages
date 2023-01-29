import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentResetPasswordState } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentResetPassword";

export const ContentResetPassword: 
    Reducer<ContentResetPasswordState> = (state: ContentResetPasswordState | undefined, incomingAction: Action): 
    ContentResetPasswordState => 
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
