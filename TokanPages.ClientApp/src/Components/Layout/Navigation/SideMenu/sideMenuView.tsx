import * as React from "react";
import { Drawer } from "@material-ui/core";
import { RenderImage, RenderSideMenu } from "../../../../Shared/Components";
import { Item } from "../../../../Shared/Components/RenderMenu/Models";
import { GET_ICONS_URL } from "../../../../Api";
import "./sideMenuView.css";

interface Properties {
    drawerState: { open: boolean };
    closeHandler: any;
    isAnonymous: boolean;
    languageId: string;
    menu: { image: string; items: Item[] };
}

//TODO: replace Drawe w/custom implementation
export const SideMenuView = (props: Properties): React.ReactElement => {
    return (
        <Drawer anchor="left" open={props.drawerState.open} onClose={props.closeHandler}>
            <nav className="sidemenu-drawer-container">
                <div className="sidemenu-drawer-hero">
                    <RenderImage
                        base={GET_ICONS_URL}
                        source={props?.menu?.image}
                        title="Logo"
                        alt="An application logo"
                        className="sidemenu-logo"
                    />
                </div>
                <RenderSideMenu
                    isAnonymous={props.isAnonymous}
                    languageId={props.languageId}
                    items={props.menu?.items}
                />
            </nav>
        </Drawer>
    );
};
