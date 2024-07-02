import * as React from "react";
import List from "@material-ui/core/List";
import { Item } from "./Models";
import { RenderNavbarItem } from "./Renderers";
import { RenderNavbarMenuStyle } from "./renderNavbarMenuStyle";

interface Properties {
    isAnonymous: boolean;
    items: Item[] | undefined;
}

export const RenderNavbarMenu = (props: Properties): JSX.Element => {
    if (props.items === undefined) return <div>Cannot render content.</div>;
    if (props.items.length === 0) return <div>Cannot render content.</div>;

    const classes = RenderNavbarMenuStyle();

    let renderBuffer: JSX.Element[] = [];
    props.items.forEach(item => {
        const isAnonymous = props.isAnonymous && (item.link === "/account" || item.link === "/signout");
        const isNotAnonymous = !props.isAnonymous && (item.link === "/signin" || item.link === "/signup");
        const isRoot = item.link === "/";
        const isTerms = item.link === "/terms";
        const isPolicy = item.link === "/policy";

        switch (item.type) {
            case "item": {
                if (isAnonymous) return;
                if (isNotAnonymous) return;
                if (isRoot) return;
                if (isTerms) return;
                if (isPolicy) return;

                renderBuffer.push(
                    <RenderNavbarItem
                        key={item.id}
                        id={item.id}
                        type={item.type}
                        value={item.value}
                        link={item.link}
                        icon={item.icon}
                        enabled={item.enabled}
                    />
                );
                break;
            }

            case "itemspan": {
                if (isAnonymous) return;
                if (isNotAnonymous) return;
                if (isRoot) return;

                renderBuffer.push(
                    <RenderNavbarItem
                        key={item.id}
                        id={item.id}
                        type={item.type}
                        value={item.value}
                        link={item.link}
                        icon={item.icon}
                        enabled={item.enabled}
                    />
                );
                break;
            }

            default: {
                renderBuffer.push(<></>);
            }
        }
    });

    return <List className={classes.list}>{renderBuffer}</List>;
}
