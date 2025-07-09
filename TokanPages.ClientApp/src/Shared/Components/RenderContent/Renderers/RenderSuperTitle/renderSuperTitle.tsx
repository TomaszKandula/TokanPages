import * as React from "react";
import { TextItem } from "../../Models";
import { API_BASE_URI } from "../../../../../Api";
import { CustomImage } from "../../../../../Shared/Components/CustomImage/customImage";

const NO_CONTENT = "EMPTY_CONTENT_PROVIDED";

export const RenderSuperTitle = (props: TextItem): React.ReactElement => {
    let propUrl = props.propImg ?? "";
    if (!propUrl.includes("https://")) {
        propUrl = `${API_BASE_URI}${propUrl}`;
    }

    return (
        <div className="bulma-content is-flex is-justify-content-space-between">
            <div className="is-flex is-flex-direction-column">
                <p className="bulma-title bulma-is-3 has-text-grey-darker">
                    {props.propTitle ?? NO_CONTENT}
                </p>
                <p className="bulma-subtitle bulma-is-5 has-text-grey-darker">
                    {props.propSubtitle ?? NO_CONTENT}
                </p>
            </div>
            <figure className="is-flex is-align-items-center">
                <CustomImage 
                    source={propUrl}
                    title="Illustration"
                    alt="An illustration of a presented article text title"
                    width={props.constraint?.width}
                    height={props.constraint?.height}
                />
            </figure>
        </div>
    );
};
