import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { GET_TESTIMONIALS_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { ITestimonialsContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_TESTIMONIALS_CONTENT = "REQUEST_TESTIMONIALS_CONTENT";
export const RECEIVE_TESTIMONIALS_CONTENT = "RECEIVE_TESTIMONIALS_CONTENT";
export interface IRequestTestimonialsContent { type: typeof REQUEST_TESTIMONIALS_CONTENT }
export interface IReceiveTestimonialsContent { type: typeof RECEIVE_TESTIMONIALS_CONTENT, payload: ITestimonialsContentDto }
export type TKnownActions = IRequestTestimonialsContent | IReceiveTestimonialsContent | TErrorActions;

export const ActionCreators = 
{
    getTestimonialsContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getTestimonialsContent.content.language;

        if (getState().getTestimonialsContent.content !== combinedDefaults.getTestimonialsContent.content && !isLanguageChanged) 
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