import React from "react";
import { Icon } from "../Icon";
import { ReactElement } from "../../../Shared/Types";
import { VideoProps } from "./Types";
import validate from "validate.js";
import "./video.css";

export const Video = (props: VideoProps): ReactElement => {
    const [isMouseOver, setIsMouseOver] = React.useState(false);
    const [videoUrl, setVideoUrl] = React.useState("");
    const [posterUrl, setPosterUrl] = React.useState("");

    const topRadius = props.isPreviewTopRadius ? "preview-top-radius" : "";
    const bottomRadius = props.isPreviewBottomRadius ? "preview-bottom-radius" : "";

    const onMouseOver = React.useCallback(() => {
        setIsMouseOver(!isMouseOver);
    }, [isMouseOver]);

    React.useEffect(() => {
        if (!validate.isEmpty(props.base) && !validate.isEmpty(props.source)) {
            setVideoUrl(`${props.base}${props.source}`);
        } else if (!validate.isEmpty(props.source)) {
            setVideoUrl(props.source);
        }
    }, [props.base, props.source]);

    React.useEffect(() => {
        if (!validate.isEmpty(props.base) && !validate.isEmpty(props.poster)) {
            setPosterUrl(`${props.base}${props.poster}`);
        } else if (!validate.isEmpty(props.poster)) {
            setPosterUrl(props.poster);
        }
    }, [props.base, props.poster]);

    return (
        <div onMouseEnter={onMouseOver} onMouseLeave={onMouseOver}>
            {isMouseOver && props.isPreviewIcon && (
                <div className={`preview-container ${topRadius} ${bottomRadius}`}>
                    <Icon name="PlayCircleOutline" size={5} className="preview-icon" />
                </div>
            )}
            <video
                src={videoUrl}
                poster={posterUrl}
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
