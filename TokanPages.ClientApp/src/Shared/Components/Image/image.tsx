import * as React from "react";
import { Icon } from "../Icon";
import { RenderImageProps } from "./Types";
import validate from "validate.js";
import "./image.css";

export const Image = (props: RenderImageProps): React.ReactElement | null => {
    const [isMouseOver, setIsMouseOver] = React.useState(false);
    const onMouseOver = React.useCallback(() => {
        setIsMouseOver(!isMouseOver);
    }, [isMouseOver]);

    let src = props.source;
    if (!validate.isEmpty(props.base) && !validate.isEmpty(props.source)) {
        src = `${props.base}/${props.source}`;
    }

    if (validate.isEmpty(props.source)) {
        return null;
    }

    return (
        <div onMouseEnter={onMouseOver} onMouseLeave={onMouseOver}>
            {isMouseOver && props.isPreviewIcon && (
                <div className="preview-container">
                    <Icon name="MagnifyPlusOutline" size={5} className="preview-icon" />
                </div>
            )}
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
        </div>
    );
};
