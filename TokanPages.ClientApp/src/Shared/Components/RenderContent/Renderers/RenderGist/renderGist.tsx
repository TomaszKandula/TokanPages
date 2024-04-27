import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import SyntaxHighlighter from "react-syntax-highlighter";
import { Card } from "@material-ui/core";
import { TextItem } from "../../Models/TextModel";
import { github } from "react-syntax-highlighter/dist/cjs/styles/hljs";
import { RaiseError } from "../../../../../Shared/Services/ErrorServices";
import { ApplicationState } from "../../../../../Store/Configuration";
import { API_BASE_URI, ExecuteAsync } from "../../../../../Api/Request";
import { RenderGistStyle } from "./renderGistStyle";
import validate from "validate.js";

export const RenderGist = (props: TextItem): JSX.Element => {
    const classes = RenderGistStyle();
    const dispatch = useDispatch();
    const template = useSelector((state: ApplicationState) => state.contentTemplates?.content?.templates);

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
            RaiseError({
                dispatch: dispatch,
                errorObject: result.error,
                content: template.application,
            });
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
