import { IArticles } from "Redux/applicationState";
import { LIST_ARTICLES } from "../Actions/actionTypes";
import { initialState } from "../applicationState";

const ListArticlesReducer = ( state = {}, { type, payload }: { type: string, payload: IArticles } ) => 
{

    if (state === undefined) 
        return initialState.listArticles;

    switch(type)
    {

        case LIST_ARTICLES: return { 
            articles: payload.articles, 
            isLoading: payload.isLoading 
        }

        default: return state;

    }

}

export default ListArticlesReducer;
