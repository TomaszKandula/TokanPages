import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetStoryContent } from "../../../Redux/States/Content/getStoryContentState";
import { 
    TKnownActions,
    RECEIVE_STORY_CONTENT, 
    REQUEST_STORY_CONTENT
} from "../../../Redux/Actions/Content/getStoryContentAction";

export const GetStoryContentReducer: Reducer<IGetStoryContent> = (state: IGetStoryContent | undefined, incomingAction: Action): IGetStoryContent => 
{
    if (state === undefined) return combinedDefaults.getStoryContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_STORY_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_STORY_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
