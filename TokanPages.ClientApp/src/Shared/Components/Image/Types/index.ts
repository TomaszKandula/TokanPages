import { TLoading, TObjectFit } from "../../../../Shared/Types";

export interface RenderImageProps {
    source: string;
    base?: string;
    className?: string;
    width?: number;
    height?: number;
    objectFit?: TObjectFit;
    alt?: string;
    title?: string;
    loading?: TLoading;
    isPreviewIcon?: boolean;
    onClick?: () => void;
}
