import Validate from "validate.js";
import { DelDataFromStorageInput } from "./interface";

export const DelDataFromStorage = (props: DelDataFromStorageInput): boolean => 
{
    if (Validate.isEmpty(props.key)) return false;
    localStorage.removeItem(props.key);
    return true;
}
