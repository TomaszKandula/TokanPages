import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetPolicyContent } from "../../../Redux/States/Content/getPolicyContentState";
import { 
    TKnownActions,
    RECEIVE_POLICY_CONTENT, 
    REQUEST_POLICY_CONTENT
} from "../../../Redux/Actions/Content/getPolicyContentAction";

export const GetPolicyContentReducer: Reducer<IGetPolicyContent> = (state: IGetPolicyContent | undefined, incomingAction: Action): IGetPolicyContent => 
{
    if (state === undefined) return combinedDefaults.getPolicyContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_POLICY_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_POLICY_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
