import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { AccountContentDto } from "../../../Api/Models";
import { GetContent, GET_ACCOUNT_CONTENT } from "../../../Api/Request";

export const REQUEST = "REQUEST_ACCOUNT_CONTENT";
export const RECEIVE = "RECEIVE_ACCOUNT_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: AccountContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentAccountAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentAccount.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentAccount.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_ACCOUNT_CONTENT,
        });
    },
};
