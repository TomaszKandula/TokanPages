import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentAccount } from "../../States";
import { 
    TKnownActions,
    REQUEST_ACCOUNT_CONTENT, 
    RECEIVE_ACCOUNT_CONTENT
} from "../../Actions/Content/getAccountContentAction";

export const ContentAccount: 
    Reducer<IContentAccount> = (state: IContentAccount | undefined, incomingAction: Action): 
    IContentAccount => 
{
    if (state === undefined) return ApplicationDefaults.contentAccount;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_ACCOUNT_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_ACCOUNT_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
