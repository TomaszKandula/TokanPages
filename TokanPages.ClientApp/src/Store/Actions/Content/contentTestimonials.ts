import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_TESTIMONIALS_CONTENT } from "../../../Shared/constants";
import { ITestimonialsContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_TESTIMONIALS_CONTENT = "REQUEST_TESTIMONIALS_CONTENT";
export const RECEIVE_TESTIMONIALS_CONTENT = "RECEIVE_TESTIMONIALS_CONTENT";
export interface IRequestTestimonialsContent { type: typeof REQUEST_TESTIMONIALS_CONTENT }
export interface IReceiveTestimonialsContent { type: typeof RECEIVE_TESTIMONIALS_CONTENT, payload: ITestimonialsContentDto }
export type TKnownActions = IRequestTestimonialsContent | IReceiveTestimonialsContent;

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
            request: REQUEST_TESTIMONIALS_CONTENT, 
            receive: RECEIVE_TESTIMONIALS_CONTENT, 
            url: GET_TESTIMONIALS_CONTENT 
        });
    }
}