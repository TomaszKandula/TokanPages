import { IApplicationAction } from "../../Configuration";
import { IUpdateUserDto } from "../../../Api/Models";
import { 
    Execute, 
    GetConfiguration, 
    IExecute, 
    IRequest, 
    UPDATE_USER
} from "../../../Api/Request";

export const UPDATE = "UPDATE_USER";
export const CLEAR = "UPDATE_USER_CLEAR";
export const RESPONSE = "UPDATE_USER_RESPONSE";
interface IUpdate { type: typeof UPDATE }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IUpdate | IClear | IResponse;

export const UserUpdateAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    update: (payload: IUpdateUserDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE });

        const request: IRequest = {
            configuration: {
                method: "POST", 
                url: UPDATE_USER, 
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
