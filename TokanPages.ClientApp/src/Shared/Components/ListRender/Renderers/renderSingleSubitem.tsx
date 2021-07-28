import * as React from "react";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import CustomListItem from "../CustomListItem/customListItem";
import { ISubitem } from "../Models/subitem";
import { GetIcon } from "../../GetIcon/getIcon";
import subitemsStyle from "../Styles/subitemsStyle";

export function RenderSingleSubitem(props: ISubitem): JSX.Element
{
    const classes = subitemsStyle();
    const icon = GetIcon({ iconName: props.icon });
    return(
        <CustomListItem button key={props.id} href={props.link} className={classes.nested} disabled={!props.enabled} >
            <ListItemIcon>{icon}</ListItemIcon>
            <ListItemText primary={props.name} />
        </CustomListItem>
    );
}
