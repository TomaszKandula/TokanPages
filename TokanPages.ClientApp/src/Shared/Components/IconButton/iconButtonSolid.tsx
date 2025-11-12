import * as React from "react";
import { ReactElement } from "../../../Shared/Types";
import { ClassListAdd, ClassListClear } from "../../../Shared/Services/Utilities";
import { Icon } from "../Icon";
import { IconButtonSolidProps } from "./Types";
import { baseClasses } from "./Constants";
import "./iconButton.css";

export const IconButtonSolid = (props: IconButtonSolidProps): ReactElement => {
    const size = props.size ?? 2.0;
    const buttonRef = React.useRef<HTMLButtonElement>(null);

    React.useLayoutEffect(() => {
        const classList = buttonRef.current?.classList;
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

        if (props.isDisabled) {
            classList.add("icon-button-solid-hoverable-disabled");
            classList.remove("is-clickable");
            classList.remove("has-text-grey");
            classList.remove("icon-button-solid-hoverable");
        } else {
            classList.remove("icon-button-solid-hoverable-disabled");
            classList.add("has-text-grey");
            classList.add("is-clickable");
            classList.add("icon-button-solid-hoverable");
        }
    }, [buttonRef.current?.classList, props.className, props.isDisabled]);

    return (
        <button
            ref={buttonRef}
            onClick={props.onClick}
            onMouseDown={props.onMouseDown}
            style={{ height: `${size}rem`, width: `${size}rem`, visibility: props.isInvisible ? "hidden" : "visible" }}
            disabled={props.isDisabled}
        >
            <Icon name={props.name} size={size} />
        </button>
    );
};
