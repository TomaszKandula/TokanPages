import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_HEADER_CONTENT } from "../../../Api/Request";
import { HeaderContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_HEADER_CONTENT";
export const RECEIVE = "RECEIVE_HEADER_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: HeaderContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentHeaderAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentHeader.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentHeader.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_HEADER_CONTENT,
        });
    },
};
