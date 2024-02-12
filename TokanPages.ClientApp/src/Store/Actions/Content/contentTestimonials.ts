import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_TESTIMONIALS_CONTENT } from "../../../Api/Request";
import { TestimonialsContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_TESTIMONIALS_CONTENT";
export const RECEIVE = "RECEIVE_TESTIMONIALS_CONTENT";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: TestimonialsContentDto;
}
export type TKnownActions = Request | Receive;

export const ContentTestimonialsAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentTestimonials.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentTestimonials.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST,
            receive: RECEIVE,
            url: GET_TESTIMONIALS_CONTENT,
        });
    },
};
