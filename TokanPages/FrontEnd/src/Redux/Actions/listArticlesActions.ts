import { LIST_ARTICLES } from "./actionTypes";
import { initialState } from "../applicationState";

export const ListArticles = 
{
    type: LIST_ARTICLES,
    payload: initialState.listArticles
};
