import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_CLIENTS_CONTENT } from "../../../Api/Request";
import { ClientsContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_CLIENTS_CONTENT";
export const RECEIVE = "RECEIVE_CLIENTS_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: ClientsContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentClientsAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentClients.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentClients.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_CLIENTS_CONTENT,
        });
    },
};
