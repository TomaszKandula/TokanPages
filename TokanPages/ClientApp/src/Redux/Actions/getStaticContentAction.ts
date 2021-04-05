import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { ITextItem } from "../../Shared/Components/ContentRender/Models/textModel";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { POLICY_URL, STORY_URL, TERMS_URL } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";

export const REQUEST_STORY = "REQUEST_STORY";
export const REQUEST_TERMS = "REQUEST_TERMS";
export const REQUEST_POLICY = "REQUEST_POLICY";

export const RECEIVE_STORY = "RECEIVE_STORY";
export const RECEIVE_TERMS = "RECEIVE_TERMS";
export const RECEIVE_POLICY = "RECEIVE_POLICY";

export interface IRequestStory { type: typeof REQUEST_STORY }
export interface IRequestTerms { type: typeof REQUEST_TERMS }
export interface IRequestPolicy { type: typeof REQUEST_POLICY }

export interface IReceiveStory { type: typeof RECEIVE_STORY, payload: ITextItem[] }
export interface IReceiveTerms { type: typeof RECEIVE_TERMS, payload: ITextItem[] }
export interface IReceivePolicy { type: typeof RECEIVE_POLICY, payload: ITextItem[] }

export type TKnownActions = 
    IRequestStory | 
    IRequestTerms | 
    IRequestPolicy |
    IReceiveStory | 
    IReceiveTerms | 
    IReceivePolicy | 
    TErrorActions
;

export type TKnownContent = 
    typeof RECEIVE_STORY | 
    typeof RECEIVE_TERMS | 
    typeof RECEIVE_POLICY
;

const GetDataFromUr = 
{
    getContent: (url: string, type: TKnownContent):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        axios.get(url, 
        {
            method: "GET", 
            responseType: "json"
        })
        .then(response =>
        {
            return response.status === 200
                ? dispatch({ type: type, payload: response.data })
                : dispatch({ type: RAISE_ERROR, errorObject: UnexpectedStatusCode(response.status) });   
            })
        .catch(error =>
        {
            dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage(error) });
        });
    }
}

export const ActionCreators = 
{
    getStoryContent: ():  AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REQUEST_STORY });
        GetDataFromUr.getContent(STORY_URL, RECEIVE_STORY);
    },
    getTermsContent: ():  AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REQUEST_TERMS });
        GetDataFromUr.getContent(TERMS_URL, RECEIVE_TERMS);
    },
    getPolicyContent: ():  AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REQUEST_POLICY });
        GetDataFromUr.getContent(POLICY_URL, RECEIVE_POLICY);
    }
}
