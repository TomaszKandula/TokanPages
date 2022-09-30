import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_ARTICLE_FEAT_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IArticleFeaturesContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_ARTICE_FEATURES = "REQUEST_ARTICE_FEATURES";
export const RECEIVE_ARTICE_FEATURES = "RECEIVE_ARTICE_FEATURES";
export interface IRequestArticleFeatures { type: typeof REQUEST_ARTICE_FEATURES }
export interface IReceiveArticleFeatures { type: typeof RECEIVE_ARTICE_FEATURES, payload: IArticleFeaturesContentDto }
export type TKnownActions = IRequestArticleFeatures | IReceiveArticleFeatures | TErrorActions;

export const ActionCreators = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getArticleFeaturesContent.content.language;

        if (getState().getArticleFeaturesContent.content !== ApplicationDefaults.getArticleFeaturesContent.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_ARTICE_FEATURES, 
            receive: RECEIVE_ARTICE_FEATURES, 
            url: GET_ARTICLE_FEAT_CONTENT 
        });
    }
}