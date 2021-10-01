import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../../applicationState";
import { ITextObject } from "../../../Shared/Components/ContentRender/Models/textModel";
import { GetErrorMessage } from "../../../Shared/helpers";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
import { POLICY_URL, STORY_URL, TERMS_URL } from "../../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./../raiseErrorAction";
import { ApiCall, EnrichConfiguration } from "../../../Api/Request";

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
        dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage(result.error) });
        Sentry.captureException(result.error);
        return;
    }

    if (result.status === 200)
    {
        dispatch({ type: receive, payload: result.content });
        return;
    }

    const error = UnexpectedStatusCode(result.status as number);
    dispatch({ type: RAISE_ERROR, errorObject: error });
    Sentry.captureException(error);
}

export const ActionCreators = 
{
    getStoryContent: ():  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        await DispatchCall(dispatch, STORY_URL, REQUEST_STORY, RECEIVE_STORY);
    },
    getTermsContent: ():  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        await DispatchCall(dispatch, TERMS_URL, REQUEST_TERMS, RECEIVE_TERMS);
    },
    getPolicyContent: ():  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        await DispatchCall(dispatch, POLICY_URL, REQUEST_POLICY, RECEIVE_POLICY);
    }
}
