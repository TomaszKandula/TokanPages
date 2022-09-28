import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ActionCreators } from "../../../Store/Actions/Articles/listArticlesAction";
import { IApplicationState } from "../../../Store/applicationState";
import { ArticleListView } from "./View/articleListView";

export const ArticleList = (): JSX.Element => 
{
    const dispatch = useDispatch();
    const listArticles = useSelector((state: IApplicationState) => state.listArticles);
    
    React.useEffect(() => 
    { 
        dispatch(ActionCreators.requestArticles())
    }, 
    [ dispatch ]);

    return (<ArticleListView bind=
    {{
        isLoading: listArticles.isLoading,
        articles: listArticles.articles
    }}/>);
}
