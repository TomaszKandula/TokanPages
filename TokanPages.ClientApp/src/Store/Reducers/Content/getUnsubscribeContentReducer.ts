import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetUnsubscribeContent } from "../../States/Content/getUnsubscribeContentState";
import { 
    TKnownActions,
    RECEIVE_UNSUBSCRIBE_CONTENT, 
    REQUEST_UNSUBSCRIBE_CONTENT
} from "../../Actions/Content/getUnsubscribeContentAction";

export const GetUnsubscribeContentReducer: Reducer<IGetUnsubscribeContent> = (state: IGetUnsubscribeContent | undefined, incomingAction: Action): IGetUnsubscribeContent => 
{
    if (state === undefined) return combinedDefaults.getUnsubscribeContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_UNSUBSCRIBE_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_UNSUBSCRIBE_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
