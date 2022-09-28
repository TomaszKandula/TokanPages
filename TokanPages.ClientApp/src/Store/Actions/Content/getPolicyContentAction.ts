import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { POLICY_URL } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IDocumentContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_POLICY_CONTENT = "REQUEST_POLICY_CONTENT";
export const RECEIVE_POLICY_CONTENT = "RECEIVE_POLICY_CONTENT";
export interface IRequestPolicyContent { type: typeof REQUEST_POLICY_CONTENT }
export interface IReceivePolicyContent { type: typeof RECEIVE_POLICY_CONTENT, payload: IDocumentContentDto }
export type TKnownActions = IRequestPolicyContent | IReceivePolicyContent | TErrorActions;

export const ActionCreators = 
{
    getPolicyContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getPolicyContent.content.language;

        if (getState().getPolicyContent.content !== combinedDefaults.getPolicyContent.content && !isLanguageChanged) 
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