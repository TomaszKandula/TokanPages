import { IApplicationAction } from "../../Configuration";
import { IArticleItem } from "../../../Shared/Components/RenderContent/Models";
import { 
    Execute, 
    GetConfiguration, 
    IExecute, 
    IRequest as IGetRequest, 
    GET_ARTICLE
} from "../../../Api/Request";

export const RESET = "RESET_SELECTION";
export const REQUEST = "REQUEST_ARTICLE";
export const RECEIVE = "RECEIVE_ARTICLE";
interface IReset { type: typeof RESET; }
interface IRequest { type: typeof REQUEST; }
interface IReceive { type: typeof RECEIVE; payload: IArticleItem; }
export type TKnownActions = IReset | IRequest | IReceive;

export const ArticleSelectionAction = 
{
    reset: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: RESET });
    },
    select: (id: string): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: REQUEST });

        const url = GET_ARTICLE.replace("{id}", id);

        const request: IGetRequest = {
            configuration: {
                method: "GET", 
                url: url
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