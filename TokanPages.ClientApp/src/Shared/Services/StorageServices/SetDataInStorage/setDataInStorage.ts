import Validate from "validate.js";
import { SetDataInStorageInput } from "./interface";

export const SetDataInStorage = (props: SetDataInStorageInput): boolean => 
{
    if (Validate.isEmpty(props.key)) return false;

    let serialized = JSON.stringify(props.selection);
    localStorage.setItem(props.key, serialized);
    return true;
}
