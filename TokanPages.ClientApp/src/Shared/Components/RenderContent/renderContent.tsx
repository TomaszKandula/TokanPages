import * as React from "react";
import { useDimensions } from "../../../Shared/Hooks";
import { TextObject } from "./Models/TextModel";
import { Languages } from "../../Languages";
import { ProgressBar } from "..";
import {
    RenderSuperTitle,
    RenderText,
    RenderGist,
    RenderTable,
    RenderImage,
    RenderVideo,
    RenderSeparator,
    RenderImages,
    RenderVideos,
} from "./Renderers";

export const RenderContent = (textObject: TextObject | undefined): React.ReactElement => {
    const media = useDimensions();

    if (textObject === undefined || textObject?.items?.length === 0) {
        return <ProgressBar />;
    }

    const renderBuffer: React.ReactElement[] = [];
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
                        loading={item.loading}
                        propTitle={item.propTitle}
                        propSubtitle={item.propSubtitle}
                        propImg={item.propImg}
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
                        loading={item.loading}
                        propTitle={item.propTitle}
                        propSubtitle={item.propSubtitle}
                        propImg={item.propImg}
                        constraint={item.constraint}
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
                        loading={item.loading}
                        constraint={item.constraint}
                    />
                );
                break;

            case "images":
                renderBuffer.push(
                    <RenderImages
                        key={item.id}
                        id={item.id}
                        type={item.type}
                        value={item.value}
                        prop={item.prop}
                        text={item.text}
                        loading={item.loading}
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
                        loading={item.loading}
                        constraint={item.constraint}
                    />
                );
                break;

            case "videos":
                renderBuffer.push(
                    <RenderVideos
                        key={item.id}
                        id={item.id}
                        type={item.type}
                        value={item.value}
                        prop={item.prop}
                        text={item.text}
                        loading={item.loading}
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

    return (
        <div data-testid="render-content" className={`bulma-content m-0 p-0 ${media.isMobile ? "mx-4" : ""}`}>
            {renderBuffer}
        </div>
    );
};
