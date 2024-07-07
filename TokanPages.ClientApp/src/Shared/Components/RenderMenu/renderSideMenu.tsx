import * as React from "react";
import List from "@material-ui/core/List";
import { Item } from "./Models";
import { RenderSidemenuItem, RenderSidemenuItemSpan } from "./Renderers";
import { Divider } from "@material-ui/core";

interface Properties {
    isAnonymous: boolean;
    items: Item[] | undefined;
}

type VariantType = "inset" | "middle" | "fullWidth" | undefined;

export const RenderSideMenu = (props: Properties): JSX.Element => {
    if (props.items === undefined) return <div>Cannot render content.</div>;
    if (props.items.length === 0) return <div>Cannot render content.</div>;

    let renderBuffer: JSX.Element[] = [];
    props.items.forEach(item => {
        const isAnonymous = props.isAnonymous && item.link === "/account";
        const isNotAnonymous = !props.isAnonymous && (item.link === "/signin" || item.link === "/signup");

        switch (item.type) {
            case "item": {
                if (isAnonymous) return;
                if (isNotAnonymous) return;
                if (!item.sideMenuOn) return;

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
                if (!item.sideMenuOn) return;

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
