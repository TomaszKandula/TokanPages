import * as React from "react";
import { TextItem } from "../../Models/TextModel";
import { API_BASE_URI } from "../../../../../Api";
import { CustomImage } from "../../../../../Shared/Components/CustomImage/customImage";
import Validate from "validate.js";

const RenderDescription = (props: { text: string }): React.ReactElement => {
    return (
        <div className="bulma-card-content">
            <span className="is-size-6">{props.text}</span>
        </div>
    );
};

export const RenderVideo = (props: TextItem): React.ReactElement => {
    let valueUrl = props.value as string;
    if (!valueUrl.includes("https://")) {
        valueUrl = `${API_BASE_URI}${valueUrl}`;
    }

    let propUrl = props.prop;
    if (!propUrl.includes("https://")) {
        propUrl = `${API_BASE_URI}${propUrl}`;
    }

    const [hasImage, setHasImage] = React.useState(true);
    const onClickEvent = React.useCallback(() => setHasImage(false), []);

    return (
        <div className="bulma-card my-6">
            <div className="bulma-card-image">
                <figure className="bulma-image">
                    {hasImage ? (
                        <CustomImage
                            source={propUrl}
                            onClick={onClickEvent}
                            title="Video"
                            alt="Video related to the presented article text"
                            width={props.constraint?.width}
                            height={props.constraint?.height}
                            loading={props.loading}
                        />
                    ) : (
                        <video
                            src={valueUrl}
                            controls
                            autoPlay
                            style={{
                                borderTopLeftRadius: "0.75rem",
                                borderTopRightRadius: "0.75rem",
                            }}
                        />
                    )}
                </figure>
            </div>
            {Validate.isEmpty(props.text) ? null : <RenderDescription text={props.text} />}
        </div>
    );
};
