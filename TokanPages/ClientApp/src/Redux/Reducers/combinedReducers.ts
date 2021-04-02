import SelectArticlesReducer from "./selectArticleReducer";
import ListArticlesReducer from "./listArticlesReducer";
import SendMessageReducer from "./sendMessageReducer";

export const combinedReducers = 
{
    selectArticle: SelectArticlesReducer,
    listArticles: ListArticlesReducer,
    sendMessage: SendMessageReducer
};
