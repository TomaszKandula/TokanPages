import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ActionCreators } from "../../Redux/Actions/Articles/listArticlesAction";
import { IApplicationState } from "../../Redux/applicationState";
import ArticleListView from "./articleListView";

const ArticleList = (): JSX.Element => 
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

export default ArticleList;
