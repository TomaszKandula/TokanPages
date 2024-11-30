import * as React from "react";
import validate from "validate.js";

interface RenderImageProps {
    basePath: string;
    imageSource: string;
    className: string;
    width?: number;
    height?: number;
}

export const RenderImage = (props: RenderImageProps): React.ReactElement | null => {
    return validate.isEmpty(props.imageSource) || validate.isEmpty(props.basePath) ? null : (
        <img 
            src={`${props.basePath}/${props.imageSource}`} 
            width={props.width}
            height={props.height}
            className={props.className} 
            alt={`image of ${props.imageSource}`} 
        />
    );
};
