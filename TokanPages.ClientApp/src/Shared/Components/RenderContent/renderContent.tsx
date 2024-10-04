import * as React from "react";
import { TextObject } from "./Models/TextModel";
import { Languages } from "../../languages";

import {
    RenderSuperTitle,
    RenderText,
    RenderGist,
    RenderTable,
    RenderImage,
    RenderVideo,
    RenderSeparator,
} from "./Renderers";

export const RenderContent = (textObject: TextObject | undefined): React.ReactElement => {
    if (textObject === undefined) return <div>Cannot render content.</div>;
    if (textObject.items.length === 0) return <div>Cannot render content.</div>;

    let renderBuffer: React.ReactElement[] = [];
    textObject.items.forEach(item => {
        switch (item.type) {
            case "separator":
                renderBuffer.push(<RenderSeparator key={item.id} />);
                break;

            case "html":
                renderBuffer.push(
                    <RenderText
                        key={item.id}
                        id={item.id}
                        type={item.type}
                        value={item.value}
                        prop={item.prop}
                        text={item.text}
                    />
                );
                break;

            case "super-title":
                renderBuffer.push(
                    <RenderSuperTitle
                        key={item.id}
                        id={item.id}
                        type={item.type}
                        value={item.value}
                        prop={item.prop}
                        text={item.text}
                        propTitle={item.propTitle}
                        propSubtitle={item.propSubtitle}
                        propImg={item.propImg}
                    />
                );
                break;

            case "image":
                renderBuffer.push(
                    <RenderImage
                        key={item.id}
                        id={item.id}
                        type={item.type}
                        value={item.value}
                        prop={item.prop}
                        text={item.text}
                        constraint={item.constraint}
                    />
                );
                break;

            case "video":
                renderBuffer.push(
                    <RenderVideo
                        key={item.id}
                        id={item.id}
                        type={item.type}
                        value={item.value}
                        prop={item.prop}
                        text={item.text}
                        constraint={item.constraint}
                    />
                );
                break;

            case "table":
                renderBuffer.push(
                    <RenderTable
                        key={item.id}
                        id={item.id}
                        type={item.type}
                        value={item.value}
                        prop={item.prop}
                        text={item.text}
                    />
                );
                break;

            case "gist":
                if (Languages.includes(item.prop))
                    renderBuffer.push(
                        <RenderGist
                            key={item.id}
                            id={item.id}
                            type={item.type}
                            value={item.value}
                            prop={item.prop}
                            text={item.text}
                        />
                    );
                break;

            default:
                renderBuffer.push(<div key={item.id}>Unknown element.</div>);
        }
    });

    return <>{renderBuffer}</>;
};
