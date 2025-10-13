import * as React from "react";
import { GET_IMAGES_URL } from "../../../../Api";
import { APP_BAR_HEIGHT_DESKTOP, APP_BAR_HEIGHT_NON_DESKTOP } from "../../../../Shared/ConstantsTemp";
import { AppBar, CustomImage, Drawer, Icon, IconButton } from "../../../../Shared/Components";
import { RenderSideMenu, RenderToolbarLargeScreen, RenderToolbarSmallScreen } from "../Components";
import { NavigationViewProps } from "../Abstractions";

const RenderBackNavigationOnly = (props: NavigationViewProps): React.ReactElement => (
    <AppBar height={APP_BAR_HEIGHT_DESKTOP}>
        <IconButton hasNoHoverEffect={props.media.isMobile || props.media.isTablet} onClick={props.backPathHandler}>
            <Icon name="ArrowLeft" size={1.5} />
        </IconButton>
    </AppBar>
);

const RenderFullNavigation = (props: NavigationViewProps): React.ReactElement => {
    const height = props.media.isDesktop ? APP_BAR_HEIGHT_DESKTOP : APP_BAR_HEIGHT_NON_DESKTOP;
    const [isClose, setIsClose] = React.useState(false);
    React.useEffect(() => {
        if (!props.isMenuOpen && isClose) {
            setIsClose(false);
        }
    }, [props.isMenuOpen, isClose]);

    return (
        <AppBar height={height} offset={2}>
            <RenderToolbarLargeScreen {...props} />
            <RenderToolbarSmallScreen {...props} />
            <Drawer isOpen={props.isMenuOpen} isExternalClose={isClose} onTrigger={props.triggerSideMenu}>
                <div className="is-flex is-justify-content-space-between">
                    <CustomImage
                        base={GET_IMAGES_URL}
                        source={props.navigation?.menu?.image}
                        title="Logo"
                        alt="An application logo"
                        className="ml-4"
                        width={40}
                        height={40}
                    />
                    <IconButton hasNoHoverEffect className="no-select mr-2">
                        <Icon
                            name="WindowClose"
                            size={1.5}
                            onClick={() => {
                                setIsClose(true);
                            }}
                        />
                    </IconButton>
                </div>
                <hr className="line-separator" />
                <RenderSideMenu
                    isAnonymous={props.isAnonymous}
                    languageId={props.languageId}
                    items={props.navigation?.menu?.items}
                />
            </Drawer>
        </AppBar>
    );
};

export const NavigationView = (props: NavigationViewProps): React.ReactElement =>
    props.backNavigationOnly ? <RenderBackNavigationOnly {...props} /> : <RenderFullNavigation {...props} />;
