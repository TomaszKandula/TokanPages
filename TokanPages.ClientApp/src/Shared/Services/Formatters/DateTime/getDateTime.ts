interface Properties {
    value: string;
    hasTimeVisible: boolean;
}

export const GetDateTime = (props: Properties): string => {
    if (props.value === "n/a" || props.value === "" || props.value === " ") return "n/a";

    const formatWithTime: Intl.DateTimeFormatOptions = {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
    };

    const formatWithoutTime: Intl.DateTimeFormatOptions = {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
    };

    const options = props.hasTimeVisible ? formatWithTime : formatWithoutTime;
    const datetime = props.value ? new Date(props.value) : null;

    let result = "n/a";
    if (datetime) result = datetime.toLocaleDateString("en-US", options);

    return result;
};
