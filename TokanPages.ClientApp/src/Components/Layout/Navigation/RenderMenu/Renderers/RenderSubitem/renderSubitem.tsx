import * as React from "react";
import { Link } from "react-router-dom";
import { ListItem, Link as Href } from "@material-ui/core";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import { SubitemDto } from "../../../../../../Api/Models";
import { GetIcon } from "../../../../../../Shared/Components";
import { EnsureDefined } from "../EnsureDefined";

interface RenderSubitemProps extends SubitemDto {
    indent?: boolean;
    navbar?: boolean;
    onClickEvent?: () => void;
}

export const RenderSubitem = (props: RenderSubitemProps): React.ReactElement => {
    const link: string = props.link as string;
    const isHref: boolean = link.includes("http://") || link.includes("https://");

    const listItemStyle = props.indent ? "render-navbar-list-item-indent" : "render-navbar-list-item";
    const listItemTextIdentStyle = props.indent ? undefined : "render-navbar-list-item-text";
    const listItemTextBaseStyle = props.navbar ? "render-navbar-list-item-text" : "render-navbar-list-item-base";
    const listItemTextStyle = `${listItemTextIdentStyle} ${listItemTextBaseStyle}`;

    const RenderItemWithHref = (): React.ReactElement => {
        return (
            <Href
                href={link}
                onClick={props.onClickEvent}
                className="render-navbar-href"
                underline="none"
                target="_blank"
                rel="noopener nofollow"
            >
                <ListItem button key={props.id} className={listItemStyle} disabled={!props.enabled}>
                    <ListItemIcon className="render-navbar-list-icon">
                        {GetIcon({ name: props.icon as string })}
                    </ListItemIcon>
                    <ListItemText primary={props.value} className={listItemTextStyle} disableTypography={true} />
                </ListItem>
            </Href>
        );
    };

    const RenderItemWithLink = (): React.ReactElement => {
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
                <ListItemIcon className="render-navbar-list-icon">
                    {GetIcon({ name: props.icon as string })}
                </ListItemIcon>
                <ListItemText primary={props.value} className={listItemTextStyle} disableTypography={true} />
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
