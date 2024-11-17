import * as React from "react";
import { Link } from "react-router-dom";
import { ListItem, Link as Href } from "@material-ui/core";
import ListItemText from "@material-ui/core/ListItemText";
import { Item } from "../../Models";
import { EnsureDefined } from "../EnsureDefined";

export const RenderNavbarItem = (props: Item): React.ReactElement => {
    const isSelected = window.location.pathname !== "/" && window.location.pathname === props.link;
    const selectionClass = "render-navbar-list-item-text render-navbar-list-item-text-selected";
    const selectionStyle = isSelected ? selectionClass : "render-navbar-list-item-text";

    const link: string = props.link as string;
    const isHref: boolean = link.includes("http://") || link.includes("https://");

    const RenderItemWithHref = (): React.ReactElement => {
        return (
            <Href href={link} className="render-navbar-href" underline="none" target="_blank" rel="noopener">
                <ListItem button key={props.id} disabled={!props.enabled}>
                    <ListItemText primary={props.value} className={selectionStyle} disableTypography={true} />
                </ListItem>
            </Href>
        );
    };

    const RenderItemWithLink = (): React.ReactElement => {
        return (
            <ListItem
                button
                key={props.id}
                disabled={!props.enabled}
                component={Link}
                to={props.link as string}
                className="render-navbar-list-item"
            >
                <ListItemText primary={props.value} className={selectionStyle} disableTypography={true} />
            </ListItem>
        );
    };

    const RenderListItem = (): React.ReactElement => {
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
