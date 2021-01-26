import * as React from "react";
import { ITextObject } from "./Model/textModel";
import { RenderText } from "./Renderers/renderText";
import { RenderCode } from "./Renderers/renderCode";
import { RenderImage } from "./Renderers/renderImage";
import { RenderVideo } from "./Renderers/renderVideo";
import { RenderSeparator } from "./Renderers/renderSeparator";
import { Languages } from "../../Shared/languageList";

export function RenderContent(jsonObject: ITextObject | undefined)
{
    if (jsonObject === undefined)
    {
        return(<div>Cannot render content.</div>);
    }
    
    let renderBuffer: JSX.Element[] = [];
    jsonObject.items.forEach(item => 
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

        if (Languages.includes(item.type)) renderBuffer.push(
            <RenderCode 
                key={item.id} 
                id={item.id} 
                type={item.type} 
                value={item.value} 
                prop={item.prop} 
                text={item.text} 
            />); 
    });

    return(
        <div data-aos="fade-up">
            {renderBuffer}
        </div>
    );
}
