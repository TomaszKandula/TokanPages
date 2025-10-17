import React from "react";
import { RenderCaptionProps } from "../../Types";
import { Skeleton } from "../../../../Shared/Components";
import { RenderTag } from "../../../../Shared/Components/RenderContent/Renderers";

export const RenderCaption = (props: RenderCaptionProps): React.ReactElement => (
    <Skeleton isLoading={props.isLoading} mode="Text" width={200} height={24} hasSkeletonCentered className="my-4">
        <RenderTag tag={props.tag ?? "p"} className="is-size-4 has-text-grey-dark has-text-centered is-uppercase p-5">
            <>{props.text}</>
        </RenderTag>
    </Skeleton>
);
