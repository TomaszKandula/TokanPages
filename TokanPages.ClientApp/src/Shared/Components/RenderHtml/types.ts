export type TagType = "p" | "span" | "div" | "h1" | "h2" | "h3" | "h4" | "h5" | "h6" | "blockquote" | "li";

export interface ProcessTextProps {
    value: string;
    tag: TagType;
    testId?: string;
    className?: string;
    style?: React.CSSProperties;
}

export interface RenderOutputProps extends ProcessTextProps {
    text: string;
}
