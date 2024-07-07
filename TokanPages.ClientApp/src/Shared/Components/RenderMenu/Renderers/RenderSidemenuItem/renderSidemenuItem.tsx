import * as React from "react";
import { Link } from "react-router-dom";
import { ListItem, Link as Href } from "@material-ui/core";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import { Item } from "../../Models";
import { GetIcon } from "../../..";
import { EnsureDefined } from "../EnsureDefined";
import { RenderSidemenuItemStyle } from "./renderSidemenuItemStyle";

export const RenderSidemenuItem = (props: Item): JSX.Element => {
    const classes = RenderSidemenuItemStyle();
    const link: string = props.link as string;
    const isHref: boolean = link.includes("http://") || link.includes("https://");

    const RenderItemWithHref = (): JSX.Element => {
        return (
            <Href href={link} className={classes.href} underline="none" target="_blank" rel="noopener">
                <ListItem button key={props.id} disabled={!props.enabled}>
                    <ListItemIcon>{GetIcon({ iconName: props.icon as string })}</ListItemIcon>
                    <ListItemText primary={props.value} />
                </ListItem>
            </Href>
        );
    };

    const RenderItemWithLink = (): JSX.Element => {
        return (
            <ListItem button key={props.id} disabled={!props.enabled} component={Link} to={props.link as string}>
                <ListItemIcon>{GetIcon({ iconName: props.icon as string })}</ListItemIcon>
                <ListItemText primary={props.value} />
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
