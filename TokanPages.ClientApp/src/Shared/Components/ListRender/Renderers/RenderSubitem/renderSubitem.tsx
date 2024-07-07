import * as React from "react";
import { Link } from "react-router-dom";
import { ListItem, Link as Href } from "@material-ui/core";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import { Subitem } from "../../Models";
import { GetIcon } from "../../../../../Shared/Components";
import { EnsureDefined } from "../EnsureDefined";
import { RenderSubitemsStyle } from "./renderSubitemsStyle";

interface RenderSubitemProps extends Subitem {
    indent?: boolean;
    onClickEvent?: () => void;
}

export const RenderSubitem = (props: RenderSubitemProps): JSX.Element => {
    const classes = RenderSubitemsStyle();

    const link: string = props.link as string;
    const isHref: boolean = link.includes("http://") || link.includes("https://");

    const listItemStyle = props.indent ? classes.list_item_indent : classes.list_item;
    const listItemTextStyle = props.indent ? undefined : classes.list_item_text;

    const RenderItemWithHref = (): JSX.Element => {
        return (
            <Href href={link} onClick={props.onClickEvent} className={classes.href} underline="none" target="_blank" rel="noopener">
                <ListItem
                    button
                    key={props.id}
                    className={listItemStyle}
                    disabled={!props.enabled}
                >
                    <ListItemIcon>{GetIcon({ iconName: props.icon as string })}</ListItemIcon>
                    <ListItemText primary={props.value} className={listItemTextStyle} disableTypography={true} />
                </ListItem>
            </Href>
        );
    };

    const RenderItemWithLink = (): JSX.Element => {
        return (
            <ListItem
                button
                key={props.id}
                onClick={props.onClickEvent}
                className={listItemStyle}
                disabled={!props.enabled}
                component={Link}
                to={props.link as string}
            >
                <ListItemIcon>{GetIcon({ iconName: props.icon as string })}</ListItemIcon>
                <ListItemText primary={props.value} className={listItemTextStyle} disableTypography={true} />
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
