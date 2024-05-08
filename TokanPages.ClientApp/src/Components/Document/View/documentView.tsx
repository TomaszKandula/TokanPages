import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { DocumentStyle } from "./documentStyle";
import { BackArrow, ProgressBar, RenderContent } from "../../../Shared/Components";
import { TextItem } from "../../../Shared/Components/RenderContent/Models";

interface DocumentViewProps {
    isLoading: boolean;
    items: TextItem[];
}

export const DocumentView = (props: DocumentViewProps): JSX.Element => {
    const classes = DocumentStyle();
    return (
        <section className={classes.section}>
            <Container className={classes.container}>
                <Box py={12}>
                    <div data-aos="fade-down">
                        <BackArrow />
                    </div>
                    <div data-aos="fade-up">
                        {props.isLoading ? <ProgressBar /> : <RenderContent items={props.items} />}
                    </div>
                </Box>
            </Container>
        </section>
    );
};
