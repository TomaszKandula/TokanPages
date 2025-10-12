import React, { useRef } from "react";
import { StandardBackdropProps } from "../Types";
import Validation from "validate.js";
import "./standardBackdrop.css";

export const StandardBackdrop = (props: StandardBackdropProps): React.ReactElement => {
    const ref = useRef<HTMLDivElement>(null);

    React.useLayoutEffect(() => {
        const classList = ref.current?.classList;
        const hasClassName = !Validation.isEmpty(props.className);

        if (!classList) {
            return;
        }

        if (props.className && hasClassName) {
            if (props.className.includes(" ")) {
                const list = props.className.split(" ");
                list.forEach(item => {
                    classList.add(item);
                });
            } else {
                classList.add(props.className);
            }
        }
    }, [props.className]);

    return (
        <div
            ref={ref}
            aria-hidden="true"
            className="standard-backdrop"
            onClick={props.onClick}
            style={props.style}
        ></div>
    );
};
