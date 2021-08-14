import * as React from "react";
import { Link } from "react-router-dom";
import { ListItem } from "@material-ui/core";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import { ISubitem } from "../Models/subitem";
import { GetIcon } from "../../GetIcon/getIcon";
import subitemsStyle from "../Styles/subitemsStyle";

export function RenderSubitem(props: ISubitem): JSX.Element
{
    if (props.link === undefined) 
        return(<div>Cannot render. Missing 'link' property.</div>);

    if (props.icon === undefined) 
        return(<div>Cannot render. Missing 'icon' property.</div>);

    if (props.enabled === undefined) 
        return(<div>Cannot render. Missing 'enabled' property.</div>);

    const icon = GetIcon({ iconName: props.icon });
    const classes = subitemsStyle();
    return(
        <ListItem button key={props.id} className={classes.nested} disabled={!props.enabled} component={Link} to={props.link}>
            <ListItemIcon>{icon}</ListItemIcon>
            <ListItemText primary={props.value} />
        </ListItem>
    );
}
