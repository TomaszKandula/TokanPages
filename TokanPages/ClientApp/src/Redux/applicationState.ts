import { IArticle } from "./States/selectArticleState";
import { IArticles } from "./States/listArticlesState";
import { ISendMessage } from "./States/sendMessageState";
import { IAddSubscriber } from "./States/addSubscriberState";
import { IUpdateSubscriber } from "./States/updateSubscriberState";
import { IRemoveSubscriber } from "./States/removeSubscriberState";
import { IUpdateArticle } from "./States/updateArticleState";
import { IRaiseError } from "./Errors/raiseErrorState";

export interface IApplicationState 
{
    raiseError: IRaiseError;
    selectArticle: IArticle;
    listArticles: IArticles;
    updateArticle: IUpdateArticle,
    sendMessage: ISendMessage,
    addSubscriber: IAddSubscriber,
    updateSubscriber: IUpdateSubscriber,
    removeSubscriber: IRemoveSubscriber
}

export interface AppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}
