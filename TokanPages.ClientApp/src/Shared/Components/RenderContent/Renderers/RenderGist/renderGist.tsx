import * as React from "react";
import { useDispatch } from "react-redux";
import { Card } from "@material-ui/core";
import { ITextItem } from "../../Models/TextModel";
import SyntaxHighlighter from "react-syntax-highlighter";
import { github } from "react-syntax-highlighter/dist/cjs/styles/hljs";
import { RAISE_ERROR } from "../../../../../Store/Actions/raiseErrorAction";
import { GetErrorMessage } from "../../../../Services/ErrorServices";
import { ApiCall } from "../../../../../Api/Request";
import { RenderGistStyle } from "./renderGistStyle";
import validate from "validate.js";

export const RenderGist = (props: ITextItem): JSX.Element =>
{
    const classes = RenderGistStyle();
    const dispatch = useDispatch();
    
    const gistUrl: string = props.value as string; 
    const [gistContent, setGistContent] = React.useState("");

    const updateContent = React.useCallback(async () => 
    {
        let result = await ApiCall(
        {
            url: gistUrl,
            method: "GET"
        });
        
        if (result.status === 200 && validate.isString(result.content)) 
        {
            setGistContent(result.content);
        }
           
        if (result.error !== null)
        {
            dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage({ errorObject: result.error }) });
        }
 
    }, [ dispatch, gistUrl ]);

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
