import SelectArticlesReducer from "./Reducers/selectArticleReducer";
import ListArticlesReducer from "./Reducers/listArticlesReducer";
import SendMessageReducer from "./Reducers/sendMessageReducer";
import AddSubscriberReducer from "./Reducers/addSubscriberReducer";
import UpdateSubscriberReducer from "./Reducers/updateSubscriberReducer";
import RemoveSubscriberReducer from "./Reducers/removeSubscriberReducer";
import UpdateArticleReducer from "./Reducers/updateArticleReducer";

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
