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

export const CollapsibleView = (props: CollapsibleViewProps): React.ReactElement => {
    return (
        <div className="is-flex is-flex-direction-column is-align-items-center">
            <div className="collapsible-transition" style={{ height: props.height }}>
                <div className="collapsible-content" ref={props.reference}>
                    <div
                        className={
                            props.isOpen ? "collapsible-gradient collapsible-gradient-expanded" : "collapsible-gradient"
                        }
                        style={{ height: props.height }}
                    ></div>
                    {props.children}
                </div>
            </div>
            <IconButton
                className={props.isOpen ? "collapsible-expand collapsible-expand-open" : "collapsible-expand"}
                onClick={props.clickHandler}
            >
                <Icon name="ChevronDown" size={1.0} className="has-text-grey" />
            </IconButton>
        </div>
    );
};
