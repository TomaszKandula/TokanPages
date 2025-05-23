import * as React from "react";
import List from "@material-ui/core/List";
import { Item } from "./Models";
import { RenderSidemenuItem, RenderSidemenuItemSpan } from "./Renderers";
import { Divider } from "@material-ui/core";

interface Properties {
    isAnonymous: boolean;
    languageId: string;
    items: Item[] | undefined;
}

type VariantType = "inset" | "middle" | "fullWidth" | undefined;

export const RenderSideMenu = (props: Properties): React.ReactElement => {
    if (props.items === undefined) return <div>Cannot render content.</div>;
    if (props.items.length === 0) return <div>Cannot render content.</div>;

    props.items.sort((a: Item, b: Item) => {
        const item1 = a.sideMenu?.sortOrder ?? 0;
        const item2 = b.sideMenu?.sortOrder ?? 0;
        const isGreater = item1 > item2 ? 1 : 0;
        return item1 < item2 ? -1 : isGreater;
    });

    const accountPath = `/${props.languageId}/account`;
    const signinPath = `/${props.languageId}/account/signin`;
    const signupPath = `/${props.languageId}/account/signup`;

    const renderBuffer: React.ReactElement[] = [];
    props.items.forEach(item => {
        const isAnonymous = props.isAnonymous && item.link === accountPath;
        const isNotAnonymous = !props.isAnonymous && (item.link === signinPath || item.link === signupPath);

        switch (item.type) {
            case "item": {
                if (isAnonymous) return;
                if (isNotAnonymous) return;
                if (!item.sideMenu?.enabled) return;

                renderBuffer.push(
                    <RenderSidemenuItem
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
                if (!item.sideMenu?.enabled) return;

                renderBuffer.push(
                    <RenderSidemenuItemSpan
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
                renderBuffer.push(<div key={item.id}></div>);
                break;
            }

            case "divider": {
                renderBuffer.push(<Divider key={item.id} variant={item.value as VariantType} />);
                break;
            }

            default: {
                renderBuffer.push(<div key={item.id}>Unknown element.</div>);
            }
        }
    });

    return <List>{renderBuffer}</List>;
};
