import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentTestimonials } from "../../States";
import { 
    TKnownActions,
    RECEIVE_TESTIMONIALS_CONTENT, 
    REQUEST_TESTIMONIALS_CONTENT
} from "../../Actions/Content/getTestimonialsContentAction";

export const ContentTestimonials: 
    Reducer<IContentTestimonials> = (state: IContentTestimonials | undefined, incomingAction: Action): 
    IContentTestimonials => 
{
    if (state === undefined) return ApplicationDefaults.contentTestimonials;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_TESTIMONIALS_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_TESTIMONIALS_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
