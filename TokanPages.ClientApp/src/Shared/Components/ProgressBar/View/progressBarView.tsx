import * as React from "react";
import { Box, CircularProgress } from "@material-ui/core";
import { ProgressBarStyle } from "./progressBarStyle";

export const ProgressBarView = (): JSX.Element => {
    const classes = ProgressBarStyle();
    return (
        <Box display="flex" alignItems="center" justifyContent="center">
            <Box m="auto">
                <CircularProgress className={classes.main} />
            </Box>
        </Box>
    );
};
