import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { ArticlesAction } from "../../../Store/Actions";
import { ArticleListView } from "./View/articleListView";

export const ArticleList = (): JSX.Element => 
{
    const dispatch = useDispatch();
    const listArticles = useSelector((state: IApplicationState) => state.listArticles);
    
    React.useEffect(() => 
    { 
        dispatch(ArticlesAction.requestArticles())
    }, 
    [ dispatch ]);

    return (<ArticleListView bind=
    {{
        isLoading: listArticles.isLoading,
        articles: listArticles.articles
    }}/>);
}
