import * as React from "react";
import { Divider, IconButton } from "@material-ui/core";
import { Link } from "react-router-dom";
import { ArrowBack } from "@material-ui/icons";
import { BackArrowStyle } from "./backArrowStyle";

export const BackArrowView = (): JSX.Element => {
    const classes = BackArrowStyle();
    return (<>
        <Link to="/">
            <IconButton>
                <ArrowBack />
            </IconButton>
        </Link>
        <Divider className={classes.divider} />
    </>);
}
