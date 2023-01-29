import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_FEATURES_CONTENT } from "../../../Api/Request";
import { FeaturesContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_FEATURES_CONTENT";
export const RECEIVE = "RECEIVE_FEATURES_CONTENT";
interface Request { type: typeof REQUEST }
interface Receive { type: typeof RECEIVE, payload: FeaturesContentDto }
export type TKnownActions = Request | Receive;

export const ContentFeaturesAction = 
{
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentFeatures.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentFeatures.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_FEATURES_CONTENT 
        });
    }
}