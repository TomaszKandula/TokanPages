import * as React from "react";
import { Link } from "react-router-dom";
import { ListItem, Link as Href } from "@material-ui/core";
import ListItemText from "@material-ui/core/ListItemText";
import { Item } from "../../Models";
import { EnsureDefined } from "../EnsureDefined";
import { RenderNavbarItemStyle } from "./renderNavbarItemStyle";

export const RenderNavbarItem = (props: Item): JSX.Element => {
    const classes = RenderNavbarItemStyle();

    const isSelected = window.location.pathname !== "/" && window.location.pathname === props.link;
    const selectionClass = `${classes.list_item_text} ${classes.list_item_text_selected}`;
    const selectionStyle = isSelected ? selectionClass : classes.list_item_text;

    const link: string = props.link as string;
    const isHref: boolean = link.includes("http://") || link.includes("https://");

    const RenderItemWithHref = (): JSX.Element => {
        return (
            <Href href={link} className={classes.href} underline="none" target="_blank" rel="noopener">
                <ListItem button key={props.id} disabled={!props.enabled}>
                    <ListItemText primary={props.value} className={selectionStyle} disableTypography={true} />
                </ListItem>
            </Href>
        );
    };

    const RenderItemWithLink = (): JSX.Element => {
        return (
            <ListItem
                button
                key={props.id}
                disabled={!props.enabled}
                component={Link}
                to={props.link as string}
                className={classes.list_item}
            >
                <ListItemText primary={props.value} className={selectionStyle} disableTypography={true} />
            </ListItem>
        );
    };

    const RenderListItem = (): JSX.Element => {
        return isHref ? <RenderItemWithHref /> : <RenderItemWithLink />;
    };

    return EnsureDefined(
        {
            values: [props.link, props.icon, props.enabled],
            messages: [
                "Cannot render. Missing 'link' property.",
                "Cannot render. Missing 'icon' property.",
                "Cannot render. Missing 'enabled' property.",
            ],
        },
        <RenderListItem />
    );
};
