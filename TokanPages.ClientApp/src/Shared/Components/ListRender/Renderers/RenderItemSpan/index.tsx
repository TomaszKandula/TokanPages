import * as React from "react";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import { Collapse } from "@material-ui/core";
import { ExpandLess, ExpandMore } from "@material-ui/icons";
import { GetIcon } from "../../..";
import { IItem } from "../../Models";
import { EnsureDefined } from "../EnsureDefined";
import { RenderSubitem } from "../RenderSubitem/renderSubitem";

export const RenderItemSpan = (props: IItem): JSX.Element =>
{
    const [state, setState] = React.useState(false);
    const onClickEvent = React.useCallback(() => setState(!state), [state]);

    return(EnsureDefined(
        {
            values: 
            [
                props.link,
                props.icon,
                props.enabled,
                props.subitems
            ],
            messages: 
            [
                "Cannot render. Missing 'link' property.",
                "Cannot render. Missing 'icon' property.",
                "Cannot render. Missing 'enabled' property.",
                "Cannot render. Missing 'subitem' property."
            ]
        }, 
        <>
            <ListItem button key={props.id} onClick={onClickEvent} disabled={!props.enabled} >
                <ListItemIcon>{GetIcon({ iconName: props.icon as string })}</ListItemIcon>
                <ListItemText primary={props.value} />
                {state ? <ExpandLess /> : <ExpandMore />}
            </ListItem>
            <Collapse in={state} timeout="auto" unmountOnExit>
            <List component="div" disablePadding>
                {props.subitems?.map(item => (
                <RenderSubitem 
                    key={item.id}
                    id={item.id} 
                    type={item.type}
                    value={item.value}
                    link={item.link}
                    icon={item.icon}
                    enabled={item.enabled}
                />))}
            </List>
            </Collapse>
        </>)
    );
}
