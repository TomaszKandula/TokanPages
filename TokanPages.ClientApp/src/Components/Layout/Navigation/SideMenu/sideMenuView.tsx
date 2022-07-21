import * as React from "react";
import { Box, Drawer } from "@material-ui/core";
import { RenderList } from "../../../../Shared/Components/ListRender/renderList";
import { IItem } from "../../../../Shared/Components/ListRender/Models/item";
import sideMenuStyle from "./sideMenuStyle";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    drawerState: { open: boolean };
    closeHandler: any;
    isAnonymous: boolean;
    menu: { image: string, items: IItem[] };
}

const SideMenuView = (props: IBinding): JSX.Element =>
{
    const classes = sideMenuStyle();
    const logo = "</>";

    return (
        <Drawer anchor="left" open={props.bind.drawerState.open} onClose={props.bind.closeHandler}>
            <div className={classes.drawer_container}>
                <Box className={classes.drawer_hero}>
                    <div className={classes.drawer_logo}>{logo}</div>
                </Box>
                <RenderList bind=
                {{ 
                    isAnonymous: props.bind.isAnonymous, 
                    items: props.bind.menu?.items 
                }}/>
            </div>
        </Drawer>
    );
}

export default SideMenuView;
