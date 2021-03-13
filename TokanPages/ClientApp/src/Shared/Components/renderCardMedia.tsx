import * as React from "react";
import CardMedia from "@material-ui/core/CardMedia";

export const renderCardMedia = (imageSource: string, media: string): JSX.Element | null =>
{
    return imageSource === "" 
        ? null 
        : <CardMedia className={media} image={imageSource} />;
}
