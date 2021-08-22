import * as React from "react";
import { Link } from "react-router-dom";
import { ListItem } from "@material-ui/core";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import { IItem } from "../Models/item";
import { GetIcon } from "../../GetIcon/getIcon";

export const RenderItem = (props: IItem): JSX.Element =>
{
    if (props.link === undefined) 
        return(<div>Cannot render. Missing 'link' property.</div>);

    if (props.icon === undefined) 
        return(<div>Cannot render. Missing 'icon' property.</div>);

    if (props.enabled === undefined) 
        return(<div>Cannot render. Missing 'enabled' property.</div>);

    const icon = GetIcon({ iconName: props.icon });
    return(
        <ListItem button key={props.id} disabled={!props.enabled} component={Link} to={props.link}>
            <ListItemIcon>{icon}</ListItemIcon>
            <ListItemText primary={props.value} />
        </ListItem>
    );
}
