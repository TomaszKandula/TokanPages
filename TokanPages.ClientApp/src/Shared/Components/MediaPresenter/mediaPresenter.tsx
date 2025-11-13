import React from "react";
import { API_BASE_URI } from "../../../Api";
import { ReactElement } from "../../../Shared/Types";
import { useDimensions } from "../../../Shared/Hooks";
import { IconButtonSolid, Image, Video } from "../../../Shared/Components";
import { ToggleBodyScroll } from "../../../Shared/Services/Utilities";
import { StandardBackdrop } from "../Backdrop";
import { MediaPresenterProps } from "./Types";
import "./mediaPresenter.css";

export const MediaPresenter = (props: MediaPresenterProps): ReactElement => {
    const media = useDimensions();
    const headerRef = React.useRef<HTMLDivElement | null>(null);

    const [mediaNumber, setMediaNumber] = React.useState<number | undefined>(undefined);
    const [mediaHeight, setMediaHeight] = React.useState(0);
    const [canMoveNext, setCanMoveNext] = React.useState(false);
    const [canMoveBack, setCanMoveBack] = React.useState(false);
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

        const baseNumber = mediaNumber ?? props.presenting;
        const nextImage = baseNumber + 1;

        if (nextImage < length) {
            setMediaNumber(nextImage);
        }
    }, [props.collection.length, props.presenting, mediaNumber]);

    const onPrevImage = React.useCallback(() => {
        const length = props.collection.length;
        if (length === 1) {
            return;
        }

        const baseNumber = mediaNumber ?? props.presenting;
        const prevImage = baseNumber - 1;

        if (prevImage > -1) {
            setMediaNumber(prevImage);
        }
    }, [props.collection.length, props.presenting, mediaNumber]);

    /* NEXT/BACK BUTTON ON/OFF */
    React.useEffect(() => {
        const length = props.collection.length - 1;
        const number = mediaNumber ?? props.presenting;

        if (number === length) {
            setCanMoveNext(false);
            setCanMoveBack(true);
        } else if (number !== 0) {
            setCanMoveNext(true);
            setCanMoveBack(true);
        } else {
            setCanMoveNext(true);
            setCanMoveBack(false);
        }
    }, [mediaNumber, props.presenting, props.collection.length]);

    /* SET IMAGE HEIGHT */
    React.useEffect(() => {
        const clientHeight = headerRef.current?.getBoundingClientRect().height;
        if (clientHeight && clientHeight !== 0) {
            setMediaHeight(media.height - clientHeight * 4);
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
                setMediaNumber(undefined);
                setCanCloseMenu(false);
            }, 430);
        }
    }, [props.isOpen, canCloseMenu]);

    /* ON UNMOUNT COMPONENT */
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
            <StandardBackdrop style={{ opacity: canShowBackdrop ? 1 : 0, backgroundColor: props.background }} />
            <div ref={headerRef} className="m-4 is-flex is-justify-content-flex-end">
                <IconButtonSolid name="WindowClose" size={2.0} onClick={onCloseHandler} className="no-select" />
            </div>
            <div className="is-flex is-justify-content-space-between is-align-items-center">
                <IconButtonSolid
                    name="ChevronLeft"
                    size={2.0}
                    className="mx-4 no-select"
                    onClick={onPrevImage}
                    isDisabled={!canMoveBack}
                    isInvisible={props.collection.length === 1 || props.isNavigationOff}
                />
                <figure className="bulma-image">
                    {props.type === "image" ? (
                        <Image
                            source={`${API_BASE_URI}${props.collection[mediaNumber ?? props.presenting]}`}
                            height={mediaHeight}
                            objectFit="scale-down"
                            loading="eager"
                        />
                    ) : (
                        <Video
                            base={API_BASE_URI}
                            source={`${props.collection[mediaNumber ?? props.presenting]}`}
                            poster={`${props.posters[mediaNumber ?? props.presenting]}`}
                            controls={false}
                            autoplay={props.autoplay ?? false}
                            preload="metadata"
                            height={mediaHeight}
                            objectFit="contain"
                        />
                    )}
                </figure>
                <IconButtonSolid
                    name="ChevronRight"
                    size={2.0}
                    className="mx-4 no-select"
                    onClick={onNextImage}
                    isDisabled={!canMoveNext}
                    isInvisible={props.collection.length === 1 || props.isNavigationOff}
                />
            </div>
        </div>
    );
};
