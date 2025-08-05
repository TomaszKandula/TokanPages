import React from "react";
import DOMPurify from "dompurify";
import { ProcessTextProps, RenderOutputProps } from "./types";
import { configuration } from "./configuration";
import { BREAKING_WHITESPACE, NON_BREAKING_WHITESPACE } from "./constants";

const RenderOutput = (props: RenderOutputProps): React.ReactElement => {
    switch(props.tag) {
        case "p":
            return <p data-testid={props.testId} className={props.className} style={props.style} dangerouslySetInnerHTML={{ __html: props.text }}></p>;
        case "span":
            return <span data-testid={props.testId} className={props.className} style={props.style} dangerouslySetInnerHTML={{ __html: props.text }}></span>;
        default:
            return <div data-testid={props.testId} className={props.className} style={props.style} dangerouslySetInnerHTML={{ __html: props.text }}></div>
    }
}

export const RenderHtml = (props: ProcessTextProps): React.ReactElement => {
    if (props.value === "") {
        return  <RenderOutput {...props} text={props.value} />;
    }

    const data = props.value.split(BREAKING_WHITESPACE);
    if (data.length < 2) {
        return  <RenderOutput {...props} text={props.value} />;
    }

    let text = "";
    for(let index = 0; index < data.length; index++) {
        const word = data[index];
        if (data.length === index + 1) {
            text += word;
        } else {
            if (word.length === 1) {
                text += word + NON_BREAKING_WHITESPACE;
            } else {
                text += word + BREAKING_WHITESPACE;
            }
        }
    }

    const sanitized = DOMPurify.sanitize(text, configuration);
    return  <RenderOutput {...props} text={sanitized} />;
}
