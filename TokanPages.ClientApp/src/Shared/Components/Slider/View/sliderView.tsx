import * as React from "react";
import Slider from "react-slick";
import { SliderViewProps } from "../Types";
import { ReactElement } from "../../../../Shared/Types";
import { Icon, IconButton } from "../../../../Shared/Components";
import { v4 as uuidv4 } from "uuid";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

const Navigation = (props: SliderViewProps): ReactElement => (
    <div className="is-flex is-justify-content-center is-gap-1.5 mb-5">
        <IconButton onClick={props.onPlayPauseClick}>
            {props.isPaused ? (
                <Icon name="PlayCircleOutline" size={1.8} className="has-text-link" />
            ) : (
                <Icon name="PauseCircleOutline" size={1.8} className="has-text-link" />
            )}
        </IconButton>
        <IconButton onClick={props.onPrevSlideClick}>
            <Icon name="ChevronLeft" size={1.8} className="has-text-link" />
        </IconButton>
        <div className="is-flex is-justify-content-center is-align-items-center">
            {Array.isArray(props.children) &&
                props.children.map((_value: ReactElement, index: number) => (
                    <Icon
                        key={uuidv4()}
                        name="Square"
                        size={0.5}
                        className={`mx-1 ${props.selection === index ? "has-text-link" : "has-text-link-light"}`}
                    />
                ))}
        </div>
        <IconButton onClick={props.onNextSlideClick}>
            <Icon name="ChevronRight" size={1.8} className="has-text-link" />
        </IconButton>
    </div>
);

export const CarouselView = (props: SliderViewProps): ReactElement => (
    <div className={`is-flex is-flex-direction-column is-gap-1.5 ${props.className ?? ""}`}>
        <Slider
            ref={props.reference}
            dots={false}
            arrows={false}
            slidesToShow={1}
            slidesToScroll={1}
            waitForAnimate={false}
            fade={props.isFading}
            infinite={props.isInfinite}
            lazyLoad={props.isLazyLoad ? "ondemand" : "anticipated"}
            swipeToSlide={props.isSwipeToSlide}
            autoplay={props.autoplay}
            autoplaySpeed={props.autoplaySpeed}
            pauseOnHover={props.pauseOnHover}
            beforeChange={props.onSlideChange}
        >
            {props.children}
        </Slider>
        {!props.isLoading && props.isNavigation && <Navigation {...props} />}
    </div>
);
