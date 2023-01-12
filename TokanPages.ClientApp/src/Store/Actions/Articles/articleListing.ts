import { IApplicationAction } from "../../Configuration";
import { IArticleItem } from "../../../Shared/Components/RenderContent/Models";
import { 
    Execute, 
    GetConfiguration, 
    IExecute, 
    IRequest as IGetRequest, 
    GET_ARTICLES
} from "../../../Api/Request";

export const REQUEST = "REQUEST_ARTICLES";
export const RECEIVE = "RECEIVE_ARTICLES";
interface IRequest { type: typeof REQUEST; }
interface IReceive { type: typeof RECEIVE; payload: IArticleItem[]; }
export type TKnownActions = IRequest | IReceive;

export const ArticleListingAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: REQUEST });

        const request: IGetRequest = {
            configuration: {
                method: "GET", 
                url: GET_ARTICLES,
                responseType: "json"
            }
        }
    
        const input: IExecute = {
            configuration: GetConfiguration(request),
            dispatch: dispatch,
            responseType: RECEIVE
        }

        Execute(input); 
    }
};
