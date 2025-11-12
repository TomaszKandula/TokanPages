import * as React from "react";
import { DocumentContent } from "../../../Components/Document";
import { ProgressOnScroll } from "../Scroll";
import { DocumentContentWrapperProps } from "./Types";

export const DocumentContentWrapper = (props: DocumentContentWrapperProps): React.ReactElement => (
    <>
        <ProgressOnScroll height={3} bgcolor="#6367EF" duration={0.1} />
        <DocumentContent isLoading={props.isLoading} items={props.items} />
    </>
);
