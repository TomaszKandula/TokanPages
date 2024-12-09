import * as React from "react";
import { Link } from "react-router-dom";
import { Typography } from "@material-ui/core";
import { ArrowRight } from "@material-ui/icons";
import { TextItem } from "../../Models/TextModel";
import { useHash } from "../../../../../Shared/Hooks";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";
import { ArticleCard } from "Components/Articles";

interface DataProps {
    value?: string;
    text?: string;
    textId?: string;
}

const NO_CONTENT = "EMPTY_CONTENT_PROVIDED";

const RenderItemLink = (props: DataProps): React.ReactElement => {
    const hash = useHash();
    const data = props.value;
    const onClickHandler = React.useCallback(() => {
        if (data && data !== "") {
            const element = document?.querySelector(data);
            if (element) {
                element.scrollIntoView({ behavior: "smooth" });
                window.history.pushState(null, "", window.location.pathname + data);
            }
        }
    }, [hash, data]);

    return (
        <Typography
            component="span"
            className="render-text-common render-text-paragraph render-text-link"
            onClick={onClickHandler}
        >
            <span className="render-text-wrapper">
                <ArrowRight />
                <ReactHtmlParser html={props.text ?? NO_CONTENT} />
            </span>
        </Typography>
    );
};

const RenderTextLink = (props: DataProps): React.ReactElement => {
    return (
        <Typography component="span" className="render-text-common render-text-paragraph">
            <Link to={props.value ?? ""}>{props.text}</Link>
        </Typography>
    );
}

const RenderRedirectLink = (props: DataProps): React.ReactElement => {
    return <ArticleCard id={props.textId ?? ""} title={props.text ?? ""} description={""} languageIso={""} canAnimate={false} />
}

const RenderTitle = (props: DataProps): React.ReactElement => {
    return (
        <div style={{ marginTop: 56, marginBottom: 0 }}>
            <Typography component="span" className="render-text-common render-text-title">
                <ReactHtmlParser html={props.value ?? NO_CONTENT} />
            </Typography>
        </div>
    );
};

const RenderSubtitle = (props: DataProps): React.ReactElement => {
    return (
        <div style={{ marginTop: 8, marginBottom: 40 }}>
            <Typography component="span" className="render-text-common render-text-sub-title">
                <ReactHtmlParser html={props.value ?? NO_CONTENT} />
            </Typography>
        </div>
    );
};

const RenderHeader = (props: DataProps): React.ReactElement => {
    return (
        <div style={{ marginTop: 56, marginBottom: 16 }}>
            <Typography component="span" className="render-text-common render-text-header">
                <ReactHtmlParser html={props.value ?? NO_CONTENT} />
            </Typography>
        </div>
    );
};

const RenderParagraph = (props: DataProps): React.ReactElement => {
    return (
        <Typography component="span" className="render-text-common render-text-paragraph">
            <ReactHtmlParser html={props.value ?? NO_CONTENT} />
        </Typography>
    );
};

const RenderParagraphWithDropCap = (props: DataProps): React.ReactElement => {
    const replaced = props.value?.replace("<p>", "<p class='custom-drop-cap'>");
    return (
        <Typography component="span" className="render-text-common render-text-paragraph">
            <ReactHtmlParser html={replaced ?? NO_CONTENT} />
        </Typography>
    );
};

export const RenderText = (props: TextItem): React.ReactElement => {
    const value: string = props.value as string;
    switch (props.prop) {
        case "item-link":
            return <RenderItemLink value={value} text={props.text} />;
        case "text-link":
            return <RenderTextLink value={value} text={props.text} />;    
        case "redirect-link":
            return <RenderRedirectLink textId={props.textId} value={value} text={props.text} />;    
        case "title":
            return <RenderTitle value={value} />;
        case "subtitle":
            return <RenderSubtitle value={value} />;
        case "header":
            return <RenderHeader value={value} />;
        case "dropcap":
            return <RenderParagraphWithDropCap value={value} />;
        default:
            return <RenderParagraph value={value} />;
    }
};
