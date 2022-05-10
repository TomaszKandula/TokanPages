import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetNavigationContent } from "../../../Redux/States/Content/getNavigationContentState";
import { 
    TKnownActions,
    RECEIVE_NAVIGATION_CONTENT, 
    REQUEST_NAVIGATION_CONTENT
} from "../../../Redux/Actions/Content/getNavigationContentAction";

export const GetNavigationContentReducer: Reducer<IGetNavigationContent> = (state: IGetNavigationContent | undefined, incomingAction: Action): IGetNavigationContent => 
{
    if (state === undefined) return combinedDefaults.getNavigationContent;

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
