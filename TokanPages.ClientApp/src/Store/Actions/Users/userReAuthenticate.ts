import axios from "axios";
import { IAppThunkAction } from "../../Configuration";
import { IAuthenticateUserResultDto, IReAuthenticateUserDto } from "../../../Api/Models";
import { API_COMMAND_REAUTHENTICATE, NULL_RESPONSE_ERROR, USER_DATA } from "../../../Shared/constants";
import { UPDATE_USERDATA, TKnownActions as TUpdateActions } from "./userDataStore";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GetDataFromStorage } from "../../../Shared/Services/StorageServices";
import { EnrichConfiguration } from "../../../Api/Request";

export const REAUTHENTICATE_USER = "REAUTHENTICATE_USER";
export const REAUTHENTICATE_USER_CLEAR = "REAUTHENTICATE_USER_CLEAR";
export const REAUTHENTICATE_USER_RESPONSE = "REAUTHENTICATE_USER_RESPONSE";
export interface IReAuthenticateUser { type: typeof REAUTHENTICATE_USER }
export interface IReAuthenticateUserClear { type: typeof REAUTHENTICATE_USER_CLEAR }
export interface IReAuthenticateUserResponse { type: typeof REAUTHENTICATE_USER_RESPONSE }
export type TKnownActions = IReAuthenticateUser | IReAuthenticateUserClear | IReAuthenticateUserResponse | TUpdateActions;

export const UserReAuthenticateAction = 
{
    clear: (): IAppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: REAUTHENTICATE_USER_CLEAR });
    },
    reAuthenticate: (): IAppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REAUTHENTICATE_USER });

        const userData = GetDataFromStorage({ key: USER_DATA }) as IAuthenticateUserResultDto;
        const payload: IReAuthenticateUserDto = 
        {
            refreshToken: userData.refreshToken
        }

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_REAUTHENTICATE, 
            data: payload
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                const pushData = () => 
                {
                    dispatch({ type: REAUTHENTICATE_USER_RESPONSE });
                    dispatch({ type: UPDATE_USERDATA, payload: response.data });
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
