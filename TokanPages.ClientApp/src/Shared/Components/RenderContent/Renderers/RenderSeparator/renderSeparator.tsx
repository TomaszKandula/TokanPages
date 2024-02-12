import * as React from "react";
import { RenderSeparatorStyle } from "./renderSeparatorStyle";

export const RenderSeparator = (): JSX.Element => {
    const classes = RenderSeparatorStyle();
    return (
        <div className={classes.separator}>
            <span className={classes.span}></span>
            <span className={classes.span}></span>
            <span className={classes.span}></span>
        </div>
    );
};
