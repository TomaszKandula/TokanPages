import * as React from "react";
import { Box, Drawer } from "@material-ui/core";
import { RenderList } from "../../../../Shared/Components";
import { Item } from "../../../../Shared/Components/ListRender/Models";
import { SideMenuStyle } from "./sideMenuStyle";

interface Properties {
    drawerState: { open: boolean };
    closeHandler: any;
    isAnonymous: boolean;
    menu: { image: string; items: Item[] };
}

export const SideMenuView = (props: Properties): JSX.Element => {
    const classes = SideMenuStyle();
    const logo = "</>";

    return (
        <Drawer anchor="left" open={props.drawerState.open} onClose={props.closeHandler}>
            <div className={classes.drawer_container}>
                <Box className={classes.drawer_hero}>
                    <div className={classes.drawer_logo}>{logo}</div>
                </Box>
                <RenderList isAnonymous={props.isAnonymous} items={props.menu?.items} />
            </div>
        </Drawer>
    );
};
