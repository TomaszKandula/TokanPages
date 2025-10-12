import React, { useRef } from "react";
import { StandardBackdropProps } from "../Types";
import { ClassListAdd, ClassListClear } from "../../../../Shared/Services/Utilities";
import Validation from "validate.js";
import "./standardBackdrop.css";

const baseClasses = [
    "standard-backdrop",
];

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
                ClassListClear(classList);
                ClassListAdd(classList, list);
            } else {
                classList.remove(props.className);
                classList.add(props.className);
            }
        }

        ClassListAdd(classList, baseClasses);

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
