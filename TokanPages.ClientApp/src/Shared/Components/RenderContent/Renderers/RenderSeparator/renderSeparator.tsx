import * as React from "react";
import "./renderSeparator.css";

export const RenderSeparator = (): React.ReactElement => {
    return (
        <div className="render-separator">
            <span className="render-separator-span"></span>
            <span className="render-separator-span"></span>
            <span className="render-separator-span"></span>
        </div>
    );
};
