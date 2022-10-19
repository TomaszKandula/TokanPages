import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { ArticleListingAction } from "../../../Store/Actions";
import { ArticleListView } from "./View/articleListView";

export const ArticleList = (): JSX.Element => 
{
    const dispatch = useDispatch();
    const article = useSelector((state: IApplicationState) => state.articleListing);
    
    React.useEffect(() => 
    { 
        dispatch(ArticleListingAction.get())
    }, 
    [ ]);

    return (<ArticleListView bind=
    {{
        isLoading: article.isLoading,
        articles: article.articles
    }}/>);
}
