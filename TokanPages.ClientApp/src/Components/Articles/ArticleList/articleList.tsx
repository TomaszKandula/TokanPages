import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ArticleListingAction } from "../../../Store/Actions";
import { useDimensions } from "../../../Shared/Hooks";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { ARTICLES_PAGE_SIZE } from "../../../Shared/constants";
import { ArticleListProps, SearchInputProps } from "./Types";
import { ArticleListView } from "./View/articleListView";

export const ArticleList = (props: ArticleListProps): React.ReactElement => {
    const media = useDimensions();
    const dispatch = useDispatch();
    const article = useSelector((state: ApplicationState) => state.articleListing);
    const content = useSelector((state: ApplicationState) => state.contentPageData.components.pageArticles);

    const [searchInput, setSearchInput] = React.useState<SearchInputProps>({ searchInput: "" });

    const onClickChangePage = React.useCallback(() => {
        const totalSize = article.payload.totalSize;
        const pagingInfo = article.payload.pagingInfo;

        const nextPage = pagingInfo.pageNumber + 1;
        const prevPage = pagingInfo.pageNumber - 1;
        const pages = Math.round(totalSize / pagingInfo.pageSize);

        if (nextPage <= pages) {
            dispatch(ArticleListingAction.get(nextPage, ARTICLES_PAGE_SIZE));
        } else if (prevPage > 0) {
            dispatch(ArticleListingAction.get(prevPage, ARTICLES_PAGE_SIZE));
        }
    }, [article.payload]);

    const onKeyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                onClickSearch();
            }
        },
        [searchInput]
    );

    const onInputHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setSearchInput({ ...searchInput, [event.currentTarget.name]: event.currentTarget.value });
        },
        [searchInput]
    );

    const onClickSearch = React.useCallback(() => {
        console.log("search");
    }, []);

    const onClickClear = React.useCallback(() => {
        console.log("clear");       
    }, []);

    React.useEffect(() => {
        if (article.isLoading) {
            return;
        }

        if (article.payload.results.length === 0) {
            dispatch(ArticleListingAction.get(1, ARTICLES_PAGE_SIZE));
        }
    }, [article.isLoading, article.payload.results]);

    return (
        <ArticleListView
            isLoading={article.isLoading}
            isMobile={media.isMobile}
            pageData={{
                totalSize: article.payload.totalSize,
                pageNumber: article.payload.pagingInfo.pageNumber,
                pageSize: article.payload.pagingInfo.pageSize,
                onClick: onClickChangePage,
            }}
            articles={article.payload.results}
            className={props.className}
            title={content?.caption}
            placeholder={content?.labels?.placeholder}
            text={content?.content}
            onKeyUp={onKeyHandler}
            onChange={onInputHandler}
            value={searchInput}
            buttonSearch={{
                label: content?.labels?.buttonSearch,
                onClick: onClickSearch,
            }}
            buttonClear={{
                label: content?.labels?.buttonClear,
                onClick: onClickClear,
            }}
        />
    );
};
