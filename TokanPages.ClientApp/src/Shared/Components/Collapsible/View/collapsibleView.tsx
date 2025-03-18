import * as React from "react";
import { IconButton } from "@material-ui/core";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import "../../../../Shared/Components/Collapsible/View/collapsibleStyle.css";

interface CollapsibleViewProps {
    isOpen?: boolean;
    height?: number;
    clickHandler: () => void;
    children: React.ReactNode;
    reference: React.RefObject<HTMLDivElement>;
}

export const CollapsibleView = (props: CollapsibleViewProps): React.ReactElement => {
    return (
        <>
            <div className="collapsible-transition" style={{ height: props.height }}>
                <div className="collapsible-content" ref={props.reference}>
                    <div 
                        className={props.isOpen 
                            ? "collapsible-gradient collapsible-gradient-expanded" 
                            : "collapsible-gradient"
                        }
                        style={{ height: props.height }}
                    >
                    </div>
                    {props.children}
                </div>
            </div>
            <IconButton
                className={
                    props.isOpen
                    ? "collapsible-expand collapsible-expand-open"
                    : "collapsible-expand"
                }
                onClick={props.clickHandler}
                aria-expanded={props.isOpen}
                aria-label="show more"
            >
                <ExpandMoreIcon />
            </IconButton>
        </>
    );
}
