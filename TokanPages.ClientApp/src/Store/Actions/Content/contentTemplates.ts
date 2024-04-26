import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_CONTENT_TEMPLATES } from "../../../Api/Request";
import { TemplatesContent } from "../../../Api/Models";

export const REQUEST = "REQUEST_TEMPLATES_CONTENT";
export const RECEIVE = "RECEIVE_TEMPLATES_CONTENT";
interface IRequest {
    type: typeof REQUEST;
}
interface IReceive {
    type: typeof RECEIVE;
    payload: TemplatesContent;
}
export type TKnownActions = IRequest | IReceive;

export const ContentTemplatesAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentTemplates.content;
        const isContentChanged = content !== ApplicationDefault.contentTemplates.content;
        const isLanguageChanged = getState().applicationLanguage.id !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_CONTENT_TEMPLATES,
        });
    },
};
