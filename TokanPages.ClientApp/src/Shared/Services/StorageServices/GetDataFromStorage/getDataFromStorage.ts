import Validate from "validate.js";
import { IGetDataFromStorage } from "./interface";

export const GetDataFromStorage = (props: IGetDataFromStorage): {} | any[] => 
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
