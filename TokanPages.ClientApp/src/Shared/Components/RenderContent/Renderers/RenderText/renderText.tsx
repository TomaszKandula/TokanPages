import * as React from "react";
import { Typography } from "@material-ui/core";
import { ArrowRight } from "@material-ui/icons";
import { TextItem } from "../../Models/TextModel";
import { useHash } from "../../../../../Shared/Hooks";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";

interface DataProps {
    data?: string;
    text?: string;
}

const NO_CONTENT = "EMPTY_CONTENT_PROVIDED";

const RenderItemLink = (props: DataProps): React.ReactElement => {
    const hash = useHash();
    const data = props.data;
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

const RenderTitle = (props: DataProps): React.ReactElement => {
    return (
        <div style={{ marginTop: 56, marginBottom: 0 }}>
            <Typography component="span" className="render-text-common render-text-title">
                <ReactHtmlParser html={props.data ?? NO_CONTENT} />
            </Typography>
        </div>
    );
};

const RenderSubtitle = (props: DataProps): React.ReactElement => {
    return (
        <div style={{ marginTop: 8, marginBottom: 40 }}>
            <Typography component="span" className="render-text-common render-text-sub-title">
                <ReactHtmlParser html={props.data ?? NO_CONTENT} />
            </Typography>
        </div>
    );
};

const RenderHeader = (props: DataProps): React.ReactElement => {
    return (
        <div style={{ marginTop: 56, marginBottom: 16 }}>
            <Typography component="span" className="render-text-common render-text-header">
                <ReactHtmlParser html={props.data ?? NO_CONTENT} />
            </Typography>
        </div>
    );
};

const RenderParagraph = (props: DataProps): React.ReactElement => {
    return (
        <Typography component="span" className="render-text-common render-text-paragraph">
            <ReactHtmlParser html={props.data ?? NO_CONTENT} />
        </Typography>
    );
};

const RenderParagraphWithDropCap = (props: DataProps): React.ReactElement => {
    const replaced = props.data?.replace("<p>", "<p class='custom-drop-cap'>");
    return (
        <Typography component="span" className="render-text-common render-text-paragraph">
            <ReactHtmlParser html={replaced ?? NO_CONTENT} />
        </Typography>
    );
};

export const RenderText = (props: TextItem): React.ReactElement => {
    const data: string = props.value as string;
    switch (props.prop) {
        case "item-link":
            return <RenderItemLink data={data} text={props.text} />;
        case "title":
            return <RenderTitle data={data} />;
        case "subtitle":
            return <RenderSubtitle data={data} />;
        case "header":
            return <RenderHeader data={data} />;
        case "dropcap":
            return <RenderParagraphWithDropCap data={data} />;
        default:
            return <RenderParagraph data={data} />;
    }
};
