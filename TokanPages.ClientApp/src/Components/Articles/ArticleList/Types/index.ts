import { ViewProperties } from "../../../../Shared/Abstractions";
import { ArticleItem } from "../../../../Shared/Components/RenderContent/Models";

export interface ArticleListProps {
    className?: string;
}

export interface ArticleListViewProps extends ViewProperties, ArticleListProps {
    isMobile: boolean;
    articles: ArticleItem[];
    title: string;
    placeholder: string;
    buttonSearch: string;
    buttonClear: string;
    content: string[];
}

export interface RenderContentProps {
    articles: ArticleItem[];
}

export interface RenderStaticTextProps {
    isLoading: boolean;
    title: string;
    text: string[];
    placeholder: string;
    buttonSearch: string;
    buttonClear: string;
}
