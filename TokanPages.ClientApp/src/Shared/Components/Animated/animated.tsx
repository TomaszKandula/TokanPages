import * as React from "react";
import { HasSnapshotMode } from "../../../Shared/Services/SpaCaching";
import { AnimatedProps } from "./Types";

export const Animated = (props: AnimatedProps): React.ReactElement => {
    const hasSnapshot = HasSnapshotMode();
    if (hasSnapshot || props.isDisabled) {
        return <div className={props.className}>{props.children}</div>;
    }

    return (
        <div className={props.className} data-aos={props.dataAos} data-aos-delay={props.dataAosDelay ?? 0}>
            {props.children}
        </div>
    );
};
