import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { ArticlesAction } from "../../../Store/Actions";
import { ArticleListView } from "./View/articleListView";

export const ArticleList = (): JSX.Element => 
{
    const dispatch = useDispatch();
    const state = useSelector((state: IApplicationState) => state.articleListing);
    
    React.useEffect(() => 
    { 
        dispatch(ArticlesAction.get())
    }, 
    [ dispatch ]);

    return (<ArticleListView bind=
    {{
        isLoading: state.isLoading,
        articles: state.articles
    }}/>);
}
