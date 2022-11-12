import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_SIGNUP_CONTENT } from "../../../Api/Request";
import { IUserSignupContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST = "REQUEST_USER_SIGNUP_CONTENT";
export const RECEIVE = "RECEIVE_USER_SIGNUP_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IUserSignupContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentUserSignupAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentUserSignup.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentUserSignup.content;
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
            url: GET_SIGNUP_CONTENT 
        });
    }
}