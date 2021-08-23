import * as React from "react";
import { useDispatch } from "react-redux";
import * as Sentry from "@sentry/react";
import { Card } from "@material-ui/core";
import { ITextItem } from "../Models/textModel";
import SyntaxHighlighter from "react-syntax-highlighter";
import { github } from "react-syntax-highlighter/dist/cjs/styles/hljs";
import { RAISE_ERROR } from "../../../../Redux/Actions/raiseErrorAction";
import { GetErrorMessage } from "../../../../Shared/helpers";
import { GetData } from "../../../../Api/request";
import renderGistStyle from "../Styles/renderGistStyle";
import validate from "validate.js";

export const RenderGist = (props: ITextItem): JSX.Element =>
{
    const classes = renderGistStyle();
    const dispatch = useDispatch();
    
    const gistUrl: string = props.value as string; 
    const [gistContent, setGistContent] = React.useState("");

    const updateContent = React.useCallback(async () => 
    {
        let result = await GetData(gistUrl);
        
        if (result.status === 200 && validate.isString(result.content)) 
        {
            setGistContent(result.content);
        }
           
        if (result.error !== null)
        {
            dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage(result.error) });
            Sentry.captureException(result.error);
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
