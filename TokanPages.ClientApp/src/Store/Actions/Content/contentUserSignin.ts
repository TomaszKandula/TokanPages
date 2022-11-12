import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_SIGNIN_CONTENT } from "../../../Api/Request";
import { IUserSigninContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST = "REQUEST_USER_SIGNIN_CONTENT";
export const RECEIVE = "RECEIVE_USER_SIGNIN_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IUserSigninContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentUserSigninAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentUserSignin.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentUserSignin.content;
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
            url: GET_SIGNIN_CONTENT 
        });
    }
}