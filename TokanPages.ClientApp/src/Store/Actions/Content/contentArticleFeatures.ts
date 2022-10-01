import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_ARTICLE_FEAT_CONTENT } from "../../../Shared/constants";
import { IArticleFeaturesContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_ARTICE_FEATURES = "REQUEST_ARTICE_FEATURES";
export const RECEIVE_ARTICE_FEATURES = "RECEIVE_ARTICE_FEATURES";
export interface IRequestArticleFeatures { type: typeof REQUEST_ARTICE_FEATURES }
export interface IReceiveArticleFeatures { type: typeof RECEIVE_ARTICE_FEATURES, payload: IArticleFeaturesContentDto }
export type TKnownActions = IRequestArticleFeatures | IReceiveArticleFeatures;

export const ContentArticleFeaturesAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentArticleFeatures.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentArticleFeatures.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_ARTICE_FEATURES, 
            receive: RECEIVE_ARTICE_FEATURES, 
            url: GET_ARTICLE_FEAT_CONTENT 
        });
    }
}