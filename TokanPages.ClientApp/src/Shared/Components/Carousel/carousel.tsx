import * as React from "react";
import Slider from "react-slick";
import { CarouselView } from "./View/carouselView";
import { CarouselProps } from "./Types";

export const Carousel = (props: CarouselProps) => {
    const ref = React.useRef<Slider | null>(null);

    const [selection, setSelection] = React.useState(0);
    const [isPaused, setIsPaused] = React.useState(false);

    const onSlideChange = React.useCallback((_current: number, next: number) => {
        setSelection(next);
    }, []);

    const onNextSlideClick = React.useCallback(() => {
        if (ref !== null) {
            ref.current?.slickNext();
        }
    }, [ref]);

    const onPrevSlideClick = React.useCallback(() => {
        if (ref !== null) {
            ref.current?.slickPrev();
        }
    }, [ref]);

    const onPlayPauseClick = React.useCallback(() => {
        setIsPaused(!isPaused);
    }, [isPaused]);

    React.useEffect(() => {
        if (isPaused) {
            ref.current?.slickPause();
        } else {
            ref.current?.slickPlay();
        }
    }, [isPaused, ref]);

    return (
        <CarouselView
            reference={ref}
            isLoading={props.isLoading}
            autoplay={props.autoplay}
            autoplaySpeed={props.autoplaySpeed}
            pauseOnHover={props.pauseOnHover}
            isPaused={isPaused}
            isFading={props.isFading}
            isInfinite={props.isInfinite}
            isNavigation={props.isNavigation}
            children={props.children}
            selection={selection}
            onSlideChange={onSlideChange}
            className={props.className}
            onNextSlideClick={onNextSlideClick}
            onPrevSlideClick={onPrevSlideClick}
            onPlayPauseClick={onPlayPauseClick}
        />
    );
};
