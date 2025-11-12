import React from "react";
import { ReactElement } from "../../../Shared/Types";
import { useDimensions } from "../../../Shared/Hooks";
import { VideoProps } from "./Types";
import validate from "validate.js";
import "./video.css";

export const Video = (props: VideoProps): ReactElement => {
    const media = useDimensions();
    const containerRef = React.useRef<HTMLDivElement | null>(null);

    const [videoUrl, setVideoUrl] = React.useState("");
    const [posterUrl, setPosterUrl] = React.useState("");

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

    React.useEffect(() => {
        if (media.isDesktop === false && containerRef.current) {
            containerRef.current?.requestFullscreen();
        }
    }, [media.isDesktop, containerRef.current]);

    return (
        <div ref={containerRef}>
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
