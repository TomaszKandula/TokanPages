import * as React from "react";
import { CircularProgress } from "@material-ui/core";

interface ProgressBarViewProps {
    styleObject?: object;
    size?: number;
}

export const ProgressBarView = (props: ProgressBarViewProps): React.ReactElement => {
    const externalStyle = props.styleObject !== undefined ? props.styleObject : {};
    return (
        <div className="progress-bar-box" style={externalStyle}>
            <div style={{ margin: "auto" }}>
                <CircularProgress className="progress-bar-progress" size={props.size} />
            </div>
        </div>
    );
};
