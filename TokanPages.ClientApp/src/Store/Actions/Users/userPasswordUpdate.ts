import { IApplicationAction } from "../../Configuration";
import { IUpdateUserPasswordDto } from "../../../Api/Models";
import { 
    Execute, 
    GetConfiguration, 
    IExecute, 
    IRequest, 
    UPDATE_USER_PASSWORD
} from "../../../Api/Request";

export const UPDATE = "UPDATE_USER_PASSWORD";
export const CLEAR = "UPDATE_USER_PASSWORD_CLEAR";
export const RESPONSE = "UPDATE_USER_PASSWORD_RESPONSE";
interface IUpdate { type: typeof UPDATE }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IUpdate | IClear | IResponse;

export const UserPasswordUpdateAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    update: (payload: IUpdateUserPasswordDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE });

        const request: IRequest = {
            configuration: {
                method: "POST", 
                url: UPDATE_USER_PASSWORD, 
                data: payload
            }
        }
    
        const input: IExecute = {
            configuration: GetConfiguration(request),
            dispatch: dispatch,
            responseType: RESPONSE
        }

        Execute(input);
    }
}
