import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_SIGNOUT_CONTENT } from "../../../Api/Request";
import { UserSignoutContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_USER_SIGNOUT_CONTENT";
export const RECEIVE = "RECEIVE_USER_SIGNOUT_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: UserSignoutContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentUserSignoutAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentUserSignout.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentUserSignout.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_SIGNOUT_CONTENT,
        });
    },
};
