import * as React from "react";
import { Box, Divider, IconButton } from "@material-ui/core";
import { Link } from "react-router-dom";
import { ArrowBack } from "@material-ui/icons";
import { BackArrowStyle } from "./backArrowStyle";

interface BackArrowViewProps {
    backPathFragment?: string;
}

export const BackArrowView = (props: BackArrowViewProps): JSX.Element => {
    const classes = BackArrowStyle();
    const path = props.backPathFragment ?? "/";

    return (
        <>
            <Box mx={2} my={2}>
                <Link to={path}>
                    <IconButton className={classes.icon}>
                        <ArrowBack />
                    </IconButton>
                </Link>
            </Box>
            <Box>
                <Divider />
            </Box>
        </>
    );
};
