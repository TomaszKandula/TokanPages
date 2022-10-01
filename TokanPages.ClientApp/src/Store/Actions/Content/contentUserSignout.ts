import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_SIGNOUT_CONTENT } from "../../../Shared/constants";
import { IUserSignoutContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_USER_SIGNOUT_CONTENT = "REQUEST_USER_SIGNOUT_CONTENT";
export const RECEIVE_USER_SIGNOUT_CONTENT = "RECEIVE_USER_SIGNOUT_CONTENT";
export interface IRequestSignoutFormContent { type: typeof REQUEST_USER_SIGNOUT_CONTENT }
export interface IReceiveSignoutFormContent { type: typeof RECEIVE_USER_SIGNOUT_CONTENT, payload: IUserSignoutContentDto }
export type TKnownActions = IRequestSignoutFormContent | IReceiveSignoutFormContent;

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
            request: REQUEST_USER_SIGNOUT_CONTENT, 
            receive: RECEIVE_USER_SIGNOUT_CONTENT, 
            url: GET_SIGNOUT_CONTENT 
        });
    }
}