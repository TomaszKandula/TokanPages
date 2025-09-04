import * as React from "react";
import { ItemDto } from "../../../../../Api/Models";
import { ProgressBar } from "../../../../../Shared/Components";
import { RenderNavbarItem } from "./Renderers";

interface RenderNavbarMenuBaseProps {
    isAnonymous: boolean;
    languageId: string;
}

interface RenderNavbarMenuProps extends RenderNavbarMenuBaseProps {
    items: ItemDto[] | undefined;
}

interface CanContinueProps extends RenderNavbarMenuBaseProps {
    item: ItemDto;
}

const CanContinue = (props: CanContinueProps): boolean => {
    const accountPath = `/${props.languageId}/account`;
    const signinPath = `/${props.languageId}/account/signin`;
    const signupPath = `/${props.languageId}/account/signup`;

    const isAnonymous = props.isAnonymous && props.item.link === accountPath;
    const isNotAnonymous = !props.isAnonymous && (props.item.link === signinPath || props.item.link === signupPath);
    const hasNavbar = props.item.navbarMenu?.enabled ?? false;

    if (isAnonymous) return false;
    if (isNotAnonymous) return false;
    if (!hasNavbar) return false;

    return true;
};

export const RenderNavbarMenu = (props: RenderNavbarMenuProps): React.ReactElement => {
    if (props.items === undefined || props.items?.length === 0) {
        return <ProgressBar size={20} />;
    }

    props.items.sort((a: ItemDto, b: ItemDto) => {
        const item1 = a.navbarMenu?.sortOrder ?? 0;
        const item2 = b.navbarMenu?.sortOrder ?? 0;
        const isGreater = item1 > item2 ? 1 : 0;
        return item1 < item2 ? -1 : isGreater;
    });

    const renderBuffer: React.ReactElement[] = [];
    props.items?.forEach(item => {
        switch (item.type) {
            case "item": {
                if (!CanContinue({ ...props, item })) {
                    return;
                }

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
                if (!CanContinue({ ...props, item })) {
                    return;
                }

                renderBuffer.push(
                    <RenderNavbarItem
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
        }
    });

    return <div className="bulma-navbar is-flex is-flex-wrap-wrap">{renderBuffer}</div>;
};
