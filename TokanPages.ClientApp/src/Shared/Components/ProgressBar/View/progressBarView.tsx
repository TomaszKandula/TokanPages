import * as React from "react";
import { CircularProgress } from "@material-ui/core";

interface ProgressBarViewProps {
    style?: React.CSSProperties;
    size?: number;
}

export const ProgressBarView = (props: ProgressBarViewProps): React.ReactElement => {
    const externalStyle = props.style !== undefined ? props.style : {};
    return (
        <div className="progress-bar-box" style={externalStyle}>
            <div style={{ margin: "auto" }}>
                <CircularProgress className="progress-bar-progress" size={props.size} />
            </div>
        </div>
    );
};
