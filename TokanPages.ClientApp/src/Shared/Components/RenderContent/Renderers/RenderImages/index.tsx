import React from "react";
import { API_BASE_URI } from "../../../../../Api";
import { ReactElement } from "../../../../../Shared/Types";
import { Carousel, Image, MediaPresenter } from "../../../../../Shared/Components";
import { ImageItemProps, TextItem } from "../../Models";
import { v4 as uuidv4 } from "uuid";

interface RenderImageProps extends TextItem {
    imageItem: ImageItemProps;
    onClick: () => void;
}

const RenderImage = (props: RenderImageProps) => {
    const value = props.imageItem;

    return (
        <>
            <div className="bulma-card-image">
                <figure className="bulma-image is-clickable" onClick={props.onClick}>
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
        </>
    );
};

export const RenderImages = (props: TextItem): ReactElement => {
    const items = props.value as ImageItemProps[];

    const [isImageOpen, setIsImageOpen] = React.useState(false);
    const [selection, setSelection] = React.useState(0);

    const onSelection = React.useCallback((selection: number) => {
        setSelection(selection);
        onImageOpenClick();
    }, []);

    const onImageOpenClick = React.useCallback(() => {
        setIsImageOpen(!isImageOpen);
    }, [isImageOpen]);

    return (
        <>
            <Carousel
                isLoading={false}
                isLazyLoad={true}
                isFading={false}
                isInfinite={true}
                isSwipeToSlide={true}
                isNavigation={true}
                autoplay={true}
                autoplaySpeed={5500}
                pauseOnHover={false}
                className="bulma-card"
            >
                {items.map((value: ImageItemProps, index: number) => (
                    <RenderImage key={uuidv4()} {...props} imageItem={value} onClick={() => { onSelection(index) }} />
                ))}
            </Carousel>
            <MediaPresenter
                isOpen={isImageOpen}
                presenting={selection}
                collection={items.map(items => items.image)}
                type="image"
                onTrigger={onImageOpenClick}
            />
        </>
    );
};
