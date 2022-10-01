import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_SIGNIN_CONTENT } from "../../../Shared/constants";
import { IUserSigninContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_USER_SIGNIN_CONTENT = "REQUEST_USER_SIGNIN_CONTENT";
export const RECEIVE_USER_SIGNIN_CONTENT = "RECEIVE_USER_SIGNIN_CONTENT";
export interface IRequestSigninFormContent { type: typeof REQUEST_USER_SIGNIN_CONTENT }
export interface IReceiveSigninFormContent { type: typeof RECEIVE_USER_SIGNIN_CONTENT, payload: IUserSigninContentDto }
export type TKnownActions = IRequestSigninFormContent | IReceiveSigninFormContent;

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
            request: REQUEST_USER_SIGNIN_CONTENT, 
            receive: RECEIVE_USER_SIGNIN_CONTENT, 
            url: GET_SIGNIN_CONTENT 
        });
    }
}