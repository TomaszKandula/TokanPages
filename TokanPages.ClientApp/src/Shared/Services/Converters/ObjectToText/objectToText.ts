import { TextObject } from "Shared/Components/RenderContent/Models";

interface Properties {
    textObject: TextObject | undefined;
}

export const ObjectToText = (props: Properties): string | undefined => {
    if (props.textObject === undefined) return undefined;
    if (props.textObject.items.length === 0) return undefined;

    let rawText: string = "";
    let htmlText: string = "";
    let aggregatedText: string = "";

    props.textObject.items.forEach(item => {
        if (item.type === "html") {
            htmlText = item.value as string;
            rawText = htmlText.replace(/<[^>]+>/g, " ");
            aggregatedText = aggregatedText + " " + rawText;
        }
    });

    return aggregatedText.trimStart().trimEnd();
};
