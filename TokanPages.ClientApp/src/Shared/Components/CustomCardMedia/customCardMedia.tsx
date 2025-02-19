import * as React from "react";
import CardMedia from "@material-ui/core/CardMedia";
import validate from "validate.js";

export const RenderCardMedia = (
    basePath: string,
    imageSource: string | undefined,
    className: string
): React.ReactElement => {
    return validate.isEmpty(imageSource) || validate.isEmpty(basePath) ? (
        <div></div>
    ) : (
        <CardMedia component="img" loading="lazy" image={`${basePath}/${imageSource}`} className={className} />
    );
};
