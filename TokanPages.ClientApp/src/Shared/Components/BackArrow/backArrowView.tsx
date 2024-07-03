import * as React from "react";
import { Divider, IconButton } from "@material-ui/core";
import { Link } from "react-router-dom";
import { ArrowBack } from "@material-ui/icons";
import { BackArrowStyle } from "./backArrowStyle";

interface BackArrowViewProps {
    backPathFragment?: string;
    className?: string;
}

export const BackArrowView = (props: BackArrowViewProps): JSX.Element => {
    const classes = BackArrowStyle();
    const path = props.backPathFragment ?? "/";

    return (
        <div className={props.className}>
            <Link to={path}>
                <IconButton className={classes.icon}>
                    <ArrowBack />
                </IconButton>
            </Link>
            <Divider className={classes.divider} />
        </div>
    );
};
