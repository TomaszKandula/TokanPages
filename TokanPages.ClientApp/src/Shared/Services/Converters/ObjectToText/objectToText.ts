import { TextObject } from "../../../../Shared/Components/RenderContent/Models";

interface Properties {
    textObject: TextObject | undefined;
}

export const ObjectToText = (props: Properties): string | undefined => {
    if (props.textObject === undefined) return undefined;
    if (props.textObject.items.length === 0) return undefined;

    let aggregated: string = "";
    props.textObject.items.forEach(item => {
        if (item.type === "html") {
            /* html type can be either 'string' or 'string[]' */
            const value = item.value as string | string[];

            let stripped: string = "";
            if (Array.isArray(value)) {
                value.forEach(item => {
                    stripped = stripped + " " + item.replace(/<[^>]+>/g, " ");
                });
            } else {
                stripped = value.replace(/<[^>]+>/g, " ");
            }

            aggregated = aggregated + " " + stripped;
        }
    });

    return aggregated.trimStart().trimEnd();
};
