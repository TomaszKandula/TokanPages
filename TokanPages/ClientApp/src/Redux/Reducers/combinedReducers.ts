import SelectArticlesReducer from "./selectArticleReducer";
import ListArticlesReducer from "./listArticlesReducer";
import SendMessageReducer from "./sendMessageReducer";
import AddSubscriberReducer from "./addSubscriberReducer";
import UpdateSubscriberReducer from "./updateSubscriberReducer";
import RemoveSubscriberReducer from "./removeSubscriberReducer";
import UpdateArticleReducer from "./updateArticleReducer";

export const combinedReducers = 
{
    selectArticle: SelectArticlesReducer,
    listArticles: ListArticlesReducer,
    sendMessage: SendMessageReducer,
    addSubscriber: AddSubscriberReducer,
    updateSubscriber: UpdateSubscriberReducer,
    removeSubscriber: RemoveSubscriberReducer,
    updateArticle: UpdateArticleReducer
};
