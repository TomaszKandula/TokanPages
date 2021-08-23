import * as React from "react";
import { Box, Typography } from "@material-ui/core";
import ReactHtmlParser from "react-html-parser";
import { ITextItem } from "../Models/textModel";
import renderTextStyle from "../Styles/renderTextStyle";
import "../../../../Theme/CustomCss/customDropCap.css";

export const RenderText = (props: ITextItem): JSX.Element =>
{
    const classes = renderTextStyle();
    const data: string = props.value as string; 

    const renderTitle = (): JSX.Element => 
    {
        return (
            <Box mt={7}>
                <Typography variant="body1" component="span" className={`${classes.common} ${classes.title}`}>
                    {ReactHtmlParser(data)}
                </Typography>
            </Box>
        );
    };

    const renderSubtitle = (): JSX.Element => 
    {
        return (
            <Box mt={-1} mb={7}>
                <Typography variant="body1" component="span" className={`${classes.common} ${classes.subTitle}`}>
                    {ReactHtmlParser(data)}
                </Typography>
            </Box>      
        );
    };

    const renderHeader = (): JSX.Element => 
    {
        return (
            <Box mt={7} mb={2}>
                <Typography variant="body1" component="span" className={`${classes.common} ${classes.header}`}>
                    {ReactHtmlParser(data)}
                </Typography>
            </Box>
        );
    };

    const renderParagraph = (): JSX.Element => 
    {
        return (
            <Typography variant="body1" component="span" className={`${classes.common} ${classes.paragraph}`}>
                {ReactHtmlParser(data)}
            </Typography>
        );
    };

    const renderParagraphWithDropCap = (): JSX.Element => 
    {
        return (
            <Typography variant="body1" component="span" className={`${classes.common} ${classes.paragraph}`}>
                {ReactHtmlParser(data.replace("<p>", "<p class='custom-drop-cap'>"))}
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
