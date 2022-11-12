import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_POLICY_CONTENT } from "../../../Api/Request";
import { IDocumentContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST = "REQUEST_POLICY_CONTENT";
export const RECEIVE = "RECEIVE_POLICY_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IDocumentContentDto }
export type TKnownActions = IRequest | IReceive;

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
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_POLICY_CONTENT 
        });
    }
}