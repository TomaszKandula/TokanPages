import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ArticleListingAction } from "../../../Store/Actions";
import { ArticleListView } from "./View/articleListView";

export interface ArticleListProps {
    background?: React.CSSProperties;
}

export const ArticleList = (props: ArticleListProps): JSX.Element => {
    const dispatch = useDispatch();
    const article = useSelector((state: ApplicationState) => state.articleListing);

    React.useEffect(() => {
        dispatch(ArticleListingAction.get());
    }, []);

    return <ArticleListView isLoading={article.isLoading} articles={article.articles} background={props.background} />;
};
