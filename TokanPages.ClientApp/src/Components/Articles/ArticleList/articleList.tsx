import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ArticleListingAction } from "../../../Store/Actions";
import { useDimensions } from "../../../Shared/Hooks";
import { ArticleListView } from "./View/articleListView";

export interface ArticleListProps {
    className?: string;
}

export const ArticleList = (props: ArticleListProps): React.ReactElement => {
    const media = useDimensions();
    const dispatch = useDispatch();
    const article = useSelector((state: ApplicationState) => state.articleListing);
    const content = useSelector((state: ApplicationState) => state.contentPageData.components.pageArticles);

    React.useEffect(() => {
        if (article.isLoading) {
            return;
        }

        if (article.articles.length === 0) {
            dispatch(ArticleListingAction.get());
        }
    }, [article.isLoading, article.articles]);

    return (
        <ArticleListView
            isLoading={article.isLoading}
            isMobile={media.isMobile}
            articles={article.articles}
            className={props.className}
            title={content?.caption}
            placeholder={content?.labels?.placeholder}
            buttonSearch={content?.labels?.buttonSearch}
            buttonClear={content?.labels?.buttonClear}
            content={content?.content}
        />
    );
};
