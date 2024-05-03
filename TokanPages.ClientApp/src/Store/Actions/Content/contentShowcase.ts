import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_SHOWCASE_CONTENT } from "../../../Api/Request";
import { DocumentContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_SHOWCASE_CONTENT";
export const RECEIVE = "RECEIVE_SHOWCASE_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: DocumentContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentShowcaseAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentShowcase.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentShowcase.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_SHOWCASE_CONTENT,
        });
    },
};
