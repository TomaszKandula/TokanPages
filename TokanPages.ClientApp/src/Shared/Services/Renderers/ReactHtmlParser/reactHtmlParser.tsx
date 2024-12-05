import * as React from "react";
import DOMPurify from "dompurify";

interface Properties {
    html: string;
    allowDataAttr?: boolean;
}

export const ReactHtmlParser = (props: Properties): React.ReactElement => {
    const safeHTML = DOMPurify.sanitize(props.html, {
        ALLOWED_TAGS: ["p", "b", "ul", "ol", "li", "a", "u", "i", "div", "span"],
        FORBID_TAGS: ["style"],
        ALLOW_ARIA_ATTR: false,
        ALLOW_DATA_ATTR: props.allowDataAttr ?? false,
        ADD_ATTR: ["target"],
    });

    return <div dangerouslySetInnerHTML={{ __html: safeHTML }}></div>;
};
