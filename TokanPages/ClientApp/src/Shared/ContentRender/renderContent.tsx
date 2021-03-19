import * as React from "react";
import { ITextObject } from "./Model/textModel";
import { RenderText } from "./Renderers/renderText";
import { RenderGist } from "./Renderers/renderGist";
import { RenderTable } from "./Renderers/renderTable";
import { RenderImage } from "./Renderers/renderImage";
import { RenderVideo } from "./Renderers/renderVideo";
import { RenderSeparator } from "./Renderers/renderSeparator";
import { Languages } from "../../Shared/languageList";

export function RenderContent(textObject: ITextObject | undefined)
{
    if (textObject === undefined)
    {
        return(<div>Cannot render content.</div>);
    }

    if (textObject.items.length === 0)
    {
        return(<></>);
    }

    let renderBuffer: JSX.Element[] = [];
    textObject.items.forEach(item => 
    {
        if (item.type === "separator") renderBuffer.push(
            <RenderSeparator 
                key={item.id} 
            />);

        if (item.type === "html") renderBuffer.push(
            <RenderText 
                key={item.id} 
                id={item.id} 
                type={item.type} 
                value={item.value} 
                prop={item.prop} 
                text={item.text} 
            />);

        if (item.type === "image") renderBuffer.push(
            <RenderImage 
                key={item.id} 
                id={item.id} 
                type={item.type} 
                value={item.value} 
                prop={item.prop} 
                text={item.text} 
            />);

        if (item.type === "video") renderBuffer.push(
            <RenderVideo 
                key={item.id} 
                id={item.id} 
                type={item.type} 
                value={item.value} 
                prop={item.prop} 
                text={item.text} 
            />);

        if (item.type === "table") renderBuffer.push(
            <RenderTable 
                key={item.id} 
                id={item.id} 
                type={item.type} 
                value={item.value} 
                prop={item.prop} 
                text={item.text} 
            />);

        if (item.prop === "gist" && Languages.includes(item.type)) renderBuffer.push(
            <RenderGist 
                key={item.id} 
                id={item.id} 
                type={item.type} 
                value={item.value} 
                prop={item.prop} 
                text={item.text} 
            />);
    });

    return(<>{renderBuffer}</>);
}
