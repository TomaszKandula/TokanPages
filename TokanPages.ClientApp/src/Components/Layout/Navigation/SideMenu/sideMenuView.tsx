import * as React from "react";
import { Drawer } from "@material-ui/core";
import { RenderImage, RenderSideMenu } from "../../../../Shared/Components";
import { Item } from "../../../../Shared/Components/RenderMenu/Models";
import { GET_ICONS_URL } from "../../../../Api/Request";

interface Properties {
    drawerState: { open: boolean };
    closeHandler: any;
    isAnonymous: boolean;
    languageId: string;
    menu: { image: string; items: Item[] };
}

export const SideMenuView = (props: Properties): React.ReactElement => {
    return (
        <Drawer anchor="left" open={props.drawerState.open} onClose={props.closeHandler}>
            <div className="sidemenu-drawer-container">
                <div className="sidemenu-drawer-hero">
                    <RenderImage basePath={GET_ICONS_URL} imageSource={props?.menu?.image} className="sidemenu-logo" />
                </div>
                <RenderSideMenu
                    isAnonymous={props.isAnonymous}
                    languageId={props.languageId}
                    items={props.menu?.items}
                />
            </div>
        </Drawer>
    );
};
