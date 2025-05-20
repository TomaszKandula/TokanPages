import * as React from "react";
import { ItemDto } from "../../../../../Api/Models";
import { RenderSidemenuItem } from "./Renderers";

interface Properties {
    isAnonymous: boolean;
    languageId: string;
    items: ItemDto[] | undefined;
}

export const RenderSideMenu = (props: Properties): React.ReactElement => {
    if (props.items === undefined) return <div>Cannot render content.</div>;
    if (props.items.length === 0) return <div>Cannot render content.</div>;

    props.items.sort((a: ItemDto, b: ItemDto) => {
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
                    <RenderSidemenuItem
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

            default: {
                renderBuffer.push(<React.Fragment key={item.id}></React.Fragment>);
            }
        }
    });

    return (
        <aside className="bulma-menu">
            <ul className="bulma-menu-list">
                {renderBuffer}
            </ul>
        </aside>
    );
};
