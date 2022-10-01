import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_SIGNIN_CONTENT } from "../../../Shared/constants";
import { IUserSigninContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_USER_SIGNIN_CONTENT = "REQUEST_USER_SIGNIN_CONTENT";
export const RECEIVE_USER_SIGNIN_CONTENT = "RECEIVE_USER_SIGNIN_CONTENT";
export interface IRequestSigninFormContent { type: typeof REQUEST_USER_SIGNIN_CONTENT }
export interface IReceiveSigninFormContent { type: typeof RECEIVE_USER_SIGNIN_CONTENT, payload: IUserSigninContentDto }
export type TKnownActions = IRequestSigninFormContent | IReceiveSigninFormContent;

export const ActionCreators = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentUserSignin.content.language;

        if (getState().contentUserSignin.content !== ApplicationDefaults.contentUserSignin.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_USER_SIGNIN_CONTENT, 
            receive: RECEIVE_USER_SIGNIN_CONTENT, 
            url: GET_SIGNIN_CONTENT 
        });
    }
}