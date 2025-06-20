import * as React from "react";
import { ProgressBar } from "..";
import "./backdrop.css";

interface RenderbackdropProps {
    isLoading: boolean;
}

export const RenderBackdrop = (props: RenderbackdropProps): React.ReactElement => (
    <>
        {props.isLoading 
        ? <div className="backdrop">
            <ProgressBar colour="#fff" size={50} thickness={4} />
        </div>
        : null}
    </>
);
