import * as React from "react";
import { useMediaPresenter } from "../../../../../Shared/Hooks";
import { MediaPresenter } from "../../../../../Shared/Components/MediaPresenter";
import { TextItem } from "../../Models/TextModel";
import Validate from "validate.js";
import { Video } from "Shared/Components/Video";

interface RenderClipProps extends TextItem {
    posterUrl: string;
    videoUrl: string;
    onClick: () => void;
}

const RenderDescription = (props: { text: string }): React.ReactElement => (
    <>
        <hr className="m-0" />
        <div className="bulma-card-content">
            <span className="is-size-6">{props.text}</span>
        </div>
    </>
);

const RenderClip = (props: RenderClipProps): React.ReactElement => {
    const hasText = !Validate.isEmpty(props.text);

    return (
        <>
            <div className="bulma-card-image">
                <figure className="bulma-image">
                    <Video
                        source={props.videoUrl}
                        poster={props.posterUrl}
                        controls={true}
                        preload="metadata"
                        width={props.constraint?.width ?? "100%"}
                        height={props.constraint?.height}
                    />
                </figure>
            </div>
            {hasText ? null : <RenderDescription text={props.text} />}
        </>
    );
};

export const RenderVideo = (props: TextItem): React.ReactElement => {
    const presenter = useMediaPresenter();

    const [posterUrl, setPosterUrl] = React.useState("");
    const [videoUrl, setVideoUrl] = React.useState("");

    const hasValue = !Validate.isEmpty(props.value);
    const hasProp = !Validate.isEmpty(props.prop);
    const hasPropAndValue = hasProp && hasValue;

    const onClickEvent = React.useCallback(() => {
        presenter.onSelectionClick(0);
    }, []);

    React.useEffect(() => {
        if (hasPropAndValue) {
            setPosterUrl(props.prop);
            setVideoUrl(props.value as string);
        }
    }, [hasPropAndValue]);

    return (
        <div className="bulma-card my-6">
            <RenderClip {...props} posterUrl={posterUrl} videoUrl={videoUrl} onClick={onClickEvent} />
            <MediaPresenter
                isOpen={presenter.isPresenterOpen}
                presenting={presenter.selection}
                collection={[videoUrl]}
                type="video"
                onTrigger={presenter.onPresenterClick}
            />
        </div>
    );
};
