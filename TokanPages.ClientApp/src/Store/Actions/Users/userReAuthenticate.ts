import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IAuthenticateUserResultDto, IReAuthenticateUserDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR, USER_DATA } from "../../../Shared/constants";
import { UPDATE, TKnownActions as TUpdateActions } from "./userDataStore";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GetDataFromStorage } from "../../../Shared/Services/StorageServices";
import { EnrichConfiguration } from "../../../Api/Request";

export const REAUTHENTICATE = "REAUTHENTICATE_USER";
export const CLEAR = "REAUTHENTICATE_USER_CLEAR";
export const RESPONSE = "REAUTHENTICATE_USER_RESPONSE";
interface IReAuthenticate { type: typeof REAUTHENTICATE }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IReAuthenticate | IClear | IResponse | TUpdateActions;

export const UserReAuthenticateAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    reAuthenticate: (): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REAUTHENTICATE });

        const userData = GetDataFromStorage({ key: USER_DATA }) as IAuthenticateUserResultDto;
        const payload: IReAuthenticateUserDto = 
        {
            refreshToken: userData.refreshToken
        }

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: REAUTHENTICATE, 
            data: payload
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                const pushData = () => 
                {
                    dispatch({ type: RESPONSE, payload: response.data });
                    dispatch({ type: UPDATE, payload: response.data });
                }
                
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : pushData();
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status}) });
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}
