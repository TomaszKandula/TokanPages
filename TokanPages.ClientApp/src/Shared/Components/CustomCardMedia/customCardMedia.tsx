import * as React from "react";
import CardMedia from "@material-ui/core/CardMedia";
import validate from "validate.js";

export const renderCardMedia = (imageSource: string | undefined, className: string): JSX.Element =>
{
    return validate.isEmpty(imageSource)
        ? <div></div>
        : <CardMedia className={className} image={imageSource} />;
}
