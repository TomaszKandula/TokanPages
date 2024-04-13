import * as React from "react";
import { useDispatch } from "react-redux";
import { Card } from "@material-ui/core";
import { TextItem } from "../../Models/TextModel";
import SyntaxHighlighter from "react-syntax-highlighter";
import { github } from "react-syntax-highlighter/dist/cjs/styles/hljs";
import { RAISE } from "../../../../../Store/Actions/Application/applicationError";
import { GetErrorMessage } from "../../../../Services/ErrorServices";
import { API_BASE_URI, ExecuteAsync } from "../../../../../Api/Request";
import { RenderGistStyle } from "./renderGistStyle";
import validate from "validate.js";

export const RenderGist = (props: TextItem): JSX.Element => {
    const classes = RenderGistStyle();
    const dispatch = useDispatch();

    let gistUrl: string = props.value as string;
    if (!gistUrl.includes("https://")) {
        gistUrl = `${API_BASE_URI}${gistUrl}`;
    }

    const [gistContent, setGistContent] = React.useState("");

    const updateContent = React.useCallback(async () => {
        let result = await ExecuteAsync({
            url: gistUrl,
            method: "GET",
        });

        if (result.status === 200 && validate.isString(result.content)) {
            setGistContent(result.content);
        }

        if (result.error !== null) {
            dispatch({ type: RAISE, errorObject: GetErrorMessage({ errorObject: result.error }) });
        }
    }, [dispatch, gistUrl]);

    React.useEffect(() => {
        updateContent();
    }, [updateContent]);

    return (
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
};
