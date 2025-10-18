import * as React from "react";
import Slider from "react-slick";
import { SliderViewProps } from "../Types";
import { ReactElement } from "../../../../Shared/Types";
import { Icon } from "../../../../Shared/Components";
import { v4 as uuidv4 } from "uuid";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

const Navigation = (props: SliderViewProps): ReactElement => (
    <div className="is-flex is-justify-content-center is-gap-1.5 p-5">
        {Array.isArray(props.children) &&
            props.children.map((_value: ReactElement, index: number) => (
                <Icon
                    key={uuidv4()}
                    name="Circle"
                    size={0.8}
                    className={props.selection === index ? "has-text-grey-dark" : "has-text-grey-light"}
                />
            ))}
    </div>
);

export const SliderView = (props: SliderViewProps): ReactElement => (
    <div className={`bulma-card ${props.className ?? ""}`}>
        <Slider
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
