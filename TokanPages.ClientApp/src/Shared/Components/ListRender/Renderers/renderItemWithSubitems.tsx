import * as React from "react";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import { Collapse } from "@material-ui/core";
import { ExpandLess, ExpandMore } from "@material-ui/icons";
import { GetIcon } from "../../GetIcon/getIcon";
import { IItem } from "../Models/item";
import { RenderSingleSubitem } from "./renderSingleSubitem";

export function RenderItemWithSubitems(props: IItem): JSX.Element
{
    const [state, setState] = React.useState(false);
    const onClickEvent = () => setState(!state);

    let renderBuffer: JSX.Element[] = [];
    props.subitems.forEach((item) => 
    {
        renderBuffer.push(<RenderSingleSubitem 
            key={item.id}
            id={item.id} 
            name={item.name}
            link={item.link}
            icon={item.icon}
            enabled={item.enabled}
        />);
    });

    return(
        <>
            <ListItem button key={props.id} onClick={onClickEvent} disabled={!props.enabled} >
                <ListItemIcon>{GetIcon({ iconName: props.icon })}</ListItemIcon>
                <ListItemText primary={props.name} />
                {state ? <ExpandLess /> : <ExpandMore />}
            </ListItem>
            <Collapse in={state} timeout="auto" unmountOnExit>
            <List component="div" disablePadding>
                {renderBuffer}
            </List>
            </Collapse>
        </>
    );
}
