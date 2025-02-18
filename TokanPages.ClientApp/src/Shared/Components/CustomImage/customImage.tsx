import * as React from "react";
import validate from "validate.js";

interface RenderImageProps {
    base: string;
    source: string;
    className: string;
    width?: number;
    height?: number;
}

export const RenderImage = (props: RenderImageProps): React.ReactElement | null => {
    return validate.isEmpty(props.source) || validate.isEmpty(props.base) ? null : (
        <img
            src={`${props.base}/${props.source}`}
            width={props.width}
            height={props.height}
            className={props.className}
            alt={`image of ${props.source}`}
        />
    );
};
