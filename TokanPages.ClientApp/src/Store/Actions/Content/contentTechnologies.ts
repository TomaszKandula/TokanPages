import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_TECHNOLOGIES_CONTENT } from "../../../Api/Request";
import { TechnologiesContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_TECHNOLOGIES_CONTENT";
export const RECEIVE = "RECEIVE_TECHNOLOGIES_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: TechnologiesContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentTechnologiesAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentTechnologies.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentTechnologies.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_TECHNOLOGIES_CONTENT,
        });
    },
};
