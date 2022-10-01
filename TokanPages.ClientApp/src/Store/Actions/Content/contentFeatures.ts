import { IAppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_FEATURES_CONTENT } from "../../../Shared/constants";
import { IFeaturesContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_FEATURES_CONTENT = "REQUEST_FEATURES_CONTENT";
export const RECEIVE_FEATURES_CONTENT = "RECEIVE_FEATURES_CONTENT";
export interface IRequestFeaturesContent { type: typeof REQUEST_FEATURES_CONTENT }
export interface IReceiveFeaturesContent { type: typeof RECEIVE_FEATURES_CONTENT, payload: IFeaturesContentDto }
export type TKnownActions = IRequestFeaturesContent | IReceiveFeaturesContent;

export const ContentFeaturesAction = 
{
    get: (): IAppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentFeatures.content.language;

        if (getState().contentFeatures.content !== ApplicationDefaults.contentFeatures.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_FEATURES_CONTENT, 
            receive: RECEIVE_FEATURES_CONTENT, 
            url: GET_FEATURES_CONTENT 
        });
    }
}