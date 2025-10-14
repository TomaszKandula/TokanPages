export interface ReplaceProps {
    key: string;
    object: React.ReactElement;
}

export interface RenderParagraphsProps {
    text: string[];
    className?: string;
    replace?: ReplaceProps;
    isLoading?: boolean;
}
