import { IApplicationAction, ApplicationDefaults } from "../../Configuration";
import { GET_FEATURED_CONTENT } from "../../../Shared/constants";
import { IFeaturedContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_FEATURED_CONTENT = "REQUEST_FEATURED_CONTENT";
export const RECEIVE_FEATURED_CONTENT = "RECEIVE_FEATURED_CONTENT";
export interface IRequestFeaturedContent { type: typeof REQUEST_FEATURED_CONTENT }
export interface IReceiveFeaturedContent { type: typeof RECEIVE_FEATURED_CONTENT, payload: IFeaturedContentDto }
export type TKnownActions = IRequestFeaturedContent | IReceiveFeaturedContent;

export const ContentFeaturedAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentFeatured.content.language;

        if (getState().contentFeatured.content !== ApplicationDefaults.contentFeatured.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_FEATURED_CONTENT, 
            receive: RECEIVE_FEATURED_CONTENT, 
            url: GET_FEATURED_CONTENT 
        });
    }
}