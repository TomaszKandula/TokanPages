import * as React from "react";
import List from "@material-ui/core/List";
import { Item } from "./Models";
import { RenderNavbarItem, RenderNavbarItemSpan } from "./Renderers";

interface Properties {
    isAnonymous: boolean;
    languageId: string;
    items: Item[] | undefined;
}

export const RenderNavbarMenu = (props: Properties): React.ReactElement => {
    if (props.items === undefined) return <div>Cannot render content.</div>;
    if (props.items.length === 0) return <div>Cannot render content.</div>;

    props.items.sort((a: Item, b: Item) => {
        const item1 = a.navbarMenu?.sortOrder ?? 0;
        const item2 = b.navbarMenu?.sortOrder ?? 0;
        const isGreater = item1 > item2 ? 1 : 0;
        return item1 < item2 ? -1 : isGreater;
    });

    const accountPath = `/${props.languageId}/account`;
    const signinPath = `/${props.languageId}/account/signin`;
    const signupPath = `/${props.languageId}/account/signup`;

    const renderBuffer: React.ReactElement[] = [];
    props.items?.forEach(item => {
        const isAnonymous = props.isAnonymous && item.link === accountPath;
        const isNotAnonymous = !props.isAnonymous && (item.link === signinPath || item.link === signupPath);

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
                    <div key={item.id} className="render-menu-list-item-pipe">
                        &nbsp;
                    </div>
                );
                break;
            }
        }
    });

    return <List className="render-menu-list">{renderBuffer}</List>;
};
