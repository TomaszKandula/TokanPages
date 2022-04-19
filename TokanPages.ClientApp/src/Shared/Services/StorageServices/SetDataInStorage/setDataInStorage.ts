import Validate from "validate.js";
import { ISetDataInStorage } from "./interface";

export const SetDataInStorage = (props: ISetDataInStorage): boolean => 
{
    if (Validate.isEmpty(props.key)) return false;

    let serialized = JSON.stringify(props.selection);
    localStorage.setItem(props.key, serialized);
    return true;
}
