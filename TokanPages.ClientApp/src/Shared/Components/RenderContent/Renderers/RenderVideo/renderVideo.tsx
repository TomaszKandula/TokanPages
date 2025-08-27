import * as React from "react";
import { TextItem } from "../../Models/TextModel";
import { API_BASE_URI } from "../../../../../Api";
import Validate from "validate.js";

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

export const RenderVideo = (props: TextItem): React.ReactElement => {
    let valueUrl = props.value as string;
    if (!valueUrl.includes("https://")) {
        valueUrl = `${API_BASE_URI}${valueUrl}`;
    }

    let propUrl = props.prop;
    if (!propUrl.includes("https://")) {
        propUrl = `${API_BASE_URI}${propUrl}`;
    }

    return (
        <div className="bulma-card my-6">
            <div className="bulma-card-image">
                <figure className="bulma-image">
                    <video
                        src={valueUrl}
                        poster={propUrl}
                        controls
                        title="Video related to the presented article text"
                        width={props.constraint?.width ?? "100%"}
                        height={props.constraint?.height}
                        style={{
                            borderTopLeftRadius: "0.75rem",
                            borderTopRightRadius: "0.75rem",
                        }}
                    />
                </figure>
            </div>
            {Validate.isEmpty(props.text) ? null : <RenderDescription text={props.text} />}
        </div>
    );
};
