import * as React from "react";
import { TLoading, TObjectFit } from "../../../Shared/Types";
import validate from "validate.js";

interface RenderImageProps {
    source: string;
    base?: string;
    className?: string;
    width?: number;
    height?: number;
    objectFit?: TObjectFit;
    alt?: string;
    title?: string;
    loading?: TLoading;
    onClick?: () => void;
}

export const CustomImage = (props: RenderImageProps): React.ReactElement | null => {
    let src = props.source;
    if (!validate.isEmpty(props.base) && !validate.isEmpty(props.source)) {
        src = `${props.base}/${props.source}`;
    }

    if (validate.isEmpty(props.source)) {
        return null;
    }

    return (
        <img
            src={src}
            loading={props.loading ?? "lazy"}
            style={{
                objectFit: props.objectFit,
                width: props.width,
                height: props.height,
            }}
            className={props.className}
            alt={props.alt}
            title={props.title}
            onClick={props.onClick}
        />
    );
};
