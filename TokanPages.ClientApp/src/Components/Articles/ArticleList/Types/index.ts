import { ArticleCategory } from "../../../../Api/Models";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ArticleItem } from "../../../../Shared/Components/RenderContent/Models";

export interface PageDataProps {
    totalSize: number;
    pageNumber: number;
    pageSize: number;
    onClick: () => void;
}

export interface SearchInputProps {
    searchInput: string;
}

export interface ArticlesProps {
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

export interface ArticleListProps {
    className?: string;
}

export interface ArticleListViewProps
    extends ViewProperties,
        ArticlesProps,
        ArticleListProps,
        TextProps,
        ButtonsProps,
        RenderSortProps {
    isMobile: boolean;
    isContentLoading: boolean;
    isOrderByAscending: boolean;
    categories: ArticleCategory[];
    pageData: PageDataProps;
    onKeyUp?: (event: ReactKeyboardEvent) => void;
    onChange?: (event: ReactChangeEvent) => void;
    value: SearchInputProps;
}

export interface RenderContentProps extends ArticlesProps {}

export interface RenderSortProps {
    onSortClick: () => void;
}

export interface RenderHeaderProps extends TextProps, ButtonsProps {
    isContentLoading: boolean;
    onKeyUp?: (event: ReactKeyboardEvent) => void;
    onChange?: (event: ReactChangeEvent) => void;
    value: SearchInputProps;
}

export interface RenderFilteringProps extends RenderHeaderProps, RenderSortProps {
    isOrderByAscending: boolean;
}
