import * as React from "react";
import validate from "validate.js";

export const RenderImage = (basePath: string, imageSource: string, className: string): JSX.Element | null =>
{
    return validate.isEmpty(imageSource) || validate.isEmpty(basePath)
        ? null 
        : <img src={basePath + imageSource} className={className} alt="" />;
};
