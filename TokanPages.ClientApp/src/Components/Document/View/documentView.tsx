import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { DocumentStyle } from "./documentStyle";
import { BackArrow, ProgressBar, RenderContent } from "../../../Shared/Components";
import { TextItem } from "../../../Shared/Components/RenderContent/Models";
import { useHash } from "../../../Shared/Hooks";

interface DocumentViewProps {
    isLoading: boolean;
    items: TextItem[];
}

export const DocumentView = (props: DocumentViewProps): JSX.Element => {
    const hash = useHash();
    const classes = DocumentStyle();

    React.useEffect(() => {
        if (props.isLoading) {
            return;
        }

        const element = document.querySelector(hash);
        if (element) {
            element.scrollIntoView({ behavior: "smooth" });
        }

    }, [ hash, props.isLoading ]);

    return (
        <section className={classes.section}>
            <Container className={classes.container}>
                <Box py={12}>
                    <div data-aos="fade-down">
                        <BackArrow />
                    </div>
                    <div data-aos="fade-up">
                        {props.isLoading ? (
                            <ProgressBar styleObject={{ marginTop: 30 }} />
                        ) : (
                            <RenderContent items={props.items} />
                        )}
                    </div>
                </Box>
            </Container>
        </section>
    );
};
