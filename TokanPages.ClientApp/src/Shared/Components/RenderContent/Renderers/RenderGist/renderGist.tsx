import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import SyntaxHighlighter from "react-syntax-highlighter";
import { TextItem } from "../../Models/TextModel";
import { github } from "react-syntax-highlighter/dist/cjs/styles/hljs";
import { RaiseError } from "../../../../../Shared/Services/ErrorServices";
import { useApiAction } from "../../../../../Shared/Hooks";
import { ApplicationState } from "../../../../../Store/Configuration";
import { API_BASE_URI } from "../../../../../Api";
import validate from "validate.js";
import "./renderGist.css";

export const RenderGist = (props: TextItem): React.ReactElement => {
    const dispatch = useDispatch();
    const actions = useApiAction();
    const template = useSelector((state: ApplicationState) => state.contentPageData.components.templates.templates);

    let gistUrl: string = props.value as string;
    if (!gistUrl.includes("https://")) {
        gistUrl = `${API_BASE_URI}${gistUrl}`;
    }

    const [gistContent, setGistContent] = React.useState("");

    const updateContent = React.useCallback(async () => {
        const result = await actions.apiAction({
            url: gistUrl,
            configuration: {
                method: "GET",
                hasJsonResponse: false,
            },
        });

        if (result.status === 200 && validate.isString(result.content)) {
            setGistContent(result.content as string);
        }

        if (result.error) {
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
        <div className="bulma-card">
            <div className="bulma-card-content">
                <SyntaxHighlighter
                    className="render-gist-syntax-highlighter"
                    style={github}
                    language={props.type}
                    showLineNumbers={true}
                >
                    {gistContent}
                </SyntaxHighlighter>
            </div>
        </div>
    );
};
