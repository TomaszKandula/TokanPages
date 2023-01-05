import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentTestimonials } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentTestimonials";

export const ContentTestimonials: 
    Reducer<IContentTestimonials> = (state: IContentTestimonials | undefined, incomingAction: Action): 
    IContentTestimonials => 
{
    if (state === undefined) return ApplicationDefault.contentTestimonials;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
