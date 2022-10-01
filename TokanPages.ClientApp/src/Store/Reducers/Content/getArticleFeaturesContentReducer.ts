import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentArticleFeatures } from "../../States";
import { 
    TKnownActions,
    RECEIVE_ARTICE_FEATURES, 
    REQUEST_ARTICE_FEATURES
} from "../../Actions/Content/getArticleFeaturesContentAction";

export const GetArticleFeaturesContentReducer: 
    Reducer<IContentArticleFeatures> = (state: IContentArticleFeatures | undefined, incomingAction: Action): 
    IContentArticleFeatures => 
{
    if (state === undefined) return ApplicationDefaults.contentArticleFeatures;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_ARTICE_FEATURES:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_ARTICE_FEATURES:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
