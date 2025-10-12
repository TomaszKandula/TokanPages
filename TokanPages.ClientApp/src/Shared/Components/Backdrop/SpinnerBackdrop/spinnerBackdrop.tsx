import * as React from "react";
import { ProgressBar } from "../..";
import "./spinnerBackdrop.css";

interface RenderbackdropProps {
    isLoading: boolean;
    children?: React.ReactElement;
}

const ChildrenOrSpinner = (props: RenderbackdropProps) => {
    return props.children === undefined ? <ProgressBar colour="#fff" size={50} thickness={4} /> : props.children;
};

export const SpinnerBackdrop = (props: RenderbackdropProps): React.ReactElement => (
    <>
        {props.isLoading ? (
            <div className="spinner-backdrop">
                <ChildrenOrSpinner {...props} />
            </div>
        ) : null}
    </>
);
