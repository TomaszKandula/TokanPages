import * as React from "react";
import { ITextObject } from "./Model/textModel";
import { RenderText } from "./Renderers/renderText";
import { RenderImage } from "./Renderers/renderImage";
import { RenderCode } from "./Renderers/renderCode";
import { Languages } from "../../Shared/languageList";

export function RenderContent(jsonObject: ITextObject | undefined, textStyleName: string)
{

    if (jsonObject === undefined)
    {
        return(<div>Cannot render content.</div>);
    }
    
    let renderBuffer: JSX.Element[] = [];
    jsonObject.items.forEach(item => 
    {
        if (item.type === "html") renderBuffer.push(RenderText(item, textStyleName));
        if (item.type === "image") renderBuffer.push(RenderImage(item));
        if (Languages.includes(item.type)) renderBuffer.push(RenderCode(item)); 
    });

    return(
        <div data-aos="fade-up">
            {renderBuffer}
        </div>
    );

}
