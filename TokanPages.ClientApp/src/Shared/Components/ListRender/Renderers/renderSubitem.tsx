import * as React from "react";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import CustomListItem from "../CustomListItem/customListItem";
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

    const classes = subitemsStyle();
    const icon = GetIcon({ iconName: props.icon });
    return(
        <CustomListItem button key={props.id} href={props.link} className={classes.nested} disabled={!props.enabled} >
            <ListItemIcon>{icon}</ListItemIcon>
            <ListItemText primary={props.value} />
        </CustomListItem>
    );
}
