import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IGetNotFoundContent } from "../../Redux/States/getNotFoundContentState";
import { 
    TKnownActions, 
    REQUEST_NOTFOUND_CONTENT, 
    RECEIVE_NOTFOUND_CONTENT 
} from "../../Redux/Actions/getNotFoundContentAction";

const GetNotFoundContentReducer: Reducer<IGetNotFoundContent> = (state: IGetNotFoundContent | undefined, incomingAction: Action): IGetNotFoundContent => 
{
    if (state === undefined) return combinedDefaults.getNotFoundContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_NOTFOUND_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_NOTFOUND_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}

export default GetNotFoundContentReducer;
