import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ActionCreators } from "../../Redux/Actions/Articles/listArticlesAction";
import { IApplicationState } from "../../Redux/applicationState";
import ArticleListView from "./articleListView";

export default function ArticleList() 
{
    const dispatch = useDispatch();
    const listArticles = useSelector((state: IApplicationState) => state.listArticles);
    const fetchData = React.useCallback(() => dispatch(ActionCreators.requestArticles()), [ dispatch ]);
    
    React.useEffect(() => { fetchData() }, [ fetchData ]);

    return (<ArticleListView bind=
    {{
        isLoading: listArticles.isLoading,
        articles: listArticles.articles
    }}/>);
}
