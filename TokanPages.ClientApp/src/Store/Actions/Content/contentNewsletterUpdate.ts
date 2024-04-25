import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_UPDATE_NEWSLETTER_CONTENT } from "../../../Api/Request";
import { NewsletterUpdateContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_UPDATE_NEWSLETTER_CONTENT";
export const RECEIVE = "RECEIVE_UPDATE_NEWSLETTER_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: NewsletterUpdateContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentNewsletterUpdateAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentNewsletterUpdate.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentNewsletterUpdate.content;
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
