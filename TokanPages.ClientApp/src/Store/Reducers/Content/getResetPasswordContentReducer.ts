import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentResetPassword } from "../../States";
import { 
    TKnownActions,
    RECEIVE_RESET_PASSWORD_CONTENT, 
    REQUEST_RESET_PASSWORD_CONTENT
} from "../../Actions/Content/getResetPasswordContentAction";

export const GetResetPasswordContentReducer: 
    Reducer<IContentResetPassword> = (state: IContentResetPassword | undefined, incomingAction: Action): 
    IContentResetPassword => 
{
    if (state === undefined) return ApplicationDefaults.contentResetPassword;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_RESET_PASSWORD_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_RESET_PASSWORD_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
