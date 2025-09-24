import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ArticleListingAction } from "../../../Store/Actions";
import { useDimensions } from "../../../Shared/Hooks";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { ARTICLES_PAGE_SIZE } from "../../../Shared/constants";
import { ArticleListProps, SearchInputProps } from "./Types";
import { ArticleListView } from "./View/articleListView";
import Validate from "validate.js";

const BaseRequest = {
    orderByColumn: "createdAt",
    isPublished: true,
    noCache: false,
};

export const ArticleList = (props: ArticleListProps): React.ReactElement => {
    const media = useDimensions();
    const dispatch = useDispatch();
    const article = useSelector((state: ApplicationState) => state.articleListing);
    const content = useSelector((state: ApplicationState) => state.contentPageData.components.pageArticles);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isContentLoading = data.isLoading;

    const [form, setForm] = React.useState<SearchInputProps>({ searchInput: "" });
    const [isSearchDisabled, setIsSearchDisabled] = React.useState(true);
    const [isClearDisabled, setIsClearDisabled] = React.useState(true);
    const [isOrderByAscending, setIsOrderByAscending] = React.useState(false);
    const [isSortingEnabled, setIsSortingEnabled] = React.useState(false);

    const onSortClick = React.useCallback(() => {
        setIsOrderByAscending(!isOrderByAscending);
        setIsSortingEnabled(true);
    }, [isOrderByAscending]);

    const onClickChangePage = React.useCallback(() => {
        const totalSize = article.payload.totalSize;
        const pagingInfo = article.payload.pagingInfo;

        const nextPage = pagingInfo.pageNumber + 1;
        const prevPage = pagingInfo.pageNumber - 1;
        const pages = Math.round(totalSize / pagingInfo.pageSize);

        if (nextPage <= pages) {
            dispatch(
                ArticleListingAction.get({
                    ...BaseRequest,
                    pageNumber: nextPage,
                    pageSize: ARTICLES_PAGE_SIZE,
                    //category: "",
                    orderByAscending: isOrderByAscending,
                })
            );
        } else if (prevPage > 0) {
            dispatch(
                ArticleListingAction.get({
                    ...BaseRequest,
                    pageNumber: prevPage,
                    pageSize: ARTICLES_PAGE_SIZE,
                    //category: "",
                    orderByAscending: isOrderByAscending,
                })
            );
        }
    }, [article.payload, isOrderByAscending]);

    const onKeyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                onClickSearch();
            }
        },
        [form]
    );

    const onInputHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const onClickSearch = React.useCallback(() => {
        const pagingInfo = article.payload.pagingInfo;
        dispatch(
            ArticleListingAction.get({
                ...BaseRequest,
                pageNumber: pagingInfo.pageNumber,
                pageSize: ARTICLES_PAGE_SIZE,
                phrase: form.searchInput,
                //category: "",
                orderByAscending: isOrderByAscending,
            })
        );
        setIsClearDisabled(false);
    }, [article.payload, form.searchInput, isOrderByAscending]);

    const onClickClear = React.useCallback(() => {
        if (Validate.isEmpty(form.searchInput)) {
            return;
        }

        setForm({ searchInput: "" });
        dispatch(
            ArticleListingAction.get({
                ...BaseRequest,
                pageNumber: 1,
                pageSize: ARTICLES_PAGE_SIZE,
                //category: "",
                orderByAscending: isOrderByAscending,
            })
        );
    }, [form.searchInput, isOrderByAscending]);

    React.useEffect(() => {
        if (article.isLoading) {
            return;
        }

        if (!Validate.isEmpty(form.searchInput)) {
            return;
        }

        if (article.payload.results.length === 0) {
            dispatch(
                ArticleListingAction.get({
                    ...BaseRequest,
                    pageNumber: 1,
                    pageSize: ARTICLES_PAGE_SIZE,
                    //category: "",
                    orderByAscending: isOrderByAscending,
                })
            );
        }
    }, [article.isLoading, article.payload.results, form.searchInput, isOrderByAscending]);

    React.useEffect(() => {
        const hasPayload = article.payload.results.length !== 0;
        if (isSortingEnabled && hasPayload) {
            setIsSortingEnabled(false);
            dispatch(
                ArticleListingAction.get({
                    ...BaseRequest,
                    pageNumber: 1,
                    pageSize: ARTICLES_PAGE_SIZE,
                    //category: "",
                    orderByAscending: isOrderByAscending,
                })
            );
        }
    }, [isSortingEnabled, isOrderByAscending, article.payload.results.length]);

    React.useEffect(() => {
        if (Validate.isEmpty(form.searchInput)) {
            setIsSearchDisabled(true);
            setIsClearDisabled(true);
        } else {
            setIsSearchDisabled(false);
        }
    }, [form.searchInput]);

    return (
        <ArticleListView
            isLoading={article.isLoading}
            isContentLoading={isContentLoading}
            isMobile={media.isMobile}
            isOrderByAscending={isOrderByAscending}
            onSortClick={onSortClick}
            pageData={{
                totalSize: article.payload.totalSize,
                pageNumber: article.payload.pagingInfo.pageNumber,
                pageSize: article.payload.pagingInfo.pageSize,
                onClick: onClickChangePage,
            }}
            selection={content?.selection}
            categories={article.payload.articleCategories}
            articles={article.payload.results}
            className={props.className}
            title={content?.caption}
            placeholder={content?.labels?.placeholder}
            text={content?.content}
            onKeyUp={onKeyHandler}
            onChange={onInputHandler}
            value={form}
            searchEmptyText1={content?.labels?.textEmptySearch1}
            searchEmptyText2={content?.labels?.textEmptySearch2}
            buttonSearch={{
                isSearchDisabled: isSearchDisabled,
                label: content?.labels?.buttonSearch,
                onClick: onClickSearch,
            }}
            buttonClear={{
                isClearDisabled: isClearDisabled,
                label: content?.labels?.buttonClear,
                onClick: onClickClear,
            }}
        />
    );
};
