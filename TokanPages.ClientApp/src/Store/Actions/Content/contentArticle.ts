import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_ARTICLE_CONTENT, GetContent } from "../../../Api/Request";
import { ArticleContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_ARTICE_CONTENT_FEATURES";
export const RECEIVE = "RECEIVE_ARTICE_CONTENT_FEATURES";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: ArticleContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentArticleAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentArticle.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentArticle.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_ARTICLE_CONTENT,
        });
    },
};
