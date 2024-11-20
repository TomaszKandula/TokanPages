import * as React from "react";
import { Drawer } from "@material-ui/core";
import { RenderImage, RenderSideMenu } from "../../../../Shared/Components";
import { Item } from "../../../../Shared/Components/RenderMenu/Models";
import { GET_ICONS_URL } from "../../../../Api/Request";

interface Properties {
    drawerState: { open: boolean };
    closeHandler: any;
    isAnonymous: boolean;
    menu: { image: string; items: Item[] };
}

export const SideMenuView = (props: Properties): React.ReactElement => {
    return (
        <Drawer anchor="left" open={props.drawerState.open} onClose={props.closeHandler}>
            <div className="sidemenu-drawer-container">
                <div className="sidemenu-drawer-hero">
                    {RenderImage(GET_ICONS_URL, props?.menu?.image, "sidemenu-logo")}
                </div>
                <RenderSideMenu isAnonymous={props.isAnonymous} items={props.menu?.items} />
            </div>
        </Drawer>
    );
};
