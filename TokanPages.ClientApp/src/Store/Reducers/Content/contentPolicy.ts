import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentPolicy } from "../../States";
import { 
    TKnownActions,
    RECEIVE_POLICY_CONTENT, 
    REQUEST_POLICY_CONTENT
} from "../../Actions/Content/getPolicyContentAction";

export const ContentPolicy: 
    Reducer<IContentPolicy> = (state: IContentPolicy | undefined, incomingAction: Action): 
    IContentPolicy => 
{
    if (state === undefined) return ApplicationDefaults.contentPolicy;

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
