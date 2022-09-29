import { Action, Reducer } from "redux";
import { CombinedDefaults } from "../../Configuration";
import { IGetHeaderContent } from "../../States/Content/getHeaderContentState";
import { 
    TKnownActions,
    RECEIVE_HEADER_CONTENT, 
    REQUEST_HEADER_CONTENT
} from "../../Actions/Content/getHeaderContentAction";

export const GetHeaderContentReducer: Reducer<IGetHeaderContent> = (state: IGetHeaderContent | undefined, incomingAction: Action): IGetHeaderContent => 
{
    if (state === undefined) return CombinedDefaults.getHeaderContent;

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
