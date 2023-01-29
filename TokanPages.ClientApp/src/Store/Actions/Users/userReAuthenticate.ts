import axios from "axios";
import { ApplicationAction } from "../../Configuration";
import { AuthenticateUserResultDto, ReAuthenticateUserDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR, USER_DATA } from "../../../Shared/constants";
import { UPDATE, TKnownActions as TUpdateActions } from "./userDataStore";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GetDataFromStorage } from "../../../Shared/Services/StorageServices";
import { 
    REAUTHENTICATE as REAUTHENTICATE_USER, 
    RequestContract,
    GetConfiguration
} from "../../../Api/Request";

export const REAUTHENTICATE = "REAUTHENTICATE_USER";
export const CLEAR = "REAUTHENTICATE_USER_CLEAR";
export const RESPONSE = "REAUTHENTICATE_USER_RESPONSE";
interface ReAuthenticate { type: typeof REAUTHENTICATE }
interface Clear { type: typeof CLEAR }
interface Response { type: typeof RESPONSE; payload: any; }
export type TKnownActions = ReAuthenticate | Clear | Response | TUpdateActions;

//TODO: refactor, simplify
export const UserReAuthenticateAction = 
{
    clear: (): ApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    reAuthenticate: (): ApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REAUTHENTICATE });

        const userData = GetDataFromStorage({ key: USER_DATA }) as AuthenticateUserResultDto;
        const payload: ReAuthenticateUserDto = 
        {
            refreshToken: userData.refreshToken
        }

        const request: RequestContract = {
            configuration: {
                method: "POST", 
                url: REAUTHENTICATE_USER, 
                data: payload
            }
        }

        axios(GetConfiguration(request))
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
