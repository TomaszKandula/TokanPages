import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IGetArticleFeatContent } from "../States/getArticleFeatContentState";
import { 
    TKnownActions,
    RECEIVE_ARTICE_FEATURES, 
    REQUEST_ARTICE_FEATURES
} from "../Actions/getArticleFeatContentAction";

const GetArticleFeatContentReducer: Reducer<IGetArticleFeatContent> = (state: IGetArticleFeatContent | undefined, incomingAction: Action): IGetArticleFeatContent => 
{
    if (state === undefined) return combinedDefaults.getArticleFeatContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_ARTICE_FEATURES:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_ARTICE_FEATURES:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}

export default GetArticleFeatContentReducer;
