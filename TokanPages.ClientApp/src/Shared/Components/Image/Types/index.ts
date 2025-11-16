import { TLoading, TObjectFit } from "../../../../Shared/Types";

export interface RenderImageProps {
    source: string;
    base?: string;
    className?: string;
    classNameCursor?: string;
    width?: number;
    height?: number;
    objectFit?: TObjectFit;
    alt?: string;
    title?: string;
    loading?: TLoading;
    previewIcon?: string;
    isPreviewAlways?: boolean;
    isPreviewIcon?: boolean;
    isPreviewTopRadius?: boolean;
    isPreviewBottomRadius?: boolean;
    onClick?: () => void;
}
