import * as React from "react";
import "./collapsibleStyle.css";
import { Icon, IconButton } from "../../../../Shared/Components";

interface CollapsibleViewProps {
    isOpen?: boolean;
    height?: number;
    clickHandler: () => void;
    children: React.ReactNode;
    reference: React.RefObject<HTMLDivElement>;
}

const openGradientClass = "collapsible-gradient collapsible-gradient-expanded";
const closeGradientClass = "collapsible-gradient";
const openExpandClass = "no-select collapsible-expand collapsible-expand-open";
const closeExpandClass = "no-select collapsible-expand";

export const CollapsibleView = (props: CollapsibleViewProps): React.ReactElement => (
    <div className="is-flex is-flex-direction-column is-align-items-center">
        <div className="collapsible-transition" style={{ height: props.height }}>
            <div className="collapsible-content" ref={props.reference}>
                <div
                    className={props.isOpen ? openGradientClass : closeGradientClass}
                    style={{ height: props.height }}
                ></div>
                {props.children}
            </div>
        </div>
        <IconButton className={props.isOpen ? openExpandClass : closeExpandClass} onClick={props.clickHandler}>
            <Icon name="ChevronDown" size={1.5} className="has-text-grey" />
        </IconButton>
    </div>
);
