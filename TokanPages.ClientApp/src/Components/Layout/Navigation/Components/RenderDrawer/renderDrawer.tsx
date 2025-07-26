import * as React from "react";
import { GET_ICONS_URL } from "../../../../../Api";
import { ItemDto } from "../../../../../Api/Models";
import { CustomImage } from "../../../../../Shared/Components";
import { useDimensions } from "../../../../../Shared/Hooks";
import { RenderSideMenu } from "../RenderMenu";
import "./renderDrawer.css";

interface RenderDrawerProps {
    isAnonymous: boolean;
    isMenuOpen: boolean;
    triggerSideMenu: () => void;
    logo: string;
    menu: { image: string; items: ItemDto[] };
    languageId: string;
}

export const RenderDrawer = (props: RenderDrawerProps): React.ReactElement => {
    const dimensions = useDimensions();

    const [canOpenMenu, setCanOpenMenu] = React.useState(false);
    const [canCloseMenu, setCanCloseMenu] = React.useState(false);
    const [canShowBackdrop, setCanShowBackdrop] = React.useState(false);

    const menuHandler = React.useCallback(() => {
        setCanCloseMenu(true);
    }, []);

    React.useEffect(() => {
        if (props.isMenuOpen && !canCloseMenu) {
            setTimeout(() => setCanShowBackdrop(true), 150);
            setTimeout(() => setCanOpenMenu(true), 250);
        }
    }, [props.isMenuOpen, canCloseMenu]);

    React.useEffect(() => {
        if (props.isMenuOpen && canCloseMenu) {
            setTimeout(() => setCanShowBackdrop(false), 250);
            setTimeout(() => setCanOpenMenu(false), 150);
            setTimeout(() => {
                props.triggerSideMenu();
                setCanCloseMenu(false);
            }, 430);
        }
    }, [props.isMenuOpen, canCloseMenu]);

    if (!props.isMenuOpen) {
        return <></>;
    }

    return (
        <nav
            className="navigation-nav-drawer-root"
            style={{
                width: dimensions.width,
                left: canOpenMenu ? 0 : -dimensions.width,
            }}
            onMouseLeave={menuHandler}
        >
            <div
                className="navigation-nav-drawer-backdrop"
                style={{ opacity: canShowBackdrop ? 1 : 0 }}
                onClick={menuHandler}
            ></div>
            <div className="navigation-nav-drawer-container">
                <div className="navigation-nav-drawer-hero">
                    <CustomImage
                        base={GET_ICONS_URL}
                        source={props?.menu?.image}
                        title="Logo"
                        alt="An application logo"
                        className="navigation-nav-drawer-logo"
                    />
                </div>
                <RenderSideMenu
                    isAnonymous={props.isAnonymous}
                    languageId={props.languageId}
                    items={props.menu?.items}
                />
            </div>
        </nav>
    );
};
