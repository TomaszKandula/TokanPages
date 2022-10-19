import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentUserSignin } from "../../States";

import { 
    TKnownActions, 
    REQUEST_USER_SIGNIN_CONTENT, 
    RECEIVE_USER_SIGNIN_CONTENT 
} from "../../Actions/Content/contentUserSignin";

export const ContentUserSignin: 
    Reducer<IContentUserSignin> = (state: IContentUserSignin | undefined, incomingAction: Action): 
    IContentUserSignin => 
{
    if (state === undefined) return ApplicationDefault.contentUserSignin;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_USER_SIGNIN_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_USER_SIGNIN_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
