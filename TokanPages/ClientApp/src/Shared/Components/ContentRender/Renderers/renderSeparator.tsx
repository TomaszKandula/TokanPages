import * as React from "react";
import renderSeparatorStyle from "../Styles/renderSeparatorStyle";

export function RenderSeparator()
{
    const classes = renderSeparatorStyle();
    return(
        <div className={classes.separator}>
            <span className={classes.span}></span>
            <span className={classes.span}></span>
            <span className={classes.span}></span>
        </div>
    );
}
