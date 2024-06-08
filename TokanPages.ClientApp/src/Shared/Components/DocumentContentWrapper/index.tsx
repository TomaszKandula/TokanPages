import * as React from "react";
import { Container } from "@material-ui/core";
import { ProgressOnScroll } from "../Scroll";
import { DocumentContent } from "../../../Components/Document";
import { Colours } from "../../../Theme";
import { TextItemDto } from "../../../Api/Models";

interface DocumentContentWrapperProps {
    isLoading: boolean;
    items: TextItemDto[];
}

export const DocumentContentWrapper = (props: DocumentContentWrapperProps): JSX.Element => {
    return (
        <>
            <ProgressOnScroll height={3} bgcolor={Colours.application.navigation} duration={0.1} />
            <Container>
                <DocumentContent isLoading={props.isLoading} items={props.items} />
            </Container>
        </>
    );
}
