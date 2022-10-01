import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentStory } from "../../States";
import { 
    TKnownActions,
    RECEIVE_STORY_CONTENT, 
    REQUEST_STORY_CONTENT
} from "../../Actions/Content/getStoryContentAction";

export const ContentStory: 
    Reducer<IContentStory> = (state: IContentStory | undefined, incomingAction: Action): 
    IContentStory => 
{
    if (state === undefined) return ApplicationDefaults.contentStory;

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
