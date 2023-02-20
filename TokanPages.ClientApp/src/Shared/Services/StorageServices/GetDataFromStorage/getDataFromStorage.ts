import Validate from "validate.js";
import { GetDataFromStorageInput } from "./interface";

export const GetDataFromStorage = (props: GetDataFromStorageInput): {} | any[] => 
{
    let serialized = localStorage.getItem(props.key) as string;
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
