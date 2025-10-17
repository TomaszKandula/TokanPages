import React from "react";
import { OccupationProps } from "../../../../Api/Models";
import { v4 as uuid } from "uuid";
import Validate from "validate.js";

export const RenderTags = (props: OccupationProps): React.ReactElement =>
    Validate.isEmpty(props.tags) ? (
        <></>
    ) : (
        <div className="bulma-tags pt-2 ml-3 mb-4">
            {props.tags?.map((item: string, _index: number) => (
                <div className="bulma-tag bulma-is-medium" key={uuid()}>
                    {item}
                </div>
            ))}
        </div>
    );
