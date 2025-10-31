import Slider from "react-slick";
import { ReactElement, ReactElements, ViewProperties } from "../../../Types";

export interface ContentProps {
    children: ReactElement | ReactElements;
}

export interface CarouselOptionsProps {
    onNextSlideClick?: () => void;
    onPrevSlideClick?: () => void;
    onPlayPauseClick?: () => void;
}

export interface CarouselProps extends ViewProperties, ContentProps, CarouselOptionsProps {
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

export interface CarouselViewProps extends CarouselProps {
    isPaused: boolean;
    reference: React.MutableRefObject<Slider | null>;
    selection: number;
    onSlideChange: (current: number, next: number) => void;
}
