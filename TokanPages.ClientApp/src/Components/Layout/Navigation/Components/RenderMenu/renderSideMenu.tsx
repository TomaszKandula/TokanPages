import * as React from "react";
import { ItemDto } from "../../../../../Api/Models";
import { ProgressBar } from "../../../../../Shared/Components";
import { RenderSidemenuItem } from "./Renderers";
import { v4 as uuidv4 } from "uuid";

interface RenderSideMenuBaseProps {
    isAnonymous: boolean;
    languageId: string;
}

interface RenderSideMenuProps extends RenderSideMenuBaseProps {
    items: ItemDto[] | undefined;
}

interface CanContinueProps extends RenderSideMenuBaseProps {
    item: ItemDto;
}

const CanContinue = (props: CanContinueProps): boolean => {
    const accountPath = "account";
    const signinPath = "signin";
    const signupPath = "signup";

    const hasAccountGroup = props.item.link?.includes(accountPath);
    const hasNoSigningLinks = !props.item.link?.includes(signinPath) && !props.item.link?.includes(signupPath);
    const hasSigningLinks = props.item.link?.includes(signinPath) || props.item.link?.includes(signupPath);
    const isAnonymous = props.isAnonymous && hasAccountGroup && hasNoSigningLinks;
    const isNotAnonymous = !props.isAnonymous && hasSigningLinks;
    const hasSideMenu = props.item.sideMenu?.enabled ?? false;

    if (isAnonymous) return false;
    if (isNotAnonymous) return false;
    if (!hasSideMenu) return false;

    return true;
};

export const RenderSideMenu = (props: RenderSideMenuProps): React.ReactElement => {
    if (props.items === undefined || props.items?.length === 0) {
        return <ProgressBar size={20} />;
    }

    props.items.sort((a: ItemDto, b: ItemDto) => {
        const item1 = a.sideMenu?.sortOrder ?? 0;
        const item2 = b.sideMenu?.sortOrder ?? 0;
        const isGreater = item1 > item2 ? 1 : 0;
        return item1 < item2 ? -1 : isGreater;
    });

    const renderBuffer: React.ReactElement[] = [];
    props.items.forEach(item => {
        switch (item.type) {
            case "item": {
                if (!CanContinue({ ...props, item })) {
                    return;
                }

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
                if (!CanContinue({ ...props, item })) {
                    return;
                }

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

            case "group": {
                renderBuffer.push(
                    <React.Fragment key={uuidv4()}>
                        <p className="bulma-menu-label">
                            {item.value}
                        </p>
                        <hr className="m-0 mb-2" />
                    </React.Fragment>
                );
                break;
            }

            default: {
                renderBuffer.push(<React.Fragment key={item.id}></React.Fragment>);
            }
        }
    });

    return (
        <aside className="bulma-menu p-3 has-background-white">
            <ul className="bulma-menu-list">{renderBuffer}</ul>
        </aside>
    );
};
