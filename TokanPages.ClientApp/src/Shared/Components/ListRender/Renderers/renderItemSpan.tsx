import * as React from "react";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import { Collapse } from "@material-ui/core";
import { ExpandLess, ExpandMore } from "@material-ui/icons";
import { GetIcon } from "../../GetIcon/getIcon";
import { IItem } from "../Models/item";
import { RenderSubitem } from "./renderSubitem";

export function RenderItemSpan(props: IItem): JSX.Element
{
    const [state, setState] = React.useState(false);
    const onClickEvent = () => setState(!state);

    if (props.subitems === undefined) 
    {
        return(<div>Cannot render item.</div>);
    }

    let renderBuffer: JSX.Element[] = [];
    props.subitems.forEach(item => 
    {
        renderBuffer.push(<RenderSubitem 
            key={item.id}
            id={item.id} 
            type={item.type}
            value={item.value}
            link={item.link}
            icon={item.icon}
            enabled={item.enabled}
        />);
    });

    if (props.link === undefined) 
        return(<div>Cannot render. Missing 'link' property.</div>);

    if (props.icon === undefined) 
        return(<div>Cannot render. Missing 'icon' property.</div>);

    if (props.enabled === undefined) 
        return(<div>Cannot render. Missing 'enabled' property.</div>);

    return(
        <>
            <ListItem button key={props.id} onClick={onClickEvent} disabled={!props.enabled} >
                <ListItemIcon>{GetIcon({ iconName: props.icon })}</ListItemIcon>
                <ListItemText primary={props.value} />
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
