import { AppThunkAction } from "../../applicationState";
import { ITextObject } from "../../../Shared/Components/RenderContent/Models";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { GetErrorMessage } from "../../../Shared/Services/ErrorServices";
import { POLICY_URL, STORY_URL, TERMS_URL } from "../../../Shared/constants";
import { GetUserLanguage } from "../../../Shared/Services/languageService";
import { RAISE_ERROR, TErrorActions } from "./../raiseErrorAction";
import { ApiCall, EnrichConfiguration } from "../../../Api/Request";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

export const REQUEST_STORY = "REQUEST_STORY";
export const REQUEST_TERMS = "REQUEST_TERMS";
export const REQUEST_POLICY = "REQUEST_POLICY";

export const RECEIVE_STORY = "RECEIVE_STORY";
export const RECEIVE_TERMS = "RECEIVE_TERMS";
export const RECEIVE_POLICY = "RECEIVE_POLICY";

export interface IRequestStory { type: typeof REQUEST_STORY }
export interface IRequestTerms { type: typeof REQUEST_TERMS }
export interface IRequestPolicy { type: typeof REQUEST_POLICY }

export interface IReceiveStory { type: typeof RECEIVE_STORY, payload: ITextObject }
export interface IReceiveTerms { type: typeof RECEIVE_TERMS, payload: ITextObject }
export interface IReceivePolicy { type: typeof RECEIVE_POLICY, payload: ITextObject }

export type TKnownActions = IRequestStory | IRequestTerms | IRequestPolicy | IReceiveStory | IReceiveTerms | IReceivePolicy | TErrorActions;
export type TReceiveContent = typeof RECEIVE_STORY | typeof RECEIVE_TERMS | typeof RECEIVE_POLICY;
export type TRequestContent = typeof REQUEST_STORY | typeof REQUEST_TERMS | typeof REQUEST_POLICY;

const DispatchCall = async (dispatch: (action: TKnownActions) => void, url: string, request: TRequestContent, receive: TReceiveContent) =>
{
    dispatch({ type: request });

    let result = await ApiCall(EnrichConfiguration(
    {
        url: url,
        method: "GET",
        responseType: "json"
    }));

    if (result.error !== null)
    {
        dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage({ errorObject: result.error }) });
        return;
    }

    if (result.status === 200)
    {
        dispatch({ type: receive, payload: result.content.content });
        return;
    }

    const error = GetTextStatusCode({ statusCode: result.status as number });
    dispatch({ type: RAISE_ERROR, errorObject: error });
}

export const ActionCreators = 
{
    getStoryContent: (isLanguageChanged: boolean = false):  AppThunkAction<TKnownActions> => async (dispatch, getState) => 
    {
        if (getState().getStaticContent.story !== combinedDefaults.getStaticContent.story && !isLanguageChanged) 
            return;

        const language = GetUserLanguage();
        const queryParam = language === "" ? "" : `&language=${language}`;

        await DispatchCall(dispatch, `${STORY_URL}${queryParam}`, REQUEST_STORY, RECEIVE_STORY);
    },
    getTermsContent: (isLanguageChanged: boolean = false):  AppThunkAction<TKnownActions> => async (dispatch, getState) => 
    {
        if (getState().getStaticContent.terms !== combinedDefaults.getStaticContent.terms && !isLanguageChanged) 
            return;

        const language = GetUserLanguage();
        const queryParam = language === "" ? "" : `&language=${language}`;

        await DispatchCall(dispatch, `${TERMS_URL}${queryParam}`, REQUEST_TERMS, RECEIVE_TERMS);
    },
    getPolicyContent: (isLanguageChanged: boolean = false):  AppThunkAction<TKnownActions> => async (dispatch, getState) => 
    {
        if (getState().getStaticContent.policy !== combinedDefaults.getStaticContent.policy && !isLanguageChanged) 
            return;

        const language = GetUserLanguage();
        const queryParam = language === "" ? "" : `&language=${language}`;

        await DispatchCall(dispatch, `${POLICY_URL}${queryParam}`, REQUEST_POLICY, RECEIVE_POLICY);
    }
}
