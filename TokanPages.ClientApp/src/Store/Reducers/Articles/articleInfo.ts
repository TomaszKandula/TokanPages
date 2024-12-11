import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ArticleInfoState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST } from "../../Actions/Articles/articleInfo";
import { ArticleItemBase } from "../../../Shared/Components/RenderContent/Models";

export const ArticleInfo: Reducer<ArticleInfoState> = (
    state: ArticleInfoState | undefined,
    incomingAction: Action
): ArticleInfoState => {
    if (state === undefined) return ApplicationDefault.articleInfo;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case REQUEST:
            return {
                isLoading: true,
                collectedInfo: [],
            };

        case RECEIVE:
            let data = [];
            if (state.collectedInfo && state.collectedInfo.length > 0) {
                data = { ...state.collectedInfo };
                data = data.filter((value: ArticleItemBase) => value.id !== action.payload.id);
                data.push(action.payload);
            } else {
                data.push(action.payload);
            }

            return {
                isLoading: false,
                collectedInfo: data,
            };

        default:
            return state;
    }
};
