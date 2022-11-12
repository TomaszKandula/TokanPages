import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_HEADER_CONTENT } from "../../../Api/Request";
import { IHeaderContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST = "REQUEST_HEADER_CONTENT";
export const RECEIVE = "RECEIVE_HEADER_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IHeaderContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentHeaderAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentHeader.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentHeader.content;
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
            url: GET_HEADER_CONTENT 
        });
    }
}