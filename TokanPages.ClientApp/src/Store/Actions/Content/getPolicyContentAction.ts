import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { POLICY_URL } from "../../../Shared/constants";
import { IDocumentContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_POLICY_CONTENT = "REQUEST_POLICY_CONTENT";
export const RECEIVE_POLICY_CONTENT = "RECEIVE_POLICY_CONTENT";
export interface IRequestPolicyContent { type: typeof REQUEST_POLICY_CONTENT }
export interface IReceivePolicyContent { type: typeof RECEIVE_POLICY_CONTENT, payload: IDocumentContentDto }
export type TKnownActions = IRequestPolicyContent | IReceivePolicyContent;

export const ActionCreators = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentPolicy.content.language;

        if (getState().contentPolicy.content !== ApplicationDefaults.contentPolicy.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_POLICY_CONTENT, 
            receive: RECEIVE_POLICY_CONTENT, 
            url: POLICY_URL 
        });
    }
}