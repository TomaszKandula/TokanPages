import * as React from "react";
import { TextItem } from "../../Models/TextModel";
import {
    RenderAnchorLink,
    RenderArticleLink,
    RenderExternalLink,
    RenderHeader1,
    RenderHeader2,
    RenderInternalLink,
    RenderParagraph,
    RenderParagraphWithDropCap,
    RenderSubtitle,
    RenderTargetLink,
    RenderTitle,
} from "./Helpers";
import "./renderText.css";

export const RenderText = (props: TextItem): React.ReactElement => {
    const value: string = props.value as string;
    switch (props.prop) {
        case "anchor-link":
            return <RenderAnchorLink value={value} text={props.text} />;
        case "target-link":
            return <RenderTargetLink value={value} text={props.text} />;
        case "internal-link":
            return <RenderInternalLink {...props} />;
        case "external-link":
            return <RenderExternalLink {...props} />;
        case "article-link":
            return <RenderArticleLink value={value} text={props.text} />;
        case "title":
            return <RenderTitle value={value} />;
        case "subtitle":
            return <RenderSubtitle value={value} />;
        case "header1":
            return <RenderHeader1 value={value} />;
        case "header2":
            return <RenderHeader2 value={value} />;
        case "dropcap":
            return <RenderParagraphWithDropCap value={value} />;
        default:
            return <RenderParagraph {...props} />;
    }
};
