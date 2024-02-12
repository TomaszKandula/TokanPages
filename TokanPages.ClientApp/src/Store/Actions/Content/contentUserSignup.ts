import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_SIGNUP_CONTENT } from "../../../Api/Request";
import { UserSignupContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_USER_SIGNUP_CONTENT";
export const RECEIVE = "RECEIVE_USER_SIGNUP_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: UserSignupContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentUserSignupAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentUserSignup.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentUserSignup.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_SIGNUP_CONTENT,
        });
    },
};
