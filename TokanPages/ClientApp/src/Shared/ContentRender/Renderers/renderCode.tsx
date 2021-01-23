import * as React from "react";
import { Card } from "@material-ui/core";
import { ITextItem } from "../Model/textModel";
import SyntaxHighlighter from "react-syntax-highlighter";
import useStyles from "../Hooks/styleRenderCode";

export function RenderCode(props: ITextItem)
{
    const classes = useStyles();
    return(
        <Card elevation={3} classes={{ root: classes.card }}>
            <SyntaxHighlighter className={classes.syntaxHighlighter} language={props.type} showLineNumbers={true}>
                {atob(props.value)}
            </SyntaxHighlighter>
        </Card>
    );
}
