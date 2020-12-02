import { SELECT_ARTICLE } from "./actionTypes";
import { initialState } from "../applicationState";

export const SelectArticle = 
{
    type: SELECT_ARTICLE,
    payload: initialState.selectArticle
};
