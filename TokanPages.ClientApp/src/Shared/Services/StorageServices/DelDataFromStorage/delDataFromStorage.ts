import Validate from "validate.js";
import { DelDataFromStorageProps } from "../Types";

export const DelDataFromStorage = (props: DelDataFromStorageProps): boolean => {
    if (Validate.isEmpty(props.key)) return false;
    localStorage.removeItem(props.key);
    return true;
};
