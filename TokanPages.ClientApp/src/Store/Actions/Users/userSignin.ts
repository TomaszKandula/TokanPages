import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IAuthenticateUserDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { UPDATE, TKnownActions as TUpdateActions } from "./userDataStore";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { 
    AUTHENTICATE as AUTHENTICATE_USER, 
    IRequest, 
    GetConfiguration 
} from "../../../Api/Request";

export const SIGNIN = "SIGNIN_USER";
export const CLEAR = "SIGNIN_USER_CLEAR";
export const RESPONSE = "SIGNIN_USER_RESPONSE";
interface ISignin { type: typeof SIGNIN }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = ISignin | IClear | IResponse | TUpdateActions;

//TODO: refactor, simplify
export const UserSigninAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    signin: (payload: IAuthenticateUserDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SIGNIN });

        const request: IRequest = {
            configuration: {
                method: "POST", 
                url: AUTHENTICATE_USER, 
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
                ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                : pushData();
            }
            
            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}
