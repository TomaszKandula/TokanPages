import Validate from "validate.js";
import { IDelDataFromStorage } from "./interface";

export const DelDataFromStorage = (props: IDelDataFromStorage): boolean => 
{
    if (Validate.isEmpty(props.key)) return false;
    localStorage.removeItem(props.key);
    return true;
}
