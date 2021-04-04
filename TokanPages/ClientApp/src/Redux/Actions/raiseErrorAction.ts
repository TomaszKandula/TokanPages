import { AppThunkAction } from "../../Redux/applicationState";

export const REQUEST_ARTICLES_ERROR = "REQUEST_ARTICLES_ERROR";
export const REQUEST_ARTICLE_ERROR = "REQUEST_ARTICLE_ERROR";
export const UPDATE_ARTICLE_ERROR = "UPDATE_ARTICLE_ERROR";
export const ADD_SUBSCRIBER_ERROR = "ADD_SUBSCRIBER_ERROR";
export const UPDATE_SUBSCRIBER_ERROR = "UPDATE_SUBSCRIBER_ERROR";
export const REMOVE_SUBSCRIBER_ERROR = "REMOVE_SUBSCRIBER_ERROR";
export const SEND_MESSAGE_ERROR = "SEND_MESSAGE_ERROR";

export interface IRequestArticlesError { type: typeof REQUEST_ARTICLES_ERROR, payload: any }
export interface IRequestArticleError { type: typeof REQUEST_ARTICLE_ERROR, payload: any }
export interface IUpdateArticleError { type: typeof UPDATE_ARTICLE_ERROR, payload: any }
export interface IAddSubscriberError { type: typeof ADD_SUBSCRIBER_ERROR, payload: any }
export interface IUpdateSubscriberError { type: typeof UPDATE_SUBSCRIBER_ERROR, payload: any }
export interface IRemoveSubscriberError { type: typeof REMOVE_SUBSCRIBER_ERROR, payload: any }
export interface ISendMessageError { type: typeof SEND_MESSAGE_ERROR, payload: any }

export type TKnownActions = 
    IRequestArticlesError | 
    IRequestArticleError | 
    IUpdateArticleError | 
    IAddSubscriberError | 
    IUpdateSubscriberError |
    IRemoveSubscriberError | 
    ISendMessageError;

export const ActionCreators = 
{
    raiseError: (knownActions: TKnownActions, payload: any):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: knownActions.type, payload: payload });
    }
}
