import * as React from "react";
import { SliderView } from "./View/sliderView";
import { SliderProps } from "./Types";

export const Slider = (props: SliderProps) => {
    const [selection, setSelection] = React.useState(0);

    const onSlideChange = React.useCallback((_current: number, next: number) => {
        setSelection(next);
    }, []);

    return (
        <SliderView
            isLoading={props.isLoading}
            autoplay={props.autoplay}
            autoplaySpeed={props.autoplaySpeed}
            pauseOnHover={props.pauseOnHover}
            isFading={props.isFading}
            isInfinite={props.isInfinite}
            isNavigation={props.isNavigation}
            children={props.children}
            selection={selection}
            onSlideChange={onSlideChange}
            className={props.className}
        />
    );
};
