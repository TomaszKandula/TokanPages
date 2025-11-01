import React from "react";
import { API_BASE_URI } from "../../../Api";
import { ReactElement } from "../../../Shared/Types";
import { useDimensions } from "../../../Shared/Hooks";
import { Image } from "../../../Shared/Components";
import { ToggleBodyScroll } from "../../../Shared/Services/Utilities";
import { Icon } from "../Icon";
import { StandardBackdrop } from "../Backdrop";
import { MediaPresenterProps } from "./Types";
import "./mediaPresenter.css";

export const MediaPresenter = (props: MediaPresenterProps): ReactElement => {
    const media = useDimensions();
    const headerRef = React.useRef<HTMLDivElement | null>(null);

    const [imageNumber, setImageNumber] = React.useState(props.presenting);
    const [imageHeight, setImageHeight] = React.useState(0);
    const [canOpenMenu, setCanOpenMenu] = React.useState(false);
    const [canCloseMenu, setCanCloseMenu] = React.useState(false);
    const [canShowBackdrop, setCanShowBackdrop] = React.useState(false);

    const onCloseHandler = React.useCallback(() => {
        setCanCloseMenu(true);
    }, []);

    const onNextImage = React.useCallback(() => {
        const length = props.collection.length;
        if (length === 1) {
            return;
        }

        const nextImage = imageNumber + 1;
        if (nextImage < length) {
            setImageNumber(nextImage);
        }
    }, [props.collection.length, imageNumber]);

    const onPrevImage = React.useCallback(() => {
        const length = props.collection.length;
        if (length === 1) {
            return;
        }

        const prevImage = imageNumber - 1;
        if (prevImage > -1) {
            setImageNumber(prevImage);
        }
    }, [props.collection.length, imageNumber]);

    /* SET IMAGE HEIGHT */
    React.useEffect(() => {
        const clientHeight = headerRef.current?.getBoundingClientRect().height;
        if (clientHeight && clientHeight !== 0) {
            setImageHeight(media.height - clientHeight * 4);
        }
    }, [headerRef.current?.clientHeight]);

    /* ON OPEN EVENT */
    React.useEffect(() => {
        if (props.isOpen && !canCloseMenu) {
            ToggleBodyScroll(false);
            setTimeout(() => setCanShowBackdrop(true), 150);
            setTimeout(() => setCanOpenMenu(true), 250);
        }
    }, [props.isOpen, canCloseMenu]);

    /* ON CLOSE EVENT */
    React.useEffect(() => {
        if (props.isOpen && canCloseMenu) {
            ToggleBodyScroll(true);
            setTimeout(() => setCanShowBackdrop(false), 250);
            setTimeout(() => setCanOpenMenu(false), 150);
            setTimeout(() => {
                props.onTrigger();
                setCanCloseMenu(false);
            }, 430);
        }
    }, [props.isOpen, canCloseMenu]);

    /* ON UNMOUNT EVENT */
    React.useEffect(() => {
        return () => {
            ToggleBodyScroll(true);
        };
    }, []);

    if (!props.isOpen) {
        return <></>;
    }

    return (
        <div role="presentation" className="media-presenter-root" style={{ opacity: canOpenMenu ? 1 : 0 }}>
            <StandardBackdrop style={{ opacity: canShowBackdrop ? 1 : 0 }} onClick={onCloseHandler} />
            <div ref={headerRef} className="m-4 is-flex is-justify-content-flex-end">
                <Icon name="WindowClose" size={2.0} className="has-text-white is-clickable" onClick={onCloseHandler} />
            </div>
            <div className="is-flex is-justify-content-space-between is-align-items-center">
                <Icon
                    name="ChevronLeft"
                    size={2.0}
                    className="mx-4 has-text-white is-clickable"
                    onClick={onPrevImage}
                />
                <figure className="bulma-image">
                    <Image
                        source={`${API_BASE_URI}${props.collection[imageNumber]}`}
                        height={imageHeight}
                        objectFit="scale-down"
                        loading="eager"
                    />
                </figure>
                <Icon
                    name="ChevronRight"
                    size={2.0}
                    className="mx-4 has-text-white is-clickable"
                    onClick={onNextImage}
                />
            </div>
        </div>
    );
};
