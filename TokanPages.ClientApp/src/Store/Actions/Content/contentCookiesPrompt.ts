import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_COOKIES_PROMPT_CONTENT } from "../../../Api/Request";
import { CookiesPromptContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_COOKIES_PROMPT_CONTENT";
export const RECEIVE = "RECEIVE_COOKIES_PROMPT_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: CookiesPromptContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentCookiesPromptAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentCookiesPrompt.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentCookiesPrompt.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_COOKIES_PROMPT_CONTENT,
        });
    },
};
