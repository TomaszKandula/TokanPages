import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentStory } from "../../States";

import { 
    TKnownActions,
    RECEIVE_STORY_CONTENT, 
    REQUEST_STORY_CONTENT
} from "../../Actions/Content/contentStory";

export const ContentStory: 
    Reducer<IContentStory> = (state: IContentStory | undefined, incomingAction: Action): 
    IContentStory => 
{
    if (state === undefined) return ApplicationDefault.contentStory;

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
