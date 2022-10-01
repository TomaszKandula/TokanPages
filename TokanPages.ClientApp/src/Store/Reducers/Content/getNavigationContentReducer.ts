import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IGetNavigationContent } from "../../States";
import { 
    TKnownActions,
    RECEIVE_NAVIGATION_CONTENT, 
    REQUEST_NAVIGATION_CONTENT
} from "../../Actions/Content/getNavigationContentAction";

export const GetNavigationContentReducer: 
    Reducer<IGetNavigationContent> = (state: IGetNavigationContent | undefined, incomingAction: Action): 
    IGetNavigationContent => 
{
    if (state === undefined) return ApplicationDefaults.contentNavigation;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_NAVIGATION_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_NAVIGATION_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
