import SelectArticlesReducer from "./selectArticleReducer";
import ListArticlesReducer from "./listArticlesReducer";

export const combinedReducers = 
{
    selectArticle: SelectArticlesReducer,
    listArticles: ListArticlesReducer
};
