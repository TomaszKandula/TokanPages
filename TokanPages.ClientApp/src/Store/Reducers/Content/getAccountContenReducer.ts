import { Action, Reducer } from "redux";
import { CombinedDefaults } from "../../Configuration";
import { IGetAccountContent } from "../../States";
import { 
    TKnownActions,
    REQUEST_ACCOUNT_CONTENT, 
    RECEIVE_ACCOUNT_CONTENT
} from "../../Actions/Content/getAccountContentAction";

export const GetAccountContentReducer: 
    Reducer<IGetAccountContent> = (state: IGetAccountContent | undefined, incomingAction: Action): 
    IGetAccountContent => 
{
    if (state === undefined) return CombinedDefaults.getAccountContent;

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
