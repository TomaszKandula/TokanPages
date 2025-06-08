import * as React from "react";
import { v4 as uuidv4 } from "uuid";

interface SkeletonProps {
    isLoading: boolean;
    children?: React.ReactElement | React.ReactElement[];
}

const SkeletonBlock = () => (
    <div className="my-2 bulma-skeleton-block">
        &nbsp;
    </div>
);

export const Skeleton = (props: SkeletonProps): React.ReactElement => {
    if (props.isLoading) {
        const buffer: React.ReactElement[] = [];
        if (Array.isArray(props.children) && props.children.length > 1) {
            props.children.forEach(_item => {
                buffer.push(<SkeletonBlock key={uuidv4()} />);
            });

            return (<>{buffer}</>);
        }

        return (<SkeletonBlock />);
    }

    return (<>{props.children}</>);
}
