import * as React from "react";
import Container from "@material-ui/core/Container";
import { ProgressBar, RenderContent } from "../../../Shared/Components";
import { TextItem } from "../../../Shared/Components/RenderContent/Models";
import { useHash } from "../../../Shared/Hooks";

interface DocumentViewProps {
    isLoading: boolean;
    items: TextItem[];
    background?: string;
}

export const DocumentView = (props: DocumentViewProps): React.ReactElement => {
    const hash = useHash();

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
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container">
                {props.isLoading ? (
                    <ProgressBar classNameWrapper="mt-30 mb-30" />
                ) : (
                    <RenderContent items={props.items} />
                )}
            </Container>
        </section>
    );
};
