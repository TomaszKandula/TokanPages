import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IGetHeaderContent } from "../../Redux/States/getHeaderContentState";
import { 
    TKnownActions,
    RECEIVE_HEADER_CONTENT, 
    REQUEST_HEADER_CONTENT
} from "../../Redux/Actions/getHeaderContentAction";

const GetHeaderContentReducer: Reducer<IGetHeaderContent> = (state: IGetHeaderContent | undefined, incomingAction: Action): IGetHeaderContent => 
{
    if (state === undefined) return combinedDefaults.getHeaderContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_HEADER_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_HEADER_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}

export default GetHeaderContentReducer;
