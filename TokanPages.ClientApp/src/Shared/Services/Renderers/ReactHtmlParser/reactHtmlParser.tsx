import * as React from "react";
import DOMPurify from "dompurify";

interface Properties
{
    html: string;
}

export const ReactHtmlParser = (props: Properties): JSX.Element => 
{
    const safeHTML = DOMPurify.sanitize(props.html, { 
        ALLOWED_TAGS: ["p", "b", "ul", "li", "a", "u", "i"],
        FORBID_TAGS: ["style"],
        ALLOW_ARIA_ATTR: false,
        ALLOW_DATA_ATTR: false
    });    

    return(<div dangerouslySetInnerHTML={{ __html: safeHTML }}></div>);
}
