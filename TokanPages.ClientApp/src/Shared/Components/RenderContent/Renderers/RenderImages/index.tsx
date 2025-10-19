import React from "react";
import { API_BASE_URI } from "../../../../../Api";
import { ReactElement } from "../../../../../Shared/Types";
import { Slider, Image } from "../../../../../Shared/Components";
import { ImageItemProps, TextItem } from "../../Models";
import { v4 as uuidv4 } from "uuid";

export const RenderImages = (props: TextItem): ReactElement => {
    const items = props.value as ImageItemProps[];

    return (
        <Slider
            isLoading={false}
            isLazyLoad={true}
            isFading={false}
            isInfinite={true}
            isSwipeToSlide={true}
            isNavigation={true}
            autoplay={true}
            autoplaySpeed={5500}
            pauseOnHover={true}
            className="is-flex is-flex-direction-column"
        >
            {items.map((value: ImageItemProps, _index: number) => (
                <React.Fragment key={uuidv4()}>
                    <div className="bulma-card-image">
                        <figure className="bulma-image">
                            <Image
                                source={`${API_BASE_URI}${value.image}`}
                                title={props.text}
                                alt={props.text}
                                width={value.constraint?.width ?? props.constraint?.width}
                                height={value.constraint?.height ?? props.constraint?.height}
                                objectFit={value.constraint?.objectFit ?? props.constraint?.objectFit}
                                loading={props.loading}
                            />
                        </figure>
                    </div>
                    <div className="bulma-card-content">
                        <p className="is-size-6 has-text-black">{value.caption}</p>
                    </div>
                </React.Fragment>
            ))}
        </Slider>
    );
};
