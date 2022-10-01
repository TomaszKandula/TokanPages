import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentFeatures } from "../../States/Content/getFeaturesContentState";
import { 
    TKnownActions,
    RECEIVE_FEATURES_CONTENT, 
    REQUEST_FEATURES_CONTENT
} from "../../Actions/Content/getFeaturesContentAction";

export const GetFeaturesContentReducer: 
    Reducer<IContentFeatures> = (state: IContentFeatures | undefined, incomingAction: Action): 
    IContentFeatures => 
{
    if (state === undefined) return ApplicationDefaults.contentFeatures;

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
