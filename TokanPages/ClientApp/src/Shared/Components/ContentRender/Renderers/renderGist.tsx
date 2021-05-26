import * as React from "react";
import { Card } from "@material-ui/core";
import { ITextItem } from "../Models/textModel";
import SyntaxHighlighter from "react-syntax-highlighter";
import { github } from "react-syntax-highlighter/dist/esm/styles/hljs";
import { GetData } from "../../../../Api/request";
import useStyles from "../Hooks/styleRenderGist";
import validate from "validate.js";

export function RenderGist(props: ITextItem)
{
    const classes = useStyles();
    const gistUrl: string = props.value as string; 
    const [gistContent, setGistContent] = React.useState("");

    const updateContent = React.useCallback(async () => 
    {
        let result = await GetData(gistUrl);
        if (result.status === 200 && validate.isString(result.content)) 
            setGistContent(result.content);
    
    }, [ gistUrl ]);

    React.useEffect(() => { updateContent(); }, [ updateContent ]);

    return(
        <Card elevation={3} classes={{ root: classes.card }}>
            <SyntaxHighlighter 
                className={classes.syntaxHighlighter} 
                style={github} 
                language={props.type} 
                showLineNumbers={true} 
            >
                {gistContent}
            </SyntaxHighlighter>
        </Card>
    );
}
