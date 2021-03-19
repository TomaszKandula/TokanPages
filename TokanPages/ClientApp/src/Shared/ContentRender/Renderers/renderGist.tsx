import * as React from "react";
import { Card } from "@material-ui/core";
import { ITextItem } from "../Model/textModel";
import SyntaxHighlighter from "react-syntax-highlighter";
import { github } from "react-syntax-highlighter/dist/esm/styles/hljs";
import { getDataFromUrl } from "../../../Api/requests";
import useStyles from "../Hooks/styleRenderGist";

export function RenderGist(props: ITextItem)
{
    const classes = useStyles();
    const gistUrl: string = props.value as string; 
    const mountedRef = React.useRef(true);
    const [gistContent, setGistContent] = React.useState("");

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setGistContent(await getDataFromUrl(gistUrl));
    }, [ gistUrl ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, 
    [ updateContent ]);

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
