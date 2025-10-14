import * as React from "react";
import { API_BASE_URI } from "../../../../../Api";
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

const RenderPicture = (props: RenderPictureProps) => (
    <div className="bulma-card-image">
        <figure className="bulma-image">
            <Image
                source={props.url}
                title="Illustration"
                alt="An image of presented article text"
                onClick={props.onClick}
                width={props.constraint?.width}
                height={props.constraint?.height}
                objectFit={props.constraint?.objectFit}
                loading={props.loading}
            />
        </figure>
    </div>
);

export const RenderImage = (props: TextItem): React.ReactElement => {
    const hasProp = !Validate.isEmpty(props.prop);
    const hasValue = !Validate.isEmpty(props.value);
    const hasText = !Validate.isEmpty(props.text);
    const hasPropAndValue = hasProp && hasValue;
    const hasValueOnly = !hasProp && hasValue;

    let valueUrl = props.value as string;
    if (!valueUrl.includes("https://")) {
        valueUrl = `${API_BASE_URI}${valueUrl}`;
    }

    let propUrl = props.prop;
    if (!propUrl.includes("https://")) {
        propUrl = `${API_BASE_URI}${propUrl}`;
    }

    const onClickEvent = React.useCallback(() => {
        window.open(valueUrl, "_blank");
    }, [valueUrl]);

    return (
        <div className="bulma-card my-6">
            {hasPropAndValue ? <RenderPicture {...props} url={propUrl} onClick={onClickEvent} /> : null}
            {hasValueOnly ? <RenderPicture {...props} url={valueUrl} onClick={onClickEvent} /> : null}
            {hasText ? <RenderDescription text={props.text} /> : null}
        </div>
    );
};
