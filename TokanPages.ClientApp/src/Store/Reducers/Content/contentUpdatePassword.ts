import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentUpdatePassword } from "../../States";
import { 
    TKnownActions,
    RECEIVE_UPDATE_PASSWORD_CONTENT, 
    REQUEST_UPDATE_PASSWORD_CONTENT
} from "../../Actions/Content/contentUpdatePassword";

export const ContentUpdatePassword: 
    Reducer<IContentUpdatePassword> = (state: IContentUpdatePassword | undefined, incomingAction: Action): 
    IContentUpdatePassword => 
{
    if (state === undefined) return ApplicationDefault.contentUpdatePassword;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_UPDATE_PASSWORD_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_UPDATE_PASSWORD_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}