import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_SIGNOUT_CONTENT } from "../../../Api/Request";
import { IUserSignoutContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST = "REQUEST_USER_SIGNOUT_CONTENT";
export const RECEIVE = "RECEIVE_USER_SIGNOUT_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IUserSignoutContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentUserSignoutAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentUserSignout.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentUserSignout.content;
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
            url: GET_SIGNOUT_CONTENT 
        });
    }
}