import * as React from "react";
import { Box, CircularProgress } from "@material-ui/core";
import CenteredCircularLoaderStyle from "./CenteredCircularLoaderStyle";

const CenteredCircularLoader = (): JSX.Element =>
{
    const classes = CenteredCircularLoaderStyle();
    return (
        <Box display="flex" alignItems="center" justifyContent="center" >                   
            <Box m="auto"> 
                <CircularProgress className={classes.main} />
            </Box>
        </Box>
    );
}

export default CenteredCircularLoader;
