import { TObjectFit } from "Shared/Types";

export interface VideoProps {
    source: string;
    poster: string;
    controls: boolean;
    preload: string;
    base?: string;
    className?: string;
    width?: number;
    height?: number;
    objectFit?: TObjectFit;
    isPreviewIcon?: boolean;
    isPreviewTopRadius?: boolean;
    isPreviewBottomRadius?: boolean;
    onClick?: () => void;
}
