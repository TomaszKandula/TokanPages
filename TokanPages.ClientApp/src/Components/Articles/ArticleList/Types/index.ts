import { ArticleCategory } from "../../../../Api/Models";
import { ReactChangeEvent, ReactKeyboardEvent, ViewProperties } from "../../../../Shared/Types";
import { ArticleItem } from "../../../../Shared/Components/RenderContent/Models";

export interface PageDataProps {
    totalSize: number;
    pageNumber: number;
    pageSize: number;
    paginationNumber: number;
    onClick: (page: number) => void;
}

export interface SearchInputProps {
    value: string;
}

export interface SearchFormProps {
    searchInputForm: SearchInputProps;
}

export interface ArticlesProps {
    isSnapshot: boolean;
    articles: ArticleItem[];
    searchEmptyText1: string;
    searchEmptyText2: string;
}

export interface SearchButtonProps extends ActionButtonProps {
    isSearchDisabled: boolean;
}

export interface ClearButtonProps extends ActionButtonProps {
    isClearDisabled: boolean;
}

export interface ActionButtonProps {
    label: string;
    onClick: () => void;
}

export interface ButtonsProps {
    buttonSearch: SearchButtonProps;
    buttonClear: ClearButtonProps;
}

export interface TextProps {
    title: string;
    text: string[];
    placeholder: string;
}

export interface ArticleListProps extends ArticleStyleProps {
    page: number;
}

export interface ArticleStyleProps {
    className?: string;
}

export interface ArticleListViewProps
    extends ViewProperties,
        ArticlesProps,
        ArticleStyleProps,
        TextProps,
        ButtonsProps,
        RenderSortProps,
        SearchFormProps {
    isMobile: boolean | null;
    isContentLoading: boolean;
    isOrderByAscending: boolean;
    selectedCategory: string;
    categories: ArticleCategory[];
    pageData: PageDataProps;
    onKeyUp?: (event: ReactKeyboardEvent) => void;
    onChange?: (event: ReactChangeEvent) => void;
    onCategoryChange: (id: string) => void;
}

export interface RenderContentProps extends ArticlesProps {}

export interface RenderSortProps {
    isDisabled?: boolean;
    onSortClick: () => void;
}

export interface RenderHeaderProps extends TextProps, ButtonsProps, SearchFormProps {
    isContentLoading: boolean;
    onKeyUp?: (event: ReactKeyboardEvent) => void;
    onChange?: (event: ReactChangeEvent) => void;
}

export interface RenderFilteringProps extends RenderHeaderProps, RenderSortProps {
    isOrderByAscending: boolean;
}
