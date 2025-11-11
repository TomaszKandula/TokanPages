import { TObjectFit, TVideoPreload } from "Shared/Types";

export interface VideoProps {
    source: string;
    poster: string;
    controls: boolean;
    preload: TVideoPreload;
    base?: string;
    className?: string;
    width?: number | string;
    height?: number | string;
    objectFit?: TObjectFit;
    isPreviewIcon?: boolean;
    isPreviewTopRadius?: boolean;
    isPreviewBottomRadius?: boolean;
    onClick?: () => void;
}
