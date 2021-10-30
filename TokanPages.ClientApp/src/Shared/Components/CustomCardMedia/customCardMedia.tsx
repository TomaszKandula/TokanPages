import * as React from "react";
import CardMedia from "@material-ui/core/CardMedia";
import validate from "validate.js";

export const renderCardMedia = (basePath: string, imageSource: string | undefined, className: string): JSX.Element =>
{
    return validate.isEmpty(imageSource) || validate.isEmpty(basePath)
        ? <div></div>
        : <CardMedia image={basePath + imageSource} className={className} />;
}
