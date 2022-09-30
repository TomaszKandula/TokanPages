import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IGetFeaturedContent } from "../../States";
import { 
    TKnownActions,
    RECEIVE_FEATURED_CONTENT, 
    REQUEST_FEATURED_CONTENT
} from "../../Actions/Content/getFeaturedContentAction";

export const GetFeaturedContentReducer: 
    Reducer<IGetFeaturedContent> = (state: IGetFeaturedContent | undefined, incomingAction: Action): 
    IGetFeaturedContent => 
{
    if (state === undefined) return ApplicationDefaults.getFeaturedContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_FEATURED_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_FEATURED_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
