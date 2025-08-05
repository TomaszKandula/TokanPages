import * as React from "react";
import { GET_ICONS_URL } from "../../../../../Api";
import { NavigationContentDto } from "../../../../../Api/Models";
import { CustomImage, Icon, IconButton } from "../../../../../Shared/Components";
import { useDimensions } from "../../../../../Shared/Hooks";
import { RenderSideMenu } from "../RenderMenu";
import "./renderDrawer.css";

interface RenderDrawerProps {
    isAnonymous: boolean;
    isMenuOpen: boolean;
    triggerSideMenu: () => void;
    navigation: NavigationContentDto;
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
                <div className="is-flex is-justify-content-space-between">
                    <CustomImage
                        base={GET_ICONS_URL}
                        source={props.navigation?.menu?.image}
                        title="Logo"
                        alt="An application logo"
                        className="ml-4"
                        width={40}
                        height={40}
                    />
                    <IconButton hasNoHoverEffect className="no-select mr-2">
                        <Icon name="WindowClose" size={1} onClick={menuHandler} />
                    </IconButton>
                </div>
                <hr className="line-separator" />
                <RenderSideMenu
                    isAnonymous={props.isAnonymous}
                    languageId={props.languageId}
                    items={props.navigation.menu?.items}
                />
            </div>
        </nav>
    );
};
