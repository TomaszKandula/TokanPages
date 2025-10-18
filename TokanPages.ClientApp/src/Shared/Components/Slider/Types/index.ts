import { ReactElements, ViewProperties } from "../../../../Shared/Types";

export interface ContentProps {
    children: ReactElements;
}

export interface SliderProps extends ViewProperties, ContentProps {
    autoplay?: boolean;
    autoplaySpeed?: number;
    pauseOnHover?: boolean;
    isFading?: boolean;
    isInfinite?: boolean;
    isLazyLoad?: boolean;
    isSwipeToSlide?: boolean;
    isNavigation?: boolean;
    className?: string;
}

export interface SliderViewProps extends SliderProps {
    selection: number;
    onSlideChange: (current: number, next: number) => void;
}
