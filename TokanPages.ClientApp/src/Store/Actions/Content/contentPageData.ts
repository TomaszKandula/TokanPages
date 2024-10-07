import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, REQUEST_PAGE_CONTENT } from "../../../Api/Request";
import { GetPageContentResultDto } from "../../../Api/Models";

export const CLEAR = "CLEAR_PAGE_CONTENT";
export const REQUEST = "REQUEST_PAGE_CONTENT";
export const RECEIVE = "RECEIVE_PAGE_CONTENT";
interface Clear {
    type: typeof CLEAR;
}
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: GetPageContentResultDto;
}
export type TKnownActions = Clear | Request | Receive;

export const ContentPageDataAction = {
    request: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentPageData.response;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentPageData.response;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: REQUEST_PAGE_CONTENT,
        });
    },
};
