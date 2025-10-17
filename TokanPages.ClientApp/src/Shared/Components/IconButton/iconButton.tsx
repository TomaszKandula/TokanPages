import * as React from "react";
import { ClassListAdd, ClassListClear } from "../../../Shared/Services/Utilities";
import { IconButtonProps } from "./Types";
import "./iconButton.css";

const baseClasses = ["icon-button-base", "is-flex", "is-align-self-center"];

export const IconButton = (props: IconButtonProps): React.ReactElement => {
    const size = props.size ?? 2.0;
    const ref = React.useRef<HTMLButtonElement>(null);

    const buttonHoverable = props.hasGreyBackground ? "icon-button-hoverable-grey" : "icon-button-hoverable";
    const buttonNonHoverable = props.hasGreyBackground ? "icon-button-non-hoverable-grey" : "icon-button-non-hoverable";
    const buttonDisabled = props.hasGreyBackground ? "icon-button-disabled-grey" : "icon-button-disabled";

    React.useLayoutEffect(() => {
        const classList = ref.current?.classList;
        if (!classList) {
            return;
        }

        if (props.className && props.className !== "") {
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

        if (props.hasNoHoverEffect) {
            classList.add(buttonNonHoverable);
            classList.remove(buttonHoverable);
        } else {
            classList.remove(buttonNonHoverable);
            classList.add(buttonHoverable);
        }

        if (props.isDisabled) {
            classList.add(buttonDisabled);
            classList.remove("has-text-grey");
        } else {
            classList.remove(buttonDisabled);
            classList.add("has-text-grey");
        }
    }, [props.className, props.hasNoHoverEffect, props.isDisabled]);

    return (
        <button
            ref={ref}
            onClick={props.onClick}
            onMouseDown={props.onMouseDown}
            style={{ height: `${size}rem`, width: `${size}rem` }}
            disabled={props.isDisabled}
        >
            {props.children}
        </button>
    );
};
