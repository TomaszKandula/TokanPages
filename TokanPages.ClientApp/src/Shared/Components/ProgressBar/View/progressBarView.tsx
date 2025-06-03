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
    return (
        <div className={`progress-bar-box ${className}`}>
            <span 
                className="progress-bar-loader" 
                style={{ 
                    height: props.size, 
                    width: props.size,
                    borderStyle: "solid",
                    borderWidth: `${props.thickness ?? "2px"}`,
                    borderColor: `${props.colour ?? "#6367ef"}`,
                }}
            ></span>
        </div>
    );
};
