import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentArticleFeatures } from "../../States";
import { 
    TKnownActions,
    RECEIVE_ARTICE_FEATURES, 
    REQUEST_ARTICE_FEATURES
} from "../../Actions/Content/contentArticleFeatures";

export const ContentArticleFeatures: 
    Reducer<IContentArticleFeatures> = (state: IContentArticleFeatures | undefined, incomingAction: Action): 
    IContentArticleFeatures => 
{
    if (state === undefined) return ApplicationDefault.contentArticleFeatures;

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
