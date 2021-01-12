import * as React from "react";
import { Box, CircularProgress } from "@material-ui/core";

export default function CenteredCircularLoader()
{
    return (
        <Box display="flex" alignItems="center" justifyContent="center" >                   
            <Box m="auto"> 
                <CircularProgress />
            </Box>
        </Box>
    );
}
