import * as React from "react";
import { Box, Typography } from "@material-ui/core";
import { TextItem } from "../../Models/TextModel";
import { RenderTextStyle } from "./renderTextStyle";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";
import "../../../../../Theme/Css/customDropCap.css";

interface DataProps { 
    data: string 
}

const RenderTitle = (props: DataProps): JSX.Element => {
    const classes = RenderTextStyle();
    return (
        <Box mt={7} mb={0}>
            <Typography variant="body1" component="span" className={`${classes.common} ${classes.title}`}>
                <ReactHtmlParser html={props.data} />
            </Typography>
        </Box>
    );
};

const RenderSubtitle = (props: DataProps): JSX.Element => {
    const classes = RenderTextStyle();
    return (
        <Box mt={1} mb={5}>
            <Typography variant="body1" component="span" className={`${classes.common} ${classes.subTitle}`}>
                <ReactHtmlParser html={props.data} />
            </Typography>
        </Box>
    );
};

const RenderHeader = (props: DataProps): JSX.Element => {
    const classes = RenderTextStyle();
    return (
        <Box mt={7} mb={2}>
            <Typography variant="body1" component="span" className={`${classes.common} ${classes.header}`}>
                <ReactHtmlParser html={props.data} />
            </Typography>
        </Box>
    );
};

const RenderParagraph = (props: DataProps): JSX.Element => {
    const classes = RenderTextStyle();
    return (
        <Typography variant="body1" component="span" className={`${classes.common} ${classes.paragraph}`}>
            <ReactHtmlParser html={props.data} />
        </Typography>
    );
};

const RenderParagraphWithDropCap = (props: DataProps): JSX.Element => {
    const classes = RenderTextStyle();
    const replaced = props.data.replace("<p>", "<p class='custom-drop-cap'>");
    return (
        <Typography variant="body1" component="span" className={`${classes.common} ${classes.paragraph}`}>
            <ReactHtmlParser html={replaced} />
        </Typography>
    );
};

export const RenderText = (props: TextItem): JSX.Element => {
    const data: string = props.value as string;

    switch (props.prop) {
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
