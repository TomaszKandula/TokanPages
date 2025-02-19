import * as React from "react";
import DOMPurify from "dompurify";

type THeaders = "h1" | "h2" | "h3" | "h4" | "h5" | "h6";
type TComponent = THeaders | "p" | "span" | "div";

interface Properties {
    html: string;
    allowDataAttr?: boolean;
    component?: TComponent;
    className?: string;
}

export const ReactHtmlParser = (props: Properties): React.ReactElement => {
    const safeHTML = DOMPurify.sanitize(props.html, {
        ALLOWED_TAGS: ["p", "b", "ul", "ol", "li", "a", "u", "i", "br", "div", "span"],
        FORBID_TAGS: ["style"],
        ALLOW_ARIA_ATTR: false,
        ALLOW_DATA_ATTR: props.allowDataAttr ?? false,
        ADD_ATTR: ["target"],
    });

    switch (props.component) {
        case "h1":
            return <h1 dangerouslySetInnerHTML={{ __html: safeHTML }} className={props.className}></h1>;
        case "h2":
            return <h2 dangerouslySetInnerHTML={{ __html: safeHTML }} className={props.className}></h2>;
        case "h3":
            return <h3 dangerouslySetInnerHTML={{ __html: safeHTML }} className={props.className}></h3>;
        case "h4":
            return <h4 dangerouslySetInnerHTML={{ __html: safeHTML }} className={props.className}></h4>;
        case "h5":
            return <h5 dangerouslySetInnerHTML={{ __html: safeHTML }} className={props.className}></h5>;
        case "h6":
            return <h6 dangerouslySetInnerHTML={{ __html: safeHTML }} className={props.className}></h6>;
        case "p":
            return <p dangerouslySetInnerHTML={{ __html: safeHTML }} className={props.className}></p>;
        case "span":
            return <span dangerouslySetInnerHTML={{ __html: safeHTML }} className={props.className}></span>;
        case "div":
            return <div dangerouslySetInnerHTML={{ __html: safeHTML }} className={props.className}></div>;
        default:
            return <div dangerouslySetInnerHTML={{ __html: safeHTML }} className={props.className}></div>;
    }
};
