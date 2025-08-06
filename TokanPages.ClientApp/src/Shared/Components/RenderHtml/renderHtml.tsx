import React from "react";
import DOMPurify from "dompurify";
import { ProcessTextProps, RenderOutputProps } from "./types";
import { configuration } from "./configuration";
import { BREAKING_WHITESPACE, NON_BREAKING_WHITESPACE } from "./constants";

const RenderOutput = (props: RenderOutputProps): React.ReactElement => {
    const html = { __html: props.text };
    const attributes = {
        className: props.className,
        style: props.style,
        dangerouslySetInnerHTML: html,
    };

    switch (props.tag) {
        case "p":
            return <p data-testid={props.testId} {...attributes}></p>;
        case "span":
            return <span data-testid={props.testId} {...attributes}></span>;
        case "h1":
            return <h1 data-testid={props.testId} {...attributes}></h1>;
        case "h2":
            return <h2 data-testid={props.testId} {...attributes}></h2>;
        case "h3":
            return <h3 data-testid={props.testId} {...attributes}></h3>;
        case "h4":
            return <h4 data-testid={props.testId} {...attributes}></h4>;
        case "h5":
            return <h5 data-testid={props.testId} {...attributes}></h5>;
        case "h6":
            return <h6 data-testid={props.testId} {...attributes}></h6>;
        case "blockquote":
            return <blockquote data-testid={props.testId} {...attributes}></blockquote>;
        case "li":
            return <li data-testid={props.testId} {...attributes}></li>;
        default:
            return <div data-testid={props.testId} {...attributes}></div>;
    }
};

export const RenderHtml = (props: ProcessTextProps): React.ReactElement => {
    if (props.value === "") {
        return <RenderOutput {...props} text={props.value} />;
    }

    const data = props.value.split(BREAKING_WHITESPACE);
    if (data.length < 2) {
        return <RenderOutput {...props} text={props.value} />;
    }

    let text = "";
    for (let index = 0; index < data.length; index++) {
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
    return <RenderOutput {...props} text={sanitized} />;
};
