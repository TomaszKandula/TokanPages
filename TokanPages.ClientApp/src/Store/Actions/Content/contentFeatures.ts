import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_FEATURES_CONTENT } from "../../../Shared/constants";
import { IFeaturesContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_FEATURES_CONTENT = "REQUEST_FEATURES_CONTENT";
export const RECEIVE_FEATURES_CONTENT = "RECEIVE_FEATURES_CONTENT";
export interface IRequestFeaturesContent { type: typeof REQUEST_FEATURES_CONTENT }
export interface IReceiveFeaturesContent { type: typeof RECEIVE_FEATURES_CONTENT, payload: IFeaturesContentDto }
export type TKnownActions = IRequestFeaturesContent | IReceiveFeaturesContent;

export const ContentFeaturesAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentFeatures.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentFeatures.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_FEATURES_CONTENT, 
            receive: RECEIVE_FEATURES_CONTENT, 
            url: GET_FEATURES_CONTENT 
        });
    }
}