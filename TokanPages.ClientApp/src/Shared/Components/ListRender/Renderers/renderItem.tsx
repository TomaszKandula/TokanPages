import * as React from "react";
import { Link } from "react-router-dom";
import { ListItem } from "@material-ui/core";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import { IItem } from "../Models/item";
import { GetIcon } from "../../GetIcon/getIcon";
import { EnsureDefined } from "./ensureDefined";

export const RenderItem = (props: IItem): JSX.Element =>
{
    return(EnsureDefined(
        {
            values: 
            [
                props.link,
                props.icon,
                props.enabled
            ],
            messages: 
            [
                "Cannot render. Missing 'link' property.",
                "Cannot render. Missing 'icon' property.",
                "Cannot render. Missing 'enabled' property."
            ]
        }, 
        <ListItem button key={props.id} disabled={!props.enabled} component={Link} to={props.link as string}>
            <ListItemIcon>{GetIcon({ iconName: props.icon as string })}</ListItemIcon>
            <ListItemText primary={props.value}/>
        </ListItem>
    ));
}
