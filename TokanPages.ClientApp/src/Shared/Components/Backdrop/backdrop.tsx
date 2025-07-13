import * as React from "react";
import { ProgressBar } from "..";
import "./backdrop.css";

interface RenderbackdropProps {
    isLoading: boolean;
    children?: React.ReactElement;
}

export const RenderBackdrop = (props: RenderbackdropProps): React.ReactElement => {
    const ChildrenOrSpinner = (props: RenderbackdropProps) => {
        return props.children === undefined ? <ProgressBar colour="#fff" size={50} thickness={4} /> : props.children;
    };

    return (
        <>
            {props.isLoading ? (
                <div className="backdrop">
                    <ChildrenOrSpinner {...props} />
                </div>
            ) : null}
        </>
    );
};
