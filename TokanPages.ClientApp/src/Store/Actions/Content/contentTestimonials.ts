import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_TESTIMONIALS_CONTENT } from "../../../Api/Request";
import { ITestimonialsContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST = "REQUEST_TESTIMONIALS_CONTENT";
export const RECEIVE = "RECEIVE_TESTIMONIALS_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: ITestimonialsContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentTestimonialsAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentTestimonials.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentTestimonials.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_TESTIMONIALS_CONTENT 
        });
    }
}