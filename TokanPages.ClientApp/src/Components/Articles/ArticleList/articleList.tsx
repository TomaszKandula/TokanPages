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
        dispatch(ArticleListingAction.get(pagingInfo.pageNumber, ARTICLES_PAGE_SIZE, form.searchInput));
        setIsClearDisabled(false);
    }, [article.payload, form.searchInput]);

    const onClickClear = React.useCallback(() => {
        if (Validate.isEmpty(form.searchInput)) {
            return;
        }

        setForm({ searchInput: "" });
        dispatch(ArticleListingAction.get(1, ARTICLES_PAGE_SIZE));
    }, [form.searchInput]);

    React.useEffect(() => {
        if (article.isLoading) {
            return;
        }

        if (article.payload.results.length === 0) {
            dispatch(ArticleListingAction.get(1, ARTICLES_PAGE_SIZE));
        }
    }, [article.isLoading, article.payload.results]);

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
            value={form}
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
