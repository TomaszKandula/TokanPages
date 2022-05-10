import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetStaticContent } from "../../../Redux/States/Content/getStaticContentState";
import { 
    TKnownActions, 
    RECEIVE_POLICY, 
    RECEIVE_STORY, 
    RECEIVE_TERMS, 
    REQUEST_POLICY, 
    REQUEST_STORY, 
    REQUEST_TERMS 
} from "../../../Redux/Actions/Content/getStaticContentAction";

export const GetStaticContentReducer: Reducer<IGetStaticContent> = (state: IGetStaticContent | undefined, incomingAction: Action): IGetStaticContent => 
{
    if (state === undefined) return combinedDefaults.getStaticContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_STORY:
            return { 
                ...state,
                story: { isLoading: true, items: [] }
            }

        case RECEIVE_STORY:
            return { 
                ...state,
                story: { isLoading: false, items: action.payload.items }
            }

        case REQUEST_TERMS:
            return { 
                ...state,
                terms: { isLoading: true, items: [] }
            }

        case RECEIVE_TERMS:
            return { 
                ...state,
                terms: { isLoading: false, items: action.payload.items }
            }
        
        case REQUEST_POLICY:
            return { 
                ...state,
                policy: { isLoading: true, items: [] }
            }

        case RECEIVE_POLICY:
            return { 
                ...state,
                policy: { isLoading: false, items: action.payload.items }
            }

        default: return state;
    }
}
