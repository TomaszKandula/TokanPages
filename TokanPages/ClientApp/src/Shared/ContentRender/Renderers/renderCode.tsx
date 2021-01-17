import * as React from "react";
import { ITextItem } from "../Model/textModel";
import SyntaxHighlighter from "react-syntax-highlighter";

export function RenderCode(props: ITextItem)
{
    return(
        <div key={props.id}>
            <SyntaxHighlighter language={props.type}>
                {atob(props.value)}
            </SyntaxHighlighter>
        </div>
    );
}
