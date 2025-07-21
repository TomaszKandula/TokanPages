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
    menuHandler: () => void;
    logo: string;
    menu: { image: string; items: ItemDto[] };
    languageId: string;
}

export const RenderDrawer = (props: RenderDrawerProps): React.ReactElement => {
    const dimensions = useDimensions();

    const [canOpen, setCanOpen] = React.useState(false);
    const [canClose, setCanClose] = React.useState(false);

    const menuHandler = React.useCallback(() => {
        setCanClose(true);
    }, []);

    React.useEffect(() => {
        if (props.isMenuOpen && !canOpen) {
            setTimeout(() => setCanOpen(true), 100);
        }

        if (props.isMenuOpen && canClose) {
            setCanOpen(false);
            setTimeout(() => {
                props.menuHandler();
                setCanClose(false);
                setCanOpen(false);
            }, 250);
        }
    }, [props.isMenuOpen, canOpen, canClose]);

    if (!props.isMenuOpen) {
        return <></>;
    }

    return (
        <nav
            className="navigation-nav-drawer-root"
            style={{ width: dimensions.width, left: canOpen ? 0 : -dimensions.width }}
            onMouseLeave={menuHandler}
        >
            <div
                className="navigation-nav-drawer-backdrop"
                style={{ opacity: canOpen ? 1 : 0 }}
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
