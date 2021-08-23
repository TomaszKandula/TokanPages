import * as React from "react";
import { Box, CircularProgress } from "@material-ui/core";

const CenteredCircularLoader = (): JSX.Element =>
{
    return (
        <Box display="flex" alignItems="center" justifyContent="center" >                   
            <Box m="auto"> 
                <CircularProgress />
            </Box>
        </Box>
    );
}

export default CenteredCircularLoader;
