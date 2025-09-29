import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ArticleCategory } from "../../../Api/Models";
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

const SELECT_ALL_ID = "b4ce2cce-28a0-486a-9c26-f6f8a559e229";

export const ArticleList = (props: ArticleListProps): React.ReactElement => {
    const media = useDimensions();
    const dispatch = useDispatch();
    const article = useSelector((state: ApplicationState) => state.articleListing);
    const content = useSelector((state: ApplicationState) => state.contentPageData.components.pageArticles);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isContentLoading = data.isLoading;

    const [form, setForm] = React.useState<SearchInputProps>({ searchInput: "" });
    const [categories, setCategories] = React.useState<ArticleCategory[] | undefined>(undefined);
    const [selection, setSelection] = React.useState<string>("");

    const [isSearchDisabled, setIsSearchDisabled] = React.useState(true);
    const [isClearDisabled, setIsClearDisabled] = React.useState(true);
    const [isOrderByAscending, setIsOrderByAscending] = React.useState(false);
    const [isSortingEnabled, setIsSortingEnabled] = React.useState(false);
    const [isCategoryChanged, setIsCategoryChanged] = React.useState(false);

    const onCategoryChangeClick = React.useCallback((id: string) => {
        if (id === SELECT_ALL_ID) {
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

    React.useEffect(() => {
        if (article.isLoading) {
            return;
        }

        if (!Validate.isEmpty(form.searchInput)) {
            return;
        }

        if (!Validate.isEmpty(selection)) {
            return;
        }

        if (article.payload.results.length === 0) {
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
    }, [article.isLoading, article.payload.results, form.searchInput, isOrderByAscending, selection]);

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

    React.useEffect(() => {
        if (isCategoryChanged) {
            setIsCategoryChanged(false);
            const pagingInfo = article.payload.pagingInfo;
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

    React.useEffect(() => {
        const hasNoCategories = !categories || categories.length === 0;
        const hasLabel = !Validate.isEmpty(content.labels.textSelectAll);
        const articleCategories = article.payload.articleCategories;

        if (hasLabel && hasNoCategories && articleCategories.length > 0) {
            let data: ArticleCategory[] = [];

            data = articleCategories.slice();
            data.unshift({
                id: SELECT_ALL_ID,
                categoryName: content.labels.textSelectAll,
            });

            setCategories(data);
        }
    }, [categories, article.payload.articleCategories, content.labels.textSelectAll]);

    React.useEffect(() => {
        if (Validate.isEmpty(form.searchInput)) {
            setIsSearchDisabled(true);
            setIsClearDisabled(true);
        } else {
            setIsSearchDisabled(false);
        }
    }, [form.searchInput]);

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
            onSortClick={onSortClick}
            pageData={{
                totalSize: article.payload.totalSize,
                pageNumber: article.payload.pagingInfo.pageNumber,
                pageSize: article.payload.pagingInfo.pageSize,
                onClick: onClickChangePage,
            }}
            selectedCategory={selection === "" ? SELECT_ALL_ID : selection}
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
