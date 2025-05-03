import * as React from "react";
import { useErrorBoundaryContent } from "../../../Shared/Hooks";

export const ErrorBoundaryView = () => {
    const content = useErrorBoundaryContent();

    return (
        <div id="error">
            <div className="error">
                <h1>{content?.title}</h1>
                <h2>{content?.subtitle}</h2>
                <div className="error-text">
                    <p>{content?.text}</p>
                    <a href={content?.linkHref}>{content?.linkText}</a>
                    <hr />
                    <p>{content?.footer}</p>
                </div>
            </div>
        </div>
    );
};
