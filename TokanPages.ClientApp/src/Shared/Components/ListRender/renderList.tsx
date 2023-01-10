import * as React from "react";
import List from "@material-ui/core/List";
import { IItem } from "./Models";
import { RenderItem, RenderItemSpan } from "./Renderers";
import { Divider } from "@material-ui/core";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    isAnonymous: boolean;
    items: IItem[] | undefined;
}

export const RenderList = (props: IBinding): JSX.Element =>
{
    if (props.bind.items === undefined) return(<div>Cannot render content.</div>);
    if (props.bind.items.length === 0) return(<div>Cannot render content.</div>);

    let renderBuffer: JSX.Element[] = [];
    props.bind.items.forEach(item => 
    {
        const isAnonymous = props.bind.isAnonymous && (item.link === "/account" || item.link === "/signout");
        const isNotAnonymous = !props.bind.isAnonymous && (item.link === "/signin" || item.link === "/signup");

        switch(item.type)
        {
            case "item":
            {
                if (isAnonymous) return;
                if (isNotAnonymous) return;
            
                renderBuffer.push(<RenderItem 
                    key={item.id}
                    id={item.id} 
                    type={item.type}
                    value={item.value}
                    link={item.link}
                    icon={item.icon}
                    enabled={item.enabled}
                />);
                break;
            }
            
            case "itemspan":
            {
                if (isAnonymous) return;
                if (isNotAnonymous) return;

                renderBuffer.push(<RenderItemSpan 
                    key={item.id}
                    id={item.id} 
                    type={item.type}
                    value={item.value}
                    link={item.link}
                    icon={item.icon}
                    enabled={item.enabled}
                    subitems={item.subitems}
                />);
                break;
            }

            case "divider":
            {
                renderBuffer.push(<Divider 
                    key={item.id} 
                    variant={item.value as "inset" | "middle" | "fullWidth" | undefined} 
                />);
                break;
            }

            default:
            {
                renderBuffer.push(<div key={item.id}>Unknown element.</div>);
            }
        }
    });

    return(<List>{renderBuffer}</List>);
}
