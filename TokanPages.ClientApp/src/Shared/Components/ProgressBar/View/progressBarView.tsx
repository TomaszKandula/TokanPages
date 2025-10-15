import * as React from "react";
import { ProgressBarViewProps } from "../Types";
import "./progressBarView.css";

export const ProgressBarView = (props: ProgressBarViewProps): React.ReactElement => {
    const className = !props.className ? "" : props.className;
    const thickness = props.thickness ? props.thickness + "px" : "2px";
    const colour = props.colour ?? "#6367ef";

    return (
        <div data-testid="progress-bar-view" className={`progress-bar-box ${className}`}>
            <span
                className="progress-bar-loader"
                style={{
                    height: props.size ?? 40,
                    width: props.size ?? 40,
                    borderStyle: "solid",
                    borderWidth: `${thickness}`,
                    borderColor: `${colour}`,
                }}
            ></span>
        </div>
    );
};
