import * as React from "react";
import useStyles from "../Styles/renderSeparatorStyle";

export function RenderSeparator()
{
    const classes = useStyles();
    return(
        <div className={classes.separator}>
            <span className={classes.span}></span>
            <span className={classes.span}></span>
            <span className={classes.span}></span>
        </div>
    );
}
