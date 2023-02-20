import Validate from "validate.js";

interface Properties
{
    key: string;
}

export const DelDataFromStorage = (props: Properties): boolean => 
{
    if (Validate.isEmpty(props.key)) return false;
    localStorage.removeItem(props.key);
    return true;
}
