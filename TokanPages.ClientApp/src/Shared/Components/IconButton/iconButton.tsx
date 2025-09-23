import * as React from "react";
import { ReactMouseEvent } from "../../../Shared/types";
import "./iconButton.css";

interface IconButtonProps {
    size?: number;
    children: React.ReactElement | React.ReactElement[];
    hasNoHoverEffect?: boolean;
    className?: string;
    isDisabled?: boolean;
    style?: "grey" | "transparent";
    onClick?: (event: ReactMouseEvent) => void;
    onMouseDown?: (event: ReactMouseEvent) => void;
}

export const IconButton = (props: IconButtonProps): React.ReactElement => {
    const size = props.size ?? 48;
    const ref = React.useRef<HTMLButtonElement>(null);
    const baseClass = "icon-button-base is-flex is-align-self-center";

    React.useLayoutEffect(() => {
        const classList = ref.current?.classList;
        if (!classList) {
            return;
        }

        if (props.className && props.className !== "") {
            if (props.className.includes(" ")) {
                const list = props.className.split(" ");
                list.forEach(item => {
                    classList.add(item);
                });
            }
        }

        if (props.hasNoHoverEffect) {
            classList.add("icon-button-non-hoverable");
            classList.remove("icon-button-hoverable");
        } else {
            classList.remove("icon-button-non-hoverable");
            classList.add("icon-button-hoverable");
        }

        if (props.isDisabled) {
            classList.add("icon-button-disabled");
            classList.remove("has-text-grey");
        } else {
            classList.remove("icon-button-disabled");
            classList.add("has-text-grey");
        }
    }, [props.className, props.hasNoHoverEffect, props.isDisabled]);

    return (
        <button
            ref={ref}
            className={baseClass}
            onClick={props.onClick}
            onMouseDown={props.onMouseDown}
            style={{ height: size, width: size }}
            disabled={props.isDisabled}
        >
            {props.children}
        </button>
    );
};
