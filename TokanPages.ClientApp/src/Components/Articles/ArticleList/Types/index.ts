import { ReactChangeEvent, ReactKeyboardEvent } from "Shared/types";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ArticleItem } from "../../../../Shared/Components/RenderContent/Models";

export interface SearchInputProps { 
    searchInput: string 
}

export interface ArticlesProps {
    articles: ArticleItem[];
}

export interface ActionButtonProps {
    label: string;
    onClick: () => void;
}

export interface ButtonsProps {
    buttonSearch: ActionButtonProps;
    buttonClear: ActionButtonProps;
}

export interface TextProps {
    title: string;
    text: string[];
    placeholder: string;
}

export interface ArticleListProps {
    className?: string;
}

export interface ArticleListViewProps extends ViewProperties, ArticlesProps, ArticleListProps, TextProps, ButtonsProps {
    isMobile: boolean;
    onKeyUp?: (event: ReactKeyboardEvent) => void;
    onChange?: (event: ReactChangeEvent) => void;
    value: SearchInputProps;   
}

export interface RenderContentProps extends ArticlesProps {
}

export interface RenderStaticTextProps extends TextProps, ButtonsProps {
    isLoading: boolean;
    onKeyUp?: (event: ReactKeyboardEvent) => void;
    onChange?: (event: ReactChangeEvent) => void;
    value: SearchInputProps;   
}
