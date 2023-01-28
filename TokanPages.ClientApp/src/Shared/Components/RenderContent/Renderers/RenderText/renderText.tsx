import * as React from "react";
import { Box, Typography } from "@material-ui/core";
import { ITextItem } from "../../Models/TextModel";
import { RenderTextStyle } from "./renderTextStyle";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";
import "../../../../../Theme/Css/customDropCap.css";

export const RenderText = (props: ITextItem): JSX.Element =>
{
    const classes = RenderTextStyle();
    const data: string = props.value as string; 

    const renderTitle = (): JSX.Element => 
    {
        return (
            <Box mt={7}>
                <Typography variant="body1" component="span" className={`${classes.common} ${classes.title}`}>
                    {ReactHtmlParser({ html: data })}
                </Typography>
            </Box>
        );
    };

    const renderSubtitle = (): JSX.Element => 
    {
        return (
            <Box mt={-1} mb={7}>
                <Typography variant="body1" component="span" className={`${classes.common} ${classes.subTitle}`}>
                    {ReactHtmlParser({ html: data })}
                </Typography>
            </Box>      
        );
    };

    const renderHeader = (): JSX.Element => 
    {
        return (
            <Box mt={7} mb={2}>
                <Typography variant="body1" component="span" className={`${classes.common} ${classes.header}`}>
                    {ReactHtmlParser({ html: data })}
                </Typography>
            </Box>
        );
    };

    const renderParagraph = (): JSX.Element => 
    {
        return (
            <Typography variant="body1" component="span" className={`${classes.common} ${classes.paragraph}`}>
                {ReactHtmlParser({ html: data })}
            </Typography>
        );
    };

    const renderParagraphWithDropCap = (): JSX.Element => 
    {
        const replaced = data.replace("<p>", "<p class='custom-drop-cap'>");
        return (
            <Typography variant="body1" component="span" className={`${classes.common} ${classes.paragraph}`}>
                {ReactHtmlParser({ html: replaced })}
            </Typography>
        );
    };

    switch(props.prop)
    {
        case "title": return renderTitle();
        case "subtitle": return renderSubtitle();
        case "header": return renderHeader();
        case "dropcap": return renderParagraphWithDropCap();
        default: return renderParagraph();
    }
}
