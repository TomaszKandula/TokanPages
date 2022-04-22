import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetAccountContent } from "../../../Redux/States/Content/getAccountContentState";
import { 
    TKnownActions,
    REQUEST_ACCOUNT_CONTENT, 
    RECEIVE_ACCOUNT_CONTENT
} from "../../../Redux/Actions/Content/getAccountContentAction";

const GetAccountContentReducer: Reducer<IGetAccountContent> = (state: IGetAccountContent | undefined, incomingAction: Action): IGetAccountContent => 
{
    if (state === undefined) return combinedDefaults.getAccountContent;

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

export default GetAccountContentReducer;
