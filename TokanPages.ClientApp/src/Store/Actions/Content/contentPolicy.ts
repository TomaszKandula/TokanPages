import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { POLICY_URL } from "../../../Shared/constants";
import { IDocumentContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_POLICY_CONTENT = "REQUEST_POLICY_CONTENT";
export const RECEIVE_POLICY_CONTENT = "RECEIVE_POLICY_CONTENT";
export interface IRequestPolicyContent { type: typeof REQUEST_POLICY_CONTENT }
export interface IReceivePolicyContent { type: typeof RECEIVE_POLICY_CONTENT, payload: IDocumentContentDto }
export type TKnownActions = IRequestPolicyContent | IReceivePolicyContent;

export const ContentPolicyAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentPolicy.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentPolicy.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_POLICY_CONTENT, 
            receive: RECEIVE_POLICY_CONTENT, 
            url: POLICY_URL 
        });
    }
}