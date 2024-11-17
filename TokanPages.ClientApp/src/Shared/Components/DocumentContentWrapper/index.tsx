import * as React from "react";
import { ProgressOnScroll } from "../Scroll";
import { DocumentContent } from "../../../Components/Document";
import { TextItemDto } from "../../../Api/Models";

interface DocumentContentWrapperProps {
    isLoading: boolean;
    items: TextItemDto[];
}

export const DocumentContentWrapper = (props: DocumentContentWrapperProps): React.ReactElement => {
    return (
        <>
            <ProgressOnScroll height={3} bgcolor="#6367EF" duration={0.1} />
            <DocumentContent isLoading={props.isLoading} items={props.items} />
        </>
    );
};
