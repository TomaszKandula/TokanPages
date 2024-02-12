import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_UPDATE_PASSWORD_CONTENT } from "../../../Api/Request";
import { UpdatePasswordContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_UPDATE_PASSWORD_CONTENT";
export const RECEIVE = "RECEIVE_UPDATE_PASSWORD_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: UpdatePasswordContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentUpdatePasswordAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentUpdatePassword.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentUpdatePassword.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_UPDATE_PASSWORD_CONTENT,
        });
    },
};
