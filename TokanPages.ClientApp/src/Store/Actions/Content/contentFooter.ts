import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_FOOTER_CONTENT } from "../../../Api/Request";
import { FooterContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_FOOTER_CONTENT";
export const RECEIVE = "RECEIVE_FOOTER_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: FooterContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentFooterAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentFooter.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentFooter.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_FOOTER_CONTENT,
        });
    },
};
