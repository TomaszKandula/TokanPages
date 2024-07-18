import * as React from "react";
import Container from "@material-ui/core/Container";
import { DocumentStyle } from "./documentStyle";
import { ProgressBar, RenderContent } from "../../../Shared/Components";
import { TextItem } from "../../../Shared/Components/RenderContent/Models";
import { useHash } from "../../../Shared/Hooks";

interface DocumentViewProps {
    isLoading: boolean;
    items: TextItem[];
    background?: React.CSSProperties;
}

export const DocumentView = (props: DocumentViewProps): JSX.Element => {
    const hash = useHash();
    const classes = DocumentStyle();

    React.useEffect(() => {
        if (props.isLoading) {
            return;
        }

        if (hash === "") {
            return;
        }

        const element = document?.querySelector(hash);
        if (element) {
            setTimeout(
                () =>
                    element.scrollIntoView({
                        block: "start",
                        behavior: "smooth",
                    }),
                500
            );
        }
    }, [hash, props.isLoading]);

    return (
        <section className={classes.section} style={props.background}>
            <Container className={classes.container}>
                {props.isLoading ? (
                    <ProgressBar styleObject={{ marginTop: 30, marginBottom: 30 }} />
                ) : (
                    <RenderContent items={props.items} />
                )}
            </Container>
        </section>
    );
};
