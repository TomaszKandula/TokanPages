import { IArticle } from "Redux/applicationState";
import { SELECT_ARTICLE } from "../Actions/actionTypes";
import { initialState } from "../applicationState";

const SelectArticlesReducer = ( state: {} | undefined, { type, payload }: { type: string, payload: IArticle } ) => 
{

    if (state === undefined) 
        return initialState.selectArticle;

    switch(type)
    {

        case SELECT_ARTICLE: return { 
                id: payload.id,
                title: payload.title,
                desc: payload.desc,
                status: payload.status,
                likes: payload.likes,
                readCount: payload.readCount
            }

        default: return state;

    }

}

export default SelectArticlesReducer;
