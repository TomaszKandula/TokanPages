import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IGetUnsubscribeContent } from "../../States";
import { 
    TKnownActions,
    RECEIVE_UNSUBSCRIBE_CONTENT, 
    REQUEST_UNSUBSCRIBE_CONTENT
} from "../../Actions/Content/getUnsubscribeContentAction";

export const GetUnsubscribeContentReducer: 
    Reducer<IGetUnsubscribeContent> = (state: IGetUnsubscribeContent | undefined, incomingAction: Action): 
    IGetUnsubscribeContent => 
{
    if (state === undefined) return ApplicationDefaults.contentUnsubscribe;

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