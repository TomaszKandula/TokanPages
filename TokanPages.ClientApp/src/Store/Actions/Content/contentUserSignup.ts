import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_SIGNUP_CONTENT } from "../../../Shared/constants";
import { IUserSignupContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_USER_SIGNUP_CONTENT = "REQUEST_USER_SIGNUP_CONTENT";
export const RECEIVE_USER_SIGNUP_CONTENT = "RECEIVE_USER_SIGNUP_CONTENT";
export interface IRequestSignupFormContent { type: typeof REQUEST_USER_SIGNUP_CONTENT }
export interface IReceiveSignupFormContent { type: typeof RECEIVE_USER_SIGNUP_CONTENT, payload: IUserSignupContentDto }
export type TKnownActions = IRequestSignupFormContent | IReceiveSignupFormContent;

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
            request: REQUEST_USER_SIGNUP_CONTENT, 
            receive: RECEIVE_USER_SIGNUP_CONTENT, 
            url: GET_SIGNUP_CONTENT 
        });
    }
}