import * as React from "react";
import { CircularProgress } from "@material-ui/core";
import "./progressBarView.css";

interface ProgressBarViewProps {
    classNameWrapper?: string;
    classNameColour?: string;
    size?: number;
}

export const ProgressBarView = (props: ProgressBarViewProps): React.ReactElement => {
    const className = !props.classNameWrapper ? "" : props.classNameWrapper;
    const colour = !props.classNameColour ? "progress-bar-progress" : props.classNameColour;
    return (
        <div className={`progress-bar-box ${className}`}>
            <div className="m-auto">
                <CircularProgress className={colour} size={props.size} />
            </div>
        </div>
    );
};
