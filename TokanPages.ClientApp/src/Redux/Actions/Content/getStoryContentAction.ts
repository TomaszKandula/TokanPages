import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { STORY_URL, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IDocumentContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";

export const REQUEST_STORY_CONTENT = "REQUEST_STORY_CONTENT";
export const RECEIVE_STORY_CONTENT = "RECEIVE_STORY_CONTENT";
export interface IRequestStoryContent { type: typeof REQUEST_STORY_CONTENT }
export interface IReceiveStoryContent { type: typeof RECEIVE_STORY_CONTENT, payload: IDocumentContentDto }
export type TKnownActions = IRequestStoryContent | IReceiveStoryContent | TErrorActions;

export const ActionCreators = 
{
    getStoryContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getStoryContent.content.language;

        if (getState().getStoryContent.content !== combinedDefaults.getStoryContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_STORY_CONTENT });

        const id = getState().userLanguage.id;
        const queryParam = id === "" ? "" : `&language=${id}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${STORY_URL}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_STORY_CONTENT, payload: response.data });
            }

            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}