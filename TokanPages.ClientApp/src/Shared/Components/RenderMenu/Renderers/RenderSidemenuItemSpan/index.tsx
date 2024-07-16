import * as React from "react";
import { useDispatch } from "react-redux";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import { Collapse } from "@material-ui/core";
import { ExpandLess, ExpandMore } from "@material-ui/icons";
import { ApplicationNavbarAction } from "../../../../../Store/Actions";
import { GetIcon } from "../../..";
import { Item } from "../../Models";
import { EnsureDefined } from "../EnsureDefined";
import { RenderSubitem } from "../RenderSubitem/renderSubitem";

export const RenderSidemenuItemSpan = (props: Item): JSX.Element => {
    const dispatch = useDispatch();
    const [isOpen, setIsOpen] = React.useState(false);
    const onListItemClickEvent = React.useCallback(() => setIsOpen(!isOpen), [isOpen]);

    const onSubitemClickEvent = React.useCallback(() => {
        dispatch(ApplicationNavbarAction.set({ 
            selection: props.id,
            name: props.value,
            path: props.link,
        }));
    }, [props.id, props.value, props.link]);

    return EnsureDefined(
        {
            values: [props.link, props.icon, props.enabled, props.subitems],
            messages: [
                "Cannot render. Missing 'link' property.",
                "Cannot render. Missing 'icon' property.",
                "Cannot render. Missing 'enabled' property.",
                "Cannot render. Missing 'subitem' property.",
            ],
        },
        <>
            <ListItem button key={props.id} onClick={onListItemClickEvent} disabled={!props.enabled}>
                <ListItemIcon>{GetIcon({ iconName: props.icon as string })}</ListItemIcon>
                <ListItemText primary={props.value} />
                {isOpen ? <ExpandLess /> : <ExpandMore />}
            </ListItem>
            <Collapse in={isOpen} timeout="auto" unmountOnExit>
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
                            indent={true}
                            navbar={false}
                            onClickEvent={onSubitemClickEvent}
                        />
                    ))}
                </List>
            </Collapse>
        </>
    );
};
