interface Properties
{
    inputText: string | undefined;
}

export const CountWords = (props: Properties): number => 
{
    if (props.inputText === undefined) 
    {
        return 0;
    }

    const filtering = (value: string): boolean => 
    {
        return value != ""
    }

    const result = props.inputText.split(" ").filter(filtering).length;
    return result;
}
