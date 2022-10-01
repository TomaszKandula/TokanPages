import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_FEATURED_CONTENT } from "../../../Shared/constants";
import { IFeaturedContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_FEATURED_CONTENT = "REQUEST_FEATURED_CONTENT";
export const RECEIVE_FEATURED_CONTENT = "RECEIVE_FEATURED_CONTENT";
export interface IRequestFeaturedContent { type: typeof REQUEST_FEATURED_CONTENT }
export interface IReceiveFeaturedContent { type: typeof RECEIVE_FEATURED_CONTENT, payload: IFeaturedContentDto }
export type TKnownActions = IRequestFeaturedContent | IReceiveFeaturedContent;

export const ContentFeaturedAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentFeatured.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentFeatured.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_FEATURED_CONTENT, 
            receive: RECEIVE_FEATURED_CONTENT, 
            url: GET_FEATURED_CONTENT 
        });
    }
}