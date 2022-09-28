import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetFeaturesContent } from "../../States/Content/getFeaturesContentState";
import { 
    TKnownActions,
    RECEIVE_FEATURES_CONTENT, 
    REQUEST_FEATURES_CONTENT
} from "../../Actions/Content/getFeaturesContentAction";

export const GetFeaturesContentReducer: Reducer<IGetFeaturesContent> = (state: IGetFeaturesContent | undefined, incomingAction: Action): IGetFeaturesContent => 
{
    if (state === undefined) return combinedDefaults.getFeaturesContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_FEATURES_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_FEATURES_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
