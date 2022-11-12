import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentAccount } from "../../States";

import { 
    TKnownActions,
    REQUEST, 
    RECEIVE
} from "../../Actions/Content/contentAccount";

export const ContentAccount: 
    Reducer<IContentAccount> = (state: IContentAccount | undefined, incomingAction: Action): 
    IContentAccount => 
{
    if (state === undefined) return ApplicationDefault.contentAccount;

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
