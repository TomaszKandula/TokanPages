import Slider from "react-slick";
import { ReactElement, ReactElements, ViewProperties } from "../../../../Shared/Types";

export interface ContentProps {
    children: ReactElement | ReactElements;
}

export interface SliderOptionsProps {
    onNextSlideClick?: () => void;
    onPrevSlideClick?: () => void;
    onPlayPauseClick?: () => void;
}

export interface SliderProps extends ViewProperties, ContentProps, SliderOptionsProps {
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
    isPaused: boolean;
    reference: React.MutableRefObject<Slider | null>;
    selection: number;
    onSlideChange: (current: number, next: number) => void;
}
