import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_UPDATE_NEWSLETTER_CONTENT } from "../../../Api/Request";
import { UpdateNewsletterContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_UPDATE_NEWSLETTER_CONTENT";
export const RECEIVE = "RECEIVE_UPDATE_NEWSLETTER_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: UpdateNewsletterContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentUpdateNewsletterAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentUpdateNewsletter.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentUpdateNewsletter.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_UPDATE_NEWSLETTER_CONTENT,
        });
    },
};
