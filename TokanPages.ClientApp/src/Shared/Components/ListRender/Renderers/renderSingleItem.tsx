import * as React from "react";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import CustomListItem from "../CustomListItem/customListItem";
import { IItem } from "../Models/item";
import { GetIcon } from "../../GetIcon/getIcon";

export function RenderSingleItem(props: IItem): JSX.Element
{
    const icon = GetIcon({ iconName: props.icon });
    return(
        <CustomListItem button key={props.id} href={props.link} disabled={!props.enabled} >
            <ListItemIcon>{icon}</ListItemIcon>
            <ListItemText primary={props.name} />
        </CustomListItem>
    );
}
