import * as React from "react";
import { TextAreaProps } from "./types";

export const TextAreaView = (props: TextAreaProps): React.ReactElement => {
    const loading = props.isLoading ? "bulma-is-loading" : "";
    const colour = props.colour ?? "";
    const size = props.size ?? "";
    const fixed = props.isFixedSize ? "bulma-has-fixed-size" : "";

    const className = `bulma-textarea ${colour} ${size} ${fixed}`;

    return (
        <div className="bulma-field">
            <div className={`bulma-control ${loading} ${props.className ?? ""}`}>
                <textarea
                    className={className}
                    id={props.uuid}
                    name={props.uuid}
                    required={props.required}
                    rows={props.rows}
                    value={props.value}
                    onKeyUp={props.onKeyUp}
                    onChange={props.onChange}
                    placeholder={props.placeholder}
                    maxLength={props.maxLength}
                ></textarea>
            </div>
        </div>
    );
};
