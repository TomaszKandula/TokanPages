import * as React from "react";
import { Box, CircularProgress } from "@material-ui/core";
import { ProgressBarStyle } from "./progressBarStyle";

interface ProgressBarViewProps {
    styleObject?: object;
    size?: number;
}

export const ProgressBarView = (props: ProgressBarViewProps): React.ReactElement => {
    const classes = ProgressBarStyle();
    const externalStyle = props.styleObject !== undefined ? props.styleObject : {};
    return (
        <Box display="flex" alignItems="center" justifyContent="center" style={externalStyle}>
            <Box m="auto">
                <CircularProgress className={classes.main} size={props.size} />
            </Box>
        </Box>
    );
};
