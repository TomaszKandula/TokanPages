import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_BUSINESS_FORM_CONTENT } from "../../../Api/Request";
import { BusinessFormContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_BUSINESS_FORM_CONTENT";
export const RECEIVE = "RECEIVE_BUSINESS_FORM_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: BusinessFormContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentBusinessFormAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentBusinessForm.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentBusinessForm.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_BUSINESS_FORM_CONTENT,
        });
    },
};
