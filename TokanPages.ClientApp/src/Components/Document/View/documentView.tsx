import * as React from "react";
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
        <section className={props.background}>
            <article className="bulma-container bulma-is-max-tablet">
                {props.isLoading ? (
                    <ProgressBar className="my-5" thickness={4} />
                ) : (
                    <RenderContent items={props.items} />
                )}
            </article>
        </section>
    );
};
