import React from "react";
import { DrawerProps } from "./Types";
import { getWidthRatio } from "./Utilities";
import { StandardBackdrop } from "../Backdrop";
import { useDimensions } from "../../../Shared/Hooks";
import { ToggleBodyScroll } from "../../../Shared/Services/Utilities";
import "./drawer.css";

export const Drawer = (props: DrawerProps): React.ReactElement => {
    const media = useDimensions();
    const widthRatio = getWidthRatio(media);

    const [canOpenMenu, setCanOpenMenu] = React.useState(false);
    const [canCloseMenu, setCanCloseMenu] = React.useState(false);
    const [canShowBackdrop, setCanShowBackdrop] = React.useState(false);

    const menuHandler = React.useCallback(() => {
        setCanCloseMenu(true);
    }, []);

    React.useEffect(() => {
        if (props.isOpen && !canCloseMenu) {
            ToggleBodyScroll(false);
            setTimeout(() => setCanShowBackdrop(true), 150);
            setTimeout(() => setCanOpenMenu(true), 250);
        }
    }, [props.isOpen, canCloseMenu]);

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

    React.useEffect(() => {
        return () => {
            ToggleBodyScroll(true);
        };
    }, []);

    if (!props.isOpen) {
        return <></>;
    }

    return (
        <div
            role="presentation"
            className="drawer-root"
            style={{
                width: media.width,
                left: canOpenMenu ? 0 : -media.width,
            }}
            onMouseLeave={menuHandler}
        >
            <StandardBackdrop style={{ opacity: canShowBackdrop ? 1 : 0 }} onClick={menuHandler} />
            <div tabIndex={-1} className="drawer-container" style={{ width: media.width * widthRatio }}>
                {props.children}
            </div>
        </div>
    );
};
