import React from "react";
import { API_BASE_URI } from "../../../../../Api";
import { useMediaPresenter } from "../../../../../Shared/Hooks";
import { ReactElement } from "../../../../../Shared/Types";
import { Carousel, MediaPresenter, Image } from "../../../../../Shared/Components";
import { VideoItemProps } from "../../Models/TextModel";
import { TextItem } from "../../Models";
import { v4 as uuidv4 } from "uuid";

interface RenderPosterProps extends TextItem {
    item: VideoItemProps;
    onClick: () => void;
}

const RenderPoster = (props: RenderPosterProps): ReactElement => {
    const value = props.item;

    return (
        <>
            <div className="bulma-card-image">
                <figure className="bulma-image is-clickable" onClick={props.onClick}>
                    <Image
                        isPreviewAlways
                        isPreviewIcon
                        isPreviewTopRadius
                        previewIcon="PlayCircleOutline"
                        source={`${API_BASE_URI}${value.poster}`}
                        width={value.constraint?.width ?? props.constraint?.width}
                        height={value.constraint?.height ?? props.constraint?.height}
                        objectFit={value.constraint?.objectFit ?? props.constraint?.objectFit}
                    />
                </figure>
            </div>
            <div className="bulma-card-content">
                <p className="is-size-6 has-text-black">{value.text}</p>
            </div>
        </>
    );
};

export const RenderVideos = (props: TextItem): ReactElement => {
    const presenter = useMediaPresenter();
    const items = props.value as VideoItemProps[];

    return (
        <>
            <Carousel
                isLoading={false}
                isLazyLoad={true}
                isFading={false}
                isInfinite={true}
                isSwipeToSlide={false}
                isNavigation={true}
                autoplay={true}
                autoplaySpeed={5500}
                pauseOnHover={false}
                className="bulma-card"
            >
                {items.map((value: VideoItemProps, index: number) => (
                    <RenderPoster
                        key={uuidv4()}
                        {...props}
                        item={value}
                        onClick={() => {
                            presenter.onSelectionClick(index);
                        }}
                    />
                ))}
            </Carousel>
            <MediaPresenter
                isOpen={presenter.isPresenterOpen}
                presenting={presenter.selection}
                collection={items.map(items => items.video)}
                posters={items.map(items => items.poster)}
                type="video"
                onTrigger={presenter.onPresenterClick}
            />
        </>
    );
}
