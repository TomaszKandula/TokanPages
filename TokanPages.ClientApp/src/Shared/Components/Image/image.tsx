import * as React from "react";
import { Icon } from "../Icon";
import { RenderImageProps } from "./Types";
import validate from "validate.js";
import "./image.css";

export const Image = (props: RenderImageProps): React.ReactElement | null => {
    const [isMouseOver, setIsMouseOver] = React.useState(false);
    const [source, setSource] = React.useState("");

    const topRadius = props.isPreviewTopRadius ? "preview-top-radius" : "";
    const bottomRadius = props.isPreviewBottomRadius ? "preview-bottom-radius" : "";

    const onMouseOver = React.useCallback(() => {
        setIsMouseOver(!isMouseOver);
    }, [isMouseOver]);

    React.useEffect(() => {
        if (!validate.isEmpty(props.base) && !validate.isEmpty(props.source)) {
            setSource(`${props.base}/${props.source}`);
        } else if (!validate.isEmpty(props.source)) {
            setSource(props.source);
        }
    }, [props.source, props.base]);

    return (
        <div onMouseEnter={onMouseOver} onMouseLeave={onMouseOver}>
            {isMouseOver && props.isPreviewIcon && (
                <div className={`preview-container ${topRadius} ${bottomRadius}`}>
                    <Icon name="MagnifyPlusOutline" size={5} className="preview-icon" />
                </div>
            )}
            <img
                src={source}
                loading={props.loading ?? "lazy"}
                style={{
                    objectFit: props.objectFit,
                    width: props.width,
                    height: props.height,
                    maxWidth: props.width,
                    maxHeight: props.height,
                }}
                className={props.className}
                alt={props.alt}
                title={props.title}
                onClick={props.onClick}
            />
        </div>
    );
};
