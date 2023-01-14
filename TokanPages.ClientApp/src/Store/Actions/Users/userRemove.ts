import { IApplicationAction } from "../../Configuration";
import { IRemoveUserDto } from "../../../Api/Models";
import { 
    Execute, 
    GetConfiguration, 
    IExecute, 
    IRequest, 
    REMOVE_USER
} from "../../../Api/Request";

export const REMOVE = "REMOVE_ACCOUNT";
export const CLEAR = "REMOVE_ACCOUNT_CLEAR";
export const RESPONSE = "REMOVE_ACCOUNT_RESPONSE";
interface IRemove { type: typeof REMOVE }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IRemove | IClear | IResponse;

export const UserRemoveAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    remove: (payload: IRemoveUserDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REMOVE });

        const request: IRequest = {
            configuration: {
                method: "POST", 
                url: REMOVE_USER, 
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
