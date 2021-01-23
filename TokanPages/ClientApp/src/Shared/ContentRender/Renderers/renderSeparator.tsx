import * as React from "react";
import useStyles from "../Hooks/styleRenderSeparator";

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
