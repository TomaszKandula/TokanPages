import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_POLICY_CONTENT } from "../../../Api/Request";
import { DocumentContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_POLICY_CONTENT";
export const RECEIVE = "RECEIVE_POLICY_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: DocumentContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentPolicyAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentPolicy.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentPolicy.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_POLICY_CONTENT,
        });
    },
};
