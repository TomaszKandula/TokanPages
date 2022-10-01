import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentActivateAccount } from "../../States";
import { 
    TKnownActions,
    REQUEST_ACTIVATE_ACCOUNT_CONTENT, 
    RECEIVE_ACTIVATE_ACCOUNT_CONTENT
} from "../../Actions/Content/getActivateAccountContentAction";

export const ContentActivateAccount: 
    Reducer<IContentActivateAccount> = (state: IContentActivateAccount | undefined, incomingAction: Action): 
    IContentActivateAccount => 
{
    if (state === undefined) return ApplicationDefaults.contentActivateAccount;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_ACTIVATE_ACCOUNT_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_ACTIVATE_ACCOUNT_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
