interface Properties {
    value: string;
    limit: number;
}

export const GetShortText = (props: Properties): string => {
    if (props.value === undefined || props.value === "") return "";

    let result = props.value;
    let output: string[] = props.value.split(/\s+/);

    if (output.length > props.limit) {
        let strings = output.slice(0, props.limit);
        let lastWord = strings[strings.length - 1];

        strings[strings.length - 1] = lastWord.replace(/[^0-9a-zA-Z ]/, "");
        result = `${strings.join(" ")}...`;
    }

    return result;
};
