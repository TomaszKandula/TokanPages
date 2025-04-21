import * as React from "react";
import { useErrorBoundaryContent } from "../../../Shared/Hooks";

interface ErrorBoundaryViewProps {
    title?: string;
    subtitle?: string;
    text?: string;
    linkHref?: string;
    linkText?: string;
    footer?: string;
}

const defaultContent: ErrorBoundaryViewProps = {
    title: "Critical Error",
    subtitle: "Something went wrong...",
    text: "Contact the site's administrator or support for assistance.",
    linkHref: "mailto:admin@tomkandula.com",
    linkText: "IT support",
    footer: "tomkandula.com",
};

export const ErrorBoundaryView = () => {
    const content = useErrorBoundaryContent();

    const title = content?.title ?? defaultContent.title;
    const subtitle = content?.subtitle ?? defaultContent.subtitle;
    const text = content?.text ?? defaultContent.text;
    const href = content?.linkHref ?? defaultContent.linkHref;
    const link = content?.linkText ?? defaultContent.linkText;
    const footer = content?.footer ?? defaultContent.footer;

    return (
        <div id="error">
            <div className="error">
                <h1>{title}</h1>
                <h2>{subtitle}</h2>
                <div className="error-text">
                    <p>{text}</p>
                    <a href={href}>{link}</a>
                    <hr />
                    <p>{footer}</p>
                </div>
            </div>
        </div>
    );
};
