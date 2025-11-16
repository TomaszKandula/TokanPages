import React from "react";
import { ReactElement } from "../../../Shared/Types";
import { VideoProps } from "./Types";
import validate from "validate.js";
import { ProgressBar } from "..";
import "./video.css";

export const Video = (props: VideoProps): ReactElement => {
    const videoRef = React.useRef<HTMLVideoElement | null>(null);

    const [videoUrl, setVideoUrl] = React.useState("");
    const [posterUrl, setPosterUrl] = React.useState("");
    const [isLoading, setIsLoading] = React.useState(true);

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
        if (!videoRef.current) {
            return;
        }

        videoRef.current?.addEventListener("loadeddata", () => {
            setIsLoading(false);
        });
        return () => videoRef.current?.addEventListener("loadeddata", () => {});
    }, [videoRef.current]);

    return (
        <>
            {isLoading ? (
                <div className="video-container">
                    <ProgressBar className="video-loader" size={60} thickness={5} colour="#FFF" />
                </div>
            ) : null}
            <video
                ref={videoRef}
                src={videoUrl}
                poster={posterUrl}
                preload={props.preload}
                controls={props.controls}
                autoPlay={props.autoplay}
                onClick={props.onClick}
                className={props.className}
                playsInline={true}
                style={{
                    objectFit: props.objectFit,
                    width: props.width,
                    height: props.height,
                    maxWidth: props.width,
                    maxHeight: props.height,
                }}
            />
        </>
    );
};
