import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_FEATURED_CONTENT } from "../../../Api/Request";
import { FeaturedContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_FEATURED_CONTENT";
export const RECEIVE = "RECEIVE_FEATURED_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: FeaturedContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentFeaturedAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentFeatured.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentFeatured.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_FEATURED_CONTENT,
        });
    },
};
