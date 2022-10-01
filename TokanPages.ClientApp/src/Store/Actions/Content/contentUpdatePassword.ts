import { IAppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_UPDATE_PASSWORD_CONTENT } from "../../../Shared/constants";
import { IUpdatePasswordContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_UPDATE_PASSWORD_CONTENT = "REQUEST_UPDATE_PASSWORD_CONTENT";
export const RECEIVE_UPDATE_PASSWORD_CONTENT = "RECEIVE_UPDATE_PASSWORD_CONTENT";
export interface IRequestUpdatePasswordContent { type: typeof REQUEST_UPDATE_PASSWORD_CONTENT }
export interface IReceiveUpdatePasswordContent { type: typeof RECEIVE_UPDATE_PASSWORD_CONTENT, payload: IUpdatePasswordContentDto }
export type TKnownActions = IRequestUpdatePasswordContent | IReceiveUpdatePasswordContent;

export const ContentUpdatePasswordAction = 
{
    get: (): IAppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentUpdatePassword.content.language;

        if (getState().contentUpdatePassword.content !== ApplicationDefaults.contentUpdatePassword.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_UPDATE_PASSWORD_CONTENT, 
            receive: RECEIVE_UPDATE_PASSWORD_CONTENT, 
            url: GET_UPDATE_PASSWORD_CONTENT 
        });
    }
}