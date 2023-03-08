import Validate from "validate.js";

interface Properties
{
    key: string;
}

export const GetDataFromStorage = (props: Properties): string | {} | any[] => 
{
    const serialized = localStorage.getItem(props.key) as string;
    if (Validate.isEmpty(serialized)) return { };

    let deserialized: string | {} | any[] = "";
    try 
    {
        deserialized = JSON.parse(serialized);
    }
    catch
    {
        console.error(`Cannot parse JSON string: ${serialized}.`);
        return { };
    }

    return deserialized;
}
