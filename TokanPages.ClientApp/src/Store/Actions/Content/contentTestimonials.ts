import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_TESTIMONIALS_CONTENT } from "../../../Shared/constants";
import { ITestimonialsContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_TESTIMONIALS_CONTENT = "REQUEST_TESTIMONIALS_CONTENT";
export const RECEIVE_TESTIMONIALS_CONTENT = "RECEIVE_TESTIMONIALS_CONTENT";
export interface IRequestTestimonialsContent { type: typeof REQUEST_TESTIMONIALS_CONTENT }
export interface IReceiveTestimonialsContent { type: typeof RECEIVE_TESTIMONIALS_CONTENT, payload: ITestimonialsContentDto }
export type TKnownActions = IRequestTestimonialsContent | IReceiveTestimonialsContent;

export const ContentTestimonialsAction = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentTestimonials.content.language;

        if (getState().contentTestimonials.content !== ApplicationDefaults.contentTestimonials.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_TESTIMONIALS_CONTENT, 
            receive: RECEIVE_TESTIMONIALS_CONTENT, 
            url: GET_TESTIMONIALS_CONTENT 
        });
    }
}