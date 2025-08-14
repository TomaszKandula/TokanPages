import * as React from "react";
import { API_BASE_URI } from "../../../../../Api";
import { CustomImage } from "../../../../../Shared/Components/CustomImage/customImage";
import { TextItem } from "../../Models/TextModel";
import Validate from "validate.js";

const RenderDescription = (props: { text: string }): React.ReactElement => {
    return (
        <div className="bulma-card-content">
            <span className="is-size-6">{props.text}</span>
        </div>
    );
};

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
            {hasPropAndValue ? (
                <div className="bulma-card-image">
                    <figure className="bulma-image">
                        <CustomImage
                            source={propUrl}
                            title="Illustration"
                            alt="An image of presented article text"
                            onClick={onClickEvent}
                            width={props.constraint?.width}
                            height={props.constraint?.height}
                        />
                    </figure>
                </div>
            ) : null}
            {hasValueOnly ? (
                <div className="bulma-card-image">
                    <figure className="bulma-image">
                        <CustomImage
                            source={valueUrl}
                            title="Illustration"
                            alt="An image of presented article text"
                            onClick={onClickEvent}
                            width={props.constraint?.width}
                            height={props.constraint?.height}
                        />
                    </figure>
                </div>
            ) : null}
            {hasText ? <RenderDescription text={props.text} /> : null}
        </div>
    );
};
