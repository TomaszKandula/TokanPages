import React, { useRef } from "react";
import { NavigationContentDto } from "../../../../../Api/Models";
import { useDimensions } from "../../../../../Shared/Hooks";
import { Icon } from "../../../../../Shared/Components";
import { ToggleBodyScroll } from "../../../../../Shared/Services/Utilities";
import "./renderBottomSheet.css";

interface RenderBottomSheetProps {
    isBottomSheetOpen: boolean;
    navigation: NavigationContentDto;
    triggerBottomSheet: () => void;
    children: React.ReactElement | React.ReactElement[];
}

export const RenderBottomSheet = (props: RenderBottomSheetProps): React.ReactElement => {
    const media = useDimensions();
    const ref = useRef<HTMLDivElement | null>(null);

    const [canOpenBottomSheet, setCanOpenBottomSheet] = React.useState(false);
    const [canCloseBottomSheet, setCanCloseBottomSheet] = React.useState(false);
    const [canShowBackdrop, setCanShowBackdrop] = React.useState(false);
    const [bottomSheetHeight, setBottomSheetHeight] = React.useState<number>(0);
    const [shouldClear, setShouldClear] = React.useState(false);

    const onCloseHandler = React.useCallback(() => {
        setCanCloseBottomSheet(true);
    }, []);

    React.useEffect(() => {
        const currentHeight = ref.current?.getBoundingClientRect().height ?? 0;
        if (media.height === currentHeight) {
            setShouldClear(true);
        } else {
            setBottomSheetHeight(currentHeight);
            setShouldClear(false);
        }
    }, [ref.current, media.height]);

    React.useEffect(() => {
        if (props.isBottomSheetOpen && !canCloseBottomSheet) {
            ToggleBodyScroll(false);
            setTimeout(() => setCanShowBackdrop(true), 75);
            setTimeout(() => setCanOpenBottomSheet(true), 175);
        }
    }, [props.isBottomSheetOpen, canCloseBottomSheet]);

    React.useEffect(() => {
        if (props.isBottomSheetOpen && canCloseBottomSheet) {
            ToggleBodyScroll(true);
            setTimeout(() => setCanShowBackdrop(false), 250);
            setTimeout(() => setCanOpenBottomSheet(false), 150);
            setTimeout(() => {
                props.triggerBottomSheet();
                setCanCloseBottomSheet(false);
            }, 430);
        }
    }, [props.isBottomSheetOpen, canCloseBottomSheet]);

    if (!props.isBottomSheetOpen || shouldClear) {
        return <></>;
    }

    return (
        <div
            role="presentation"
            className="bottomsheet-nav-drawer-root"
            style={{
                width: media.width,
                height: media.height,
                bottom: canOpenBottomSheet ? -media.height + bottomSheetHeight : -media.height,
            }}
            onMouseLeave={onCloseHandler}
        >
            <div
                aria-hidden="true"
                className="bottomsheet-nav-drawer-backdrop"
                style={{ opacity: canShowBackdrop ? 1 : 0 }}
                onClick={onCloseHandler}
            ></div>
            <div ref={ref} tabIndex={-1} className="bottomsheet-nav-drawer-container">
                <div className="pb-6">
                    <div className="navbar-top-line"></div>
                    <div className="is-flex is-justify-content-space-between is-align-items-center mx-4 mt-5">
                        <h2 className="is-size-4 has-text-weight-normal">{props.navigation?.languageMenu.caption}</h2>
                        <Icon
                            name="WindowClose"
                            size={2.0}
                            className="has-text-grey-dark no-select"
                            onClick={onCloseHandler}
                        />
                    </div>
                    {props.children}
                </div>
            </div>
        </div>
    );
};
