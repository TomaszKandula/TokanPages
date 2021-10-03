import * as React from "react";
import { Drawer } from "@material-ui/core";
import { RenderList } from "../../../Shared/Components/ListRender/renderList";
import { IItem } from "../../../Shared/Components/ListRender/Models/item";
import { IMAGES_PATH } from "../../../Shared/constants";
import menuStyle from "./menuStyle";
import Validate from "validate.js";

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

const MenuView = (props: IBinding): JSX.Element =>
{
    const classes = menuStyle();
    const image = Validate.isEmpty(props.bind.menu?.image) ? "" : `${IMAGES_PATH}background/${props.bind.menu?.image}`;
    return (
        <Drawer anchor="left" open={props.bind.drawerState.open} onClose={props.bind.closeHandler}>
            <div className={classes.drawerContainer}>
                <img src={image} className={classes.menuBackground} alt="" />
                <RenderList bind=
                {{ 
                    isAnonymous: props.bind.isAnonymous, 
                    items: props.bind.menu?.items 
                }}/>
            </div>
        </Drawer>
    );
}

export default MenuView;
