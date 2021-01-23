import * as React from "react";
import { ITextObject } from "./Model/textModel";
import { RenderText } from "./Renderers/renderText";
import { RenderImage } from "./Renderers/renderImage";
import { RenderCode } from "./Renderers/renderCode";
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
        if (item.type === "separator") renderBuffer.push(<RenderSeparator key={item.id} />);
        if (item.type === "html") renderBuffer.push(<RenderText key={item.id} id={item.id} type={item.type} value={item.value} />);
        if (item.type === "image") renderBuffer.push(<RenderImage key={item.id} id={item.id} type={item.type} value={item.value} />);
        if (Languages.includes(item.type)) renderBuffer.push(<RenderCode key={item.id} id={item.id} type={item.type} value={item.value} />); 
    });

    return(
        <div data-aos="fade-up">
            {renderBuffer}
        </div>
    );

}
