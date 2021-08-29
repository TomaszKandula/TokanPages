import * as React from "react";
import { ITextObject } from "./Models/textModel";
import { RenderText } from "./Renderers/renderText";
import { RenderGist } from "./Renderers/renderGist";
import { RenderTable } from "./Renderers/renderTable";
import { RenderImage } from "./Renderers/renderImage";
import { RenderVideo } from "./Renderers/renderVideo";
import { RenderSeparator } from "./Renderers/renderSeparator";
import { Languages } from "../../languageList";

export const RenderContent = (textObject: ITextObject | undefined): JSX.Element => 
{
    if (textObject === undefined) return(<div>Cannot render content.</div>);
    if (textObject.items.length === 0) return(<div>Cannot render content.</div>);

    let renderBuffer: JSX.Element[] = [];
    textObject.items.forEach(item => 
    {
        switch(item.type)
        {
            case "separator":
                renderBuffer.push(<RenderSeparator 
                    key={item.id} 
                />);
            break;

            case "html":
                renderBuffer.push(<RenderText 
                    key={item.id} 
                    id={item.id} 
                    type={item.type} 
                    value={item.value} 
                    prop={item.prop} 
                    text={item.text} 
                />);
            break;

            case "image": 
                renderBuffer.push(<RenderImage 
                    key={item.id} 
                    id={item.id} 
                    type={item.type} 
                    value={item.value} 
                    prop={item.prop} 
                    text={item.text} 
                />);
            break;

            case "video":
                renderBuffer.push(<RenderVideo 
                    key={item.id} 
                    id={item.id} 
                    type={item.type} 
                    value={item.value} 
                    prop={item.prop} 
                    text={item.text} 
                />);
            break;

            case "table": 
                renderBuffer.push(<RenderTable 
                    key={item.id} 
                    id={item.id} 
                    type={item.type} 
                    value={item.value} 
                    prop={item.prop} 
                    text={item.text} 
                />);
            break;

            case "gist":
                if (Languages.includes(item.type)) renderBuffer.push(
                    <RenderGist 
                        key={item.id} 
                        id={item.id} 
                        type={item.type} 
                        value={item.value} 
                        prop={item.prop} 
                        text={item.text} 
                    />);
            break;

            default: renderBuffer.push(<div>Unknown element.</div>);
        }
    });

    return(<>{renderBuffer}</>);
}
