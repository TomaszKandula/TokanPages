import * as React from "react";
import { Box, Drawer } from "@material-ui/core";
import { RenderImage, RenderList } from "../../../../Shared/Components";
import { Item } from "../../../../Shared/Components/ListRender/Models";
import { GET_ICONS_URL } from "../../../../Api/Request";
import { SideMenuStyle } from "./sideMenuStyle";

interface Properties {
    drawerState: { open: boolean };
    closeHandler: any;
    isAnonymous: boolean;
    menu: { image: string; items: Item[] };
}

export const SideMenuView = (props: Properties): JSX.Element => {
    const classes = SideMenuStyle();
    return (
        <Drawer anchor="left" open={props.drawerState.open} onClose={props.closeHandler}>
            <div className={classes.drawer_container}>
                <Box className={classes.drawer_hero}>
                    {RenderImage(GET_ICONS_URL, props.menu.image, classes.logo)}
                </Box>
                <RenderList isAnonymous={props.isAnonymous} items={props.menu?.items} />
            </div>
        </Drawer>
    );
};
