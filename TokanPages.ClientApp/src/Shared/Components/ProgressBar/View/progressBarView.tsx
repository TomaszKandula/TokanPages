import * as React from "react";
import "./progressBarView.css";

interface ProgressBarViewProps {
    className?: string;
    size?: number;
    thickness?: number;
    colour?: string;
}

export const ProgressBarView = (props: ProgressBarViewProps): React.ReactElement => {
    const className = !props.className ? "" : props.className;
    const thickness = props.thickness ? props.thickness + "px" : "2px";
    const colour = props.colour ?? "#6367ef";

    return (
        <div className={`progress-bar-box ${className}`}>
            <span
                className="progress-bar-loader"
                style={{
                    height: props.size ?? "40px",
                    width: props.size ?? "40px",
                    borderStyle: "solid",
                    borderWidth: `${thickness}`,
                    borderColor: `${colour}`,
                }}
            ></span>
        </div>
    );
};
