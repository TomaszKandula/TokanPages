import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_SIGNUP_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "../applicationError";
import { IUserSignupContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_USER_SIGNUP_CONTENT = "REQUEST_USER_SIGNUP_CONTENT";
export const RECEIVE_USER_SIGNUP_CONTENT = "RECEIVE_USER_SIGNUP_CONTENT";
export interface IRequestSignupFormContent { type: typeof REQUEST_USER_SIGNUP_CONTENT }
export interface IReceiveSignupFormContent { type: typeof RECEIVE_USER_SIGNUP_CONTENT, payload: IUserSignupContentDto }
export type TKnownActions = IRequestSignupFormContent | IReceiveSignupFormContent | TErrorActions;

export const ActionCreators = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentUserSignup.content.language;

        if (getState().contentUserSignup.content !== ApplicationDefaults.contentUserSignup.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_USER_SIGNUP_CONTENT, 
            receive: RECEIVE_USER_SIGNUP_CONTENT, 
            url: GET_SIGNUP_CONTENT 
        });
    }
}