import * as React from "react";
import { Link } from "react-router-dom";
import { ListItem, Link as Href } from "@material-ui/core";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import { GetIcon } from "../../../../../../../Shared/Components";
import { ItemDto } from "../../../../../../../Api/Models";
import { EnsureDefined } from "../EnsureDefined";

export const RenderSidemenuItem = (props: ItemDto): React.ReactElement => {
    const link: string = props.link as string;
    const isHref: boolean = link.includes("http://") || link.includes("https://");

    const RenderItemWithHref = (): React.ReactElement => {
        return (
            <Href href={link} className="render-navbar-href" underline="none" target="_blank" rel="noopener nofollow">
                <ListItem button key={props.id} disabled={!props.enabled}>
                    <ListItemIcon className="render-navbar-list-icon">
                        {GetIcon({ name: props.icon as string })}
                    </ListItemIcon>
                    <ListItemText primary={props.value} />
                </ListItem>
            </Href>
        );
    };

    const RenderItemWithLink = (): React.ReactElement => {
        return (
            <ListItem button key={props.id} disabled={!props.enabled} component={Link} to={props.link as string}>
                <ListItemIcon className="render-navbar-list-icon">
                    {GetIcon({ name: props.icon as string })}
                </ListItemIcon>
                <ListItemText primary={props.value} />
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
