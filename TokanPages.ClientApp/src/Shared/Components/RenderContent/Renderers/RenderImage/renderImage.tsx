import * as React from "react";
import { API_BASE_URI } from "../../../../../Api";
import { MediaPresenter } from "../../../../../Shared/Components/MediaPresenter";
import { useMediaPresenter } from "../../../../../Shared/Hooks";
import { Image } from "../../../Image";
import { TextItem } from "../../Models/TextModel";
import Validate from "validate.js";

interface RenderPictureProps extends TextItem {
    url: string;
    onClick: () => void;
}

const RenderDescription = (props: { text: string }): React.ReactElement => {
    return (
        <>
            <hr className="m-0" />
            <div className="bulma-card-content">
                <span className="is-size-6">{props.text}</span>
            </div>
        </>
    );
};

const RenderPicture = (props: RenderPictureProps): React.ReactElement => {
    const hasText = !Validate.isEmpty(props.text);

    return (
        <>
            <div className="bulma-card-image">
                <figure className="bulma-image" onClick={props.onClick}>
                    <Image
                        isPreviewIcon
                        isPreviewTopRadius
                        isPreviewBottomRadius={!hasText}
                        source={!Validate.isEmpty(props.url) ? `${API_BASE_URI}${props.url}` : ""}
                        title="Illustration"
                        alt="An image of presented article text"
                        width={props.constraint?.width}
                        height={props.constraint?.height}
                        objectFit={props.constraint?.objectFit}
                        loading={props.loading}
                    />
                </figure>
            </div>
            {hasText ? <RenderDescription text={props.text} /> : null}
        </>
    );
};

export const RenderImage = (props: TextItem): React.ReactElement => {
    const presenter = useMediaPresenter();

    const [imageUrl, setImageUrl] = React.useState("");

    const hasProp = !Validate.isEmpty(props.prop);
    const hasValue = !Validate.isEmpty(props.value);
    const hasPropAndValue = hasProp && hasValue;
    const hasValueOnly = !hasProp && hasValue;

    const onClickEvent = React.useCallback(() => {
        presenter.onSelectionClick(0);
    }, []);

    React.useEffect(() => {
        if (hasPropAndValue) {
            setImageUrl(props.prop);
        } else if (hasValueOnly) {
            setImageUrl(props.value as string);
        }
    }, [hasPropAndValue, hasValueOnly, props.value, props.prop]);

    return (
        <div className="bulma-card my-6">
            <RenderPicture {...props} url={imageUrl} onClick={onClickEvent} />
            <MediaPresenter
                isOpen={presenter.isPresenterOpen}
                presenting={presenter.selection}
                collection={[imageUrl]}
                type="image"
                onTrigger={presenter.onPresenterClick}
            />
        </div>
    );
};
