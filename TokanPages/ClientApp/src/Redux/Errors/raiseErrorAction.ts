import { AppThunkAction } from "Redux/applicationState";

export const API_ADD_SUBSCRIBER_ERROR = "API_ADD_SUBSCRIBER_ERROR";

export interface IApiAddSubscriberError { type: typeof API_ADD_SUBSCRIBER_ERROR, payload: any }

export type TKnownActions = IApiAddSubscriberError;

export const ActionCreators = 
{
    raiseError: (knownActions: TKnownActions, payload: any):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: knownActions.type, payload: payload });
    }
}
