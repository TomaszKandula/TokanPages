import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IArticleListing } from "../../States";
import { 
    TKnownActions, 
    RECEIVE_ARTICLES, 
    REQUEST_ARTICLES, 
} from "../../Actions/Articles/articleListing";

export const ArticleListing: Reducer<IArticleListing> = (state: IArticleListing | undefined, incomingAction: Action): IArticleListing => 
{
    if (state === undefined) return ApplicationDefaults.articleListing;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_ARTICLES:
            return { 
                isLoading: true, 
                articles: state.articles
            };

        case RECEIVE_ARTICLES:
            return { 
                isLoading: false, 
                articles: action.payload
            };

        default: return state;
    }
}
