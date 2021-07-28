import * as React from "react";
import { IItem } from "./Models/item";
import { RenderSingleItem } from "./Renderers/renderSingleItem";
import { RenderItemWithSubitems } from "./Renderers/renderItemWithSubitems";

export function RenderList(items: IItem[])
{
    if (items === undefined)
    {
        return(<div>Cannot render content.</div>);
    }

    if (items.length === 0)
    {
        return(<></>);
    }

    let renderBuffer: JSX.Element[] = [];
    items.forEach(item => 
    {
        if (item.subitems.length === 0)
        {
            renderBuffer.push(<RenderSingleItem key={item.id} />);
        }
        else
        {
            renderBuffer.push(<RenderItemWithSubitems key={item.id} />);
        }
    });

    return(<>{renderBuffer}</>);
}
