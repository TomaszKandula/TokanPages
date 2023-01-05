import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_FEATURES_CONTENT } from "../../../Api/Request";
import { IFeaturesContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST = "REQUEST_FEATURES_CONTENT";
export const RECEIVE = "RECEIVE_FEATURES_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IFeaturesContentDto }
export type TKnownActions = IRequest | IReceive;

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
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_FEATURES_CONTENT 
        });
    }
}