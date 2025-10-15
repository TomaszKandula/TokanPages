export type Mode = "Text" | "Rect" | "Circle";

export interface SkeletonProps {
    isLoading: boolean;
    mode?: Mode;
    width?: number;
    height?: number;
    hasSkeletonCentered?: boolean;
    disableMarginY?: boolean;
    className?: string;
    children: React.ReactNode | React.ReactElement | React.ReactElement[];
}
