import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentUpdatePassword } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentUpdatePassword";

export const ContentUpdatePassword: 
    Reducer<IContentUpdatePassword> = (state: IContentUpdatePassword | undefined, incomingAction: Action): 
    IContentUpdatePassword => 
{
    if (state === undefined) return ApplicationDefault.contentUpdatePassword;

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
