import React from "react";
import { Icon } from "../Icon";
import { ReactElement } from "../../../Shared/Types";
import { VideoProps } from "./Types";
import validate from "validate.js";
import "./video.css";

export const Video = (props: VideoProps): ReactElement => {
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
                    <Icon name="PlayCircleOutline" size={5} className="preview-icon" />
                </div>
            )}
            <video
                src={source}
                poster={props.poster}
                preload={props.preload}
                controls={props.controls}
                onClick={props.onClick}
                className={props.className}
                style={{
                    objectFit: props.objectFit,
                    width: props.width,
                    height: props.height,
                    maxWidth: props.width,
                    maxHeight: props.height,
                    borderTopLeftRadius: "0.75rem",
                    borderTopRightRadius: "0.75rem",
                }}
            />
        </div>
    );
};
