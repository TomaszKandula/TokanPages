import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_SIGNIN_CONTENT } from "../../../Api/Request";
import { UserSigninContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_USER_SIGNIN_CONTENT";
export const RECEIVE = "RECEIVE_USER_SIGNIN_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: UserSigninContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentUserSigninAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentUserSignin.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentUserSignin.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_SIGNIN_CONTENT,
        });
    },
};
