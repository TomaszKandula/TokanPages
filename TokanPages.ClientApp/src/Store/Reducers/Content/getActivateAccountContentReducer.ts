import { Action, Reducer } from "redux";
import { CombinedDefaults } from "../../Configuration";
import { IGetActivateAccountContent } from "../../States";
import { 
    TKnownActions,
    REQUEST_ACTIVATE_ACCOUNT_CONTENT, 
    RECEIVE_ACTIVATE_ACCOUNT_CONTENT
} from "../../Actions/Content/getActivateAccountContentAction";

export const GetActivateAccountContentReducer: 
    Reducer<IGetActivateAccountContent> = (state: IGetActivateAccountContent | undefined, incomingAction: Action): 
    IGetActivateAccountContent => 
{
    if (state === undefined) return CombinedDefaults.getActivateAccountContent;

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
