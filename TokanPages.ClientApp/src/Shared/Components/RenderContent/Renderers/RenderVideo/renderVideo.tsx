import * as React from "react";
import { API_BASE_URI } from "../../../../../Api";
import { useMediaPresenter } from "../../../../../Shared/Hooks";
import { MediaPresenter, Image } from "../../../../../Shared/Components";
import { TextItem } from "../../Models/TextModel";
import Validate from "validate.js";

interface RenderClipProps extends TextItem {
    posterUrl: string;
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

const RenderPoster = (props: RenderClipProps): React.ReactElement => {
    const hasText = !Validate.isEmpty(props.text);

    return (
        <>
            <div className="bulma-card-image">
                <figure className="bulma-image" onClick={props.onClick}>
                    <Image 
                        previewIcon="PlayCircleOutline"
                        isPreviewAlways
                        isPreviewIcon
                        isPreviewTopRadius
                        source={`${API_BASE_URI}${props.posterUrl}`}
                    />
                </figure>
            </div>
            {hasText ? <RenderDescription text={props.text} /> : null}
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
    }, [hasPropAndValue, props.prop, props.value]);

    return (
        <div className="bulma-card my-6">
            <RenderPoster {...props} posterUrl={posterUrl} onClick={onClickEvent} />
            <MediaPresenter
                isOpen={presenter.isPresenterOpen}
                presenting={presenter.selection}
                collection={[videoUrl]}
                posters={[posterUrl]}
                type="video"
                onTrigger={presenter.onPresenterClick}
            />
        </div>
    );
};
