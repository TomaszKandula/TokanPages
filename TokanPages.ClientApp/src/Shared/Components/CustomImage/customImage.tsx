import * as React from "react";
import { TLoading } from "Shared/types";
import validate from "validate.js";

interface RenderImageProps {
    source: string;
    base?: string;
    className?: string;
    width?: number;
    height?: number;
    alt?: string;
    title?: string;
    loading?: TLoading;
    onClick?: () => void;
}

export const CustomImage = (props: RenderImageProps): React.ReactElement | null => {
    let className = props.className;
    if (props.className && props.className !== "" && !props.className.includes("lazyloaded")) {
        className = `${className} lazyloaded`;
    }

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
            loading={props.loading}
            width={props.width}
            height={props.height}
            className={className}
            alt={props.alt}
            title={props.title}
            onClick={props.onClick}
        />
    );
};
