import { IAppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_SIGNOUT_CONTENT } from "../../../Shared/constants";
import { IUserSignoutContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_USER_SIGNOUT_CONTENT = "REQUEST_USER_SIGNOUT_CONTENT";
export const RECEIVE_USER_SIGNOUT_CONTENT = "RECEIVE_USER_SIGNOUT_CONTENT";
export interface IRequestSignoutFormContent { type: typeof REQUEST_USER_SIGNOUT_CONTENT }
export interface IReceiveSignoutFormContent { type: typeof RECEIVE_USER_SIGNOUT_CONTENT, payload: IUserSignoutContentDto }
export type TKnownActions = IRequestSignoutFormContent | IReceiveSignoutFormContent;

export const ContentUserSignoutAction = 
{
    get: (): IAppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentUserSignout.content.language;

        if (getState().contentUserSignout.content !== ApplicationDefaults.contentUserSignout.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_USER_SIGNOUT_CONTENT, 
            receive: RECEIVE_USER_SIGNOUT_CONTENT, 
            url: GET_SIGNOUT_CONTENT 
        });
    }
}