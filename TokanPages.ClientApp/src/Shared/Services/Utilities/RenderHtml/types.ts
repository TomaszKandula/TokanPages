export type TagType = "p" | "span" | "div";

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
