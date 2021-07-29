import * as React from "react";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import CustomListItem from "../CustomListItem/customListItem";
import { IItem } from "../Models/item";
import { GetIcon } from "../../GetIcon/getIcon";

export function RenderItem(props: IItem): JSX.Element
{
    if (props.link === undefined) 
        return(<div>Cannot render. Missing 'link' property.</div>);

    if (props.icon === undefined) 
        return(<div>Cannot render. Missing 'icon' property.</div>);

    if (props.enabled === undefined) 
        return(<div>Cannot render. Missing 'enabled' property.</div>);

    const icon = GetIcon({ iconName: props.icon });
    return(
        <CustomListItem button key={props.id} href={props.link} disabled={!props.enabled} >
            <ListItemIcon>{icon}</ListItemIcon>
            <ListItemText primary={props.value} />
        </CustomListItem>
    );
}
