import * as React from "react";
import { Box, CircularProgress } from "@material-ui/core";

interface ProgressBarViewProps {
    styleObject?: object;
    size?: number;
}

export const ProgressBarView = (props: ProgressBarViewProps): React.ReactElement => {
    const externalStyle = props.styleObject !== undefined ? props.styleObject : {};
    return (
        <Box className="progress-bar-box" style={externalStyle}>
            <Box m="auto">
                <CircularProgress className="progress-bar-progress" size={props.size} />
            </Box>
        </Box>
    );
};
