import * as React from "react";
import List from "@material-ui/core/List";
import { IItem } from "./Models/item";
import { RenderSingleItem } from "./Renderers/renderSingleItem";
import { RenderItemWithSubitems } from "./Renderers/renderItemWithSubitems";

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
        if (item.subitems.length === 0)
        {
            if (props.bind.isAnonymous && item.name === "Account") return;
            if (!props.bind.isAnonymous && (item.name === "Login" || item.name === "Register")) return;
            
            renderBuffer.push(<RenderSingleItem 
                key={item.id}
                id={item.id} 
                name={item.name}
                link={item.link}
                icon={item.icon}
                enabled={item.enabled}
                subitems={item.subitems}
            />);
        }
        else
        {
            renderBuffer.push(<RenderItemWithSubitems 
                key={item.id}
                id={item.id} 
                name={item.name}
                link={item.link}
                icon={item.icon}
                enabled={item.enabled}
                subitems={item.subitems}
            />);
        }
    });

    return(<List>{renderBuffer}</List>);
}
