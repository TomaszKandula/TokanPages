import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentPageDataState } from "../../States";
import { ContentPageData as ContentPageDataDefault } from "../../Defaults"; 
import { OperationStatus } from "../../../Shared/enums";
import { UpdateComponents } from "../../../Shared/Services/Utilities";
import { TKnownActions, CLEAR, RECEIVE, REQUEST } from "../../Actions/Content/contentPageData";

export const ContentPageData: Reducer<ContentPageDataState> = (
    state: ContentPageDataState | undefined,
    incomingAction: Action
): ContentPageDataState => {
    if (state === undefined) return ApplicationDefault.contentPageData;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                ...ContentPageDataDefault,
            };
        case REQUEST:
            return {
                status: OperationStatus.inProgress,
                isLoading: true,
                languageId: state.languageId,
                components: state.components,
            };
        case RECEIVE:
            return {
                status: OperationStatus.hasFinished,
                isLoading: false,
                languageId: action.payload.language,
                components: UpdateComponents(state, action.payload.components)
            };
        default:
            return state;
    }
};
