import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IGetFooterContent } from "../../States";
import { 
    TKnownActions,
    RECEIVE_FOOTER_CONTENT, 
    REQUEST_FOOTER_CONTENT
} from "../../Actions/Content/getFooterContentAction";

export const GetFooterContentReducer: 
    Reducer<IGetFooterContent> = (state: IGetFooterContent | undefined, incomingAction: Action): 
    IGetFooterContent => 
{
    if (state === undefined) return ApplicationDefaults.contentFooter;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_FOOTER_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_FOOTER_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
