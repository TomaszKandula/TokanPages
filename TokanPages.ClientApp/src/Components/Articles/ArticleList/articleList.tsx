import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ArticleCategory } from "../../../Api/Models";
import { ApplicationState } from "../../../Store/Configuration";
import { ArticleListingAction } from "../../../Store/Actions";
import { useDimensions } from "../../../Shared/Hooks";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { HasSnapshotMode } from "../../../Shared/Services/SpaCaching";
import { ARTICLES_PAGE_SIZE, ARTICLES_SELECT_ALL_ID } from "../../../Shared/constants";
import { UpdatePageParam } from "../../../Shared/Services/Utilities";
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
    const hasSnapshot = HasSnapshotMode();
    const article = useSelector((state: ApplicationState) => state.articleListing);
    const content = useSelector((state: ApplicationState) => state.contentPageData.components.pageArticles);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isContentLoading = data.isLoading;

    const [form, setForm] = React.useState<SearchInputProps>({ searchInput: "" });
    const [categories, setCategories] = React.useState<ArticleCategory[] | undefined>(undefined);
    const [selection, setSelection] = React.useState<string>("");
    const [paginationNumber, setPaginationNumber] = React.useState(0);

    const [isSearchDisabled, setIsSearchDisabled] = React.useState(true);
    const [isClearDisabled, setIsClearDisabled] = React.useState(true);
    const [isOrderByAscending, setIsOrderByAscending] = React.useState(false);
    const [isSortingEnabled, setIsSortingEnabled] = React.useState(false);
    const [isCategoryChanged, setIsCategoryChanged] = React.useState(false);

    const category = selection === "" ? ARTICLES_SELECT_ALL_ID : selection;

    const onCategoryChangeClick = React.useCallback((id: string) => {
        if (id === ARTICLES_SELECT_ALL_ID) {
            setSelection("");
        } else {
            setSelection(id);
        }

        setIsCategoryChanged(true);
    }, []);

    const onSortClick = React.useCallback(() => {
        setIsOrderByAscending(!isOrderByAscending);
        setIsSortingEnabled(true);
    }, [isOrderByAscending]);

    const onClickChangePage = React.useCallback(
        (page: number) => {
            UpdatePageParam(page);
            dispatch(
                ArticleListingAction.get({
                    ...BaseRequest,
                    pageNumber: page,
                    pageSize: ARTICLES_PAGE_SIZE,
                    categoryId: selection,
                    orderByAscending: isOrderByAscending,
                })
            );
        },
        [isOrderByAscending, selection]
    );

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
        UpdatePageParam(pagingInfo.pageNumber);
        dispatch(
            ArticleListingAction.get({
                ...BaseRequest,
                pageNumber: pagingInfo.pageNumber,
                pageSize: ARTICLES_PAGE_SIZE,
                phrase: form.searchInput,
                categoryId: selection,
                orderByAscending: isOrderByAscending,
            })
        );
        setIsClearDisabled(false);
    }, [article.payload, form.searchInput, isOrderByAscending, selection]);

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
                categoryId: selection,
                orderByAscending: isOrderByAscending,
            })
        );
    }, [form.searchInput, isOrderByAscending, selection]);

    /* ON START: RETRIEVE ARTICLES */
    React.useEffect(() => {
        if (hasSnapshot) {
            return;
        }

        if (article.isLoading) {
            return;
        }

        if (!Validate.isEmpty(form.searchInput)) {
            return;
        }

        if (!Validate.isEmpty(selection)) {
            return;
        }

        if (props.page > 0 && article.payload.results.length === 0) {
            dispatch(
                ArticleListingAction.get({
                    ...BaseRequest,
                    pageNumber: props.page,
                    pageSize: ARTICLES_PAGE_SIZE,
                    categoryId: selection,
                    orderByAscending: isOrderByAscending,
                })
            );
        }
    }, [article.isLoading, article.payload.results, form.searchInput, props.page, isOrderByAscending, selection]);

    /* SORTING  AZ | ZA */
    React.useEffect(() => {
        const hasPayload = article.payload.results.length !== 0;
        if (isSortingEnabled && hasPayload) {
            setIsSortingEnabled(false);
            dispatch(
                ArticleListingAction.get({
                    ...BaseRequest,
                    pageNumber: 1,
                    pageSize: ARTICLES_PAGE_SIZE,
                    categoryId: selection,
                    orderByAscending: isOrderByAscending,
                })
            );
        }
    }, [isSortingEnabled, isOrderByAscending, article.payload.results.length, selection]);

    /* FILTERING BY CATEGORY NAME */
    React.useEffect(() => {
        if (isCategoryChanged) {
            setIsCategoryChanged(false);
            const pagingInfo = article.payload.pagingInfo;
            UpdatePageParam(pagingInfo.pageNumber);
            dispatch(
                ArticleListingAction.get({
                    ...BaseRequest,
                    pageNumber: pagingInfo.pageNumber,
                    pageSize: ARTICLES_PAGE_SIZE,
                    phrase: form.searchInput,
                    categoryId: selection,
                    orderByAscending: isOrderByAscending,
                })
            );
        }
    }, [isCategoryChanged, isOrderByAscending, selection, form.searchInput]);

    /* ADD 'SELECT ALL' TO CATEGORY LIST */
    React.useEffect(() => {
        const hasNoCategories = !categories || categories.length === 0;
        const hasLabel = !Validate.isEmpty(content.labels.textSelectAll);
        const articleCategories = article.payload.articleCategories;

        if (hasLabel && hasNoCategories && articleCategories.length > 0) {
            let data: ArticleCategory[] = [];

            data = articleCategories.slice();
            data.unshift({
                id: ARTICLES_SELECT_ALL_ID,
                categoryName: content.labels.textSelectAll,
            });

            setCategories(data);
        }
    }, [categories, article.payload.articleCategories, content.labels.textSelectAll]);

    /* SORTING MECHANISM */
    React.useEffect(() => {
        if (Validate.isEmpty(form.searchInput)) {
            setIsSearchDisabled(true);
            setIsClearDisabled(true);
        } else {
            setIsSearchDisabled(false);
        }
    }, [form.searchInput]);

    /* CALCULATE NUMBER OF PAGES */
    React.useEffect(() => {
        const pageOffset = 1;
        const totalSize = article.payload.totalSize;
        const pageSize = article.payload.pagingInfo.pageSize;
        const calc = totalSize / pageSize;

        let pages = Math.floor(calc);
        if (Number.isInteger(calc)) {
            setPaginationNumber(pages);
        } else {
            pages += pageOffset;
            setPaginationNumber(pages);
        }
    }, [article.payload.totalSize, article.payload.pagingInfo.pageSize]);

    /* CLEAR ON UNMOUNT */
    React.useEffect(() => {
        return () => {
            dispatch(ArticleListingAction.clear());
        };
    }, []);

    return (
        <ArticleListView
            isLoading={article.isLoading}
            isContentLoading={isContentLoading}
            isMobile={media.isMobile}
            isOrderByAscending={isOrderByAscending}
            hasSnapshotMode={hasSnapshot}
            onSortClick={onSortClick}
            pageData={{
                totalSize: article.payload.totalSize,
                pageNumber: article.payload.pagingInfo.pageNumber,
                pageSize: article.payload.pagingInfo.pageSize,
                paginationNumber: paginationNumber,
                onClick: onClickChangePage,
            }}
            selectedCategory={category}
            categories={categories ?? []}
            articles={article.payload.results}
            className={props.className}
            title={content?.caption}
            placeholder={content?.labels?.placeholder}
            text={content?.content}
            onKeyUp={onKeyHandler}
            onChange={onInputHandler}
            onCategoryChange={onCategoryChangeClick}
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
