import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_FOOTER_CONTENT } from "../../../Api/Request";
import { IFooterContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_FOOTER_CONTENT";
export const RECEIVE = "RECEIVE_FOOTER_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IFooterContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentFooterAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentFooter.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentFooter.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_FOOTER_CONTENT 
        });
    }
}