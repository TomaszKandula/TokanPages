import * as React from "react";

export const renderImage = (basePath: string, imageSource: string, className: string): JSX.Element | null =>
{
    return imageSource === "" || basePath === ""
        ? null 
        : <img src={basePath + imageSource} className={className} alt="" />;
};
