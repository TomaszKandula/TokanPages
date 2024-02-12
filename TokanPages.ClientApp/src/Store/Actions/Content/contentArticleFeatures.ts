import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_ARTICLE_FEAT_CONTENT } from "../../../Api/Request";
import { ArticleFeaturesContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_ARTICE_FEATURES";
export const RECEIVE = "RECEIVE_ARTICE_FEATURES";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: ArticleFeaturesContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentArticleFeaturesAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentArticleFeatures.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentArticleFeatures.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_ARTICLE_FEAT_CONTENT,
        });
    },
};
