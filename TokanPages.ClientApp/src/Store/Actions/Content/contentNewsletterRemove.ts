import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_UNSUBSCRIBE_CONTENT } from "../../../Api/Request";
import { NewsletterRemoveContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_REMOVE_NEWSLETTER_CONTENT";
export const RECEIVE = "RECEIVE_REMOVE_NEWSLETTER_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: NewsletterRemoveContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentNewsletterRemoveAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentNewsletterRemove.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentNewsletterRemove.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_UNSUBSCRIBE_CONTENT,
        });
    },
};
