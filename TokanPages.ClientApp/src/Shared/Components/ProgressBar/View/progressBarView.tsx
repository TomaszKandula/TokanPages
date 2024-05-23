import * as React from "react";
import { Box, CircularProgress } from "@material-ui/core";
import { ProgressBarStyle } from "./progressBarStyle";

interface ProgressBarViewProps {
    styleObject?: object;
}

export const ProgressBarView = (props: ProgressBarViewProps): JSX.Element => {
    const classes = ProgressBarStyle();
    const externalStyle = props.styleObject !== undefined ? props.styleObject : {};
    return (
        <Box display="flex" alignItems="center" justifyContent="center" style={externalStyle}>
            <Box m="auto">
                <CircularProgress className={classes.main} />
            </Box>
        </Box>
    );
};
