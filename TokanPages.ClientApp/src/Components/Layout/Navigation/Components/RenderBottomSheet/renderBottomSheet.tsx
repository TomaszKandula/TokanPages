import React from "react";
import { NavigationContentDto } from "../../../../../Api/Models";
import { useDimensions } from "../../../../../Shared/Hooks";
import { Icon, IconButton } from "../../../../../Shared/Components";
import "./renderBottomSheet.css";

interface RenderBottomSheetProps {
    bottomSheetHeight: number;
    isBottomSheetOpen: boolean;
    navigation: NavigationContentDto;
    triggerBottomSheet: () => void;
    children: React.ReactElement | React.ReactElement[];
}

export const RenderBottomSheet = (props: RenderBottomSheetProps): React.ReactElement => {
    const dimensions = useDimensions();

    const [canOpenBottomSheet, setCanOpenBottomSheet] = React.useState(false);
    const [canCloseBottomSheet, setCanCloseBottomSheet] = React.useState(false);
    const [canShowBackdrop, setCanShowBackdrop] = React.useState(false);

    const menuHandler = React.useCallback(() => {
        setCanCloseBottomSheet(true);
    }, []);

    React.useEffect(() => {
        if (props.isBottomSheetOpen && !canCloseBottomSheet) {
            setTimeout(() => setCanShowBackdrop(true), 75);
            setTimeout(() => setCanOpenBottomSheet(true), 175);
        }
    }, [props.isBottomSheetOpen, canCloseBottomSheet]);

    React.useEffect(() => {
        if (props.isBottomSheetOpen && canCloseBottomSheet) {
            setTimeout(() => setCanShowBackdrop(false), 250);
            setTimeout(() => setCanOpenBottomSheet(false), 150);
            setTimeout(() => {
                props.triggerBottomSheet();
                setCanCloseBottomSheet(false);
            }, 430);
        }
    }, [props.isBottomSheetOpen, canCloseBottomSheet]);

    if (!props.isBottomSheetOpen) {
        return <></>;
    }

    return (
        <nav
            className="bottomsheet-nav-drawer-root"
            style={{
                width: dimensions.width,
                bottom: canOpenBottomSheet ? -dimensions.height + props.bottomSheetHeight : -dimensions.height,
            }}
            onMouseLeave={menuHandler}
        >
            <div
                className="bottomsheet-nav-drawer-backdrop"
                style={{ opacity: canShowBackdrop ? 1 : 0 }}
                onClick={menuHandler}
            ></div>
            <div className="bottomsheet-nav-drawer-container" style={{ height: props.bottomSheetHeight }}>
                <div className="navbar-top-line"></div>
                <div className="is-flex is-justify-content-space-between is-align-items-center mx-4 mt-2">
                    <h2 className="is-size-4 has-text-weight-semibold">{props.navigation?.languageMenu.caption}</h2>
                    <IconButton onClick={menuHandler} hasNoHoverEffect className="no-select">
                        <Icon name="WindowClose" size={1.5} className="has-text-grey-dark" />
                    </IconButton>
                </div>
                {props.children}
            </div>
        </nav>
    );
};
