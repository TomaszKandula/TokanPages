import * as React from "react";
import List from "@material-ui/core/List";
import { IItem } from "./Models/item";
import { RenderItem } from "./Renderers/renderItem";
import { RenderItemSpan } from "./Renderers/renderItemSpan";
import { Divider } from "@material-ui/core";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    isAnonymous: boolean;
    items: IItem[];
}

export function RenderList(props: IBinding)
{
    if (props.bind.items === undefined)
    {
        return(<div>Cannot render content.</div>);
    }

    if (props.bind.items.length === 0)
    {
        return(<></>);
    }

    let renderBuffer: JSX.Element[] = [];
    props.bind.items.forEach(item => 
    {
        if (item.type === "item")
        {
            if (props.bind.isAnonymous && item.value === "Account") return;
            if (!props.bind.isAnonymous && (item.value === "Login" || item.value === "Register")) return;
            
            renderBuffer.push(<RenderItem 
                key={item.id}
                id={item.id} 
                type={item.type}
                value={item.value}
                link={item.link}
                icon={item.icon}
                enabled={item.enabled}
            />);
        }

        if (item.type === "itemspan") renderBuffer.push(
            <RenderItemSpan 
                key={item.id}
                id={item.id} 
                type={item.type}
                value={item.value}
                link={item.link}
                icon={item.icon}
                enabled={item.enabled}
                subitems={item.subitems}
            />);

        if (item.type === "divider" 
            && (item.value === "middle" 
            || item.value === "inset" 
            || item.value === "fullWidth")) renderBuffer.push(
            <Divider key={item.id} variant={item.value} />
        );
    });

    return(<List>{renderBuffer}</List>);
}
