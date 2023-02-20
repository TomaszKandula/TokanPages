interface Properties
{
    error: string;
    template: string;
}

export const GetTextError = (props: Properties): string =>
{
    return props.template.replace("{ERROR}", props.error);
}
