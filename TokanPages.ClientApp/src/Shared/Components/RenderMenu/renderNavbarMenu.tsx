import * as React from "react";
import List from "@material-ui/core/List";
import { Item } from "./Models";
import { RenderNavbarItem, RenderNavbarItemSpan } from "./Renderers";
import { RenderNavbarMenuStyle } from "./renderNavbarMenuStyle";

interface Properties {
    isAnonymous: boolean;
    items: Item[] | undefined;
}

export const RenderNavbarMenu = (props: Properties): React.ReactElement => {
    const classes = RenderNavbarMenuStyle();

    if (props.items === undefined) return <div>Cannot render content.</div>;
    if (props.items.length === 0) return <div>Cannot render content.</div>;

    props.items.sort((a: Item, b: Item) => {
        const item1 = a.navbarMenu?.sortOrder ?? 0;
        const item2 = b.navbarMenu?.sortOrder ?? 0;
        return item1 < item2 ? -1 : item1 > item2 ? 1 : 0;
    });

    let renderBuffer: React.ReactElement[] = [];
    props.items?.forEach(item => {
        const isAnonymous = props.isAnonymous && item.link === "/account";
        const isNotAnonymous = !props.isAnonymous && (item.link === "/signin" || item.link === "/signup");

        switch (item.type) {
            case "item": {
                if (isAnonymous) return;
                if (isNotAnonymous) return;
                if (!item.navbarMenu?.enabled) return;

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
                if (!item.navbarMenu?.enabled) return;

                renderBuffer.push(
                    <RenderNavbarItemSpan
                        key={item.id}
                        id={item.id}
                        type={item.type}
                        value={item.value}
                        link={item.link}
                        icon={item.icon}
                        enabled={item.enabled}
                        subitems={item.subitems}
                    />
                );
                break;
            }

            case "itempipe": {
                renderBuffer.push(
                    <div key={item.id} className={classes.list_item_pipe}>
                        &nbsp;
                    </div>
                );
                break;
            }
        }
    });

    return <List className={classes.list}>{renderBuffer}</List>;
};
