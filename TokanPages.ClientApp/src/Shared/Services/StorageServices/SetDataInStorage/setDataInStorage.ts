import Validate from "validate.js";

interface Properties
{
    selection: {} | any[]; 
    key: string;
}

export const SetDataInStorage = (props: Properties): boolean => 
{
    if (Validate.isEmpty(props.key)) return false;

    let serialized = JSON.stringify(props.selection);
    localStorage.setItem(props.key, serialized);
    return true;
}
