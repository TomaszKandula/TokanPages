import React from "react";
import { useDimensions } from "../../../../../Shared/Hooks";
import "./renderBottomSheet.css";

interface RenderBottomSheetProps {
    bottomSheetHeight: number;
    isBottomSheetOpen: boolean;
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
            setTimeout(() => setCanShowBackdrop(true), 150);
            setTimeout(() => setCanOpenBottomSheet(true), 250);
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
            //onMouseLeave={menuHandler}
        >
            <div
                className="bottomsheet-nav-drawer-backdrop"
                style={{ opacity: canShowBackdrop ? 1 : 0 }}
                onClick={menuHandler}
            ></div>
            <div className="bottomsheet-nav-drawer-container" style={{ height: props.bottomSheetHeight }}>
                {props.children}
            </div>
        </nav>
    );
};
