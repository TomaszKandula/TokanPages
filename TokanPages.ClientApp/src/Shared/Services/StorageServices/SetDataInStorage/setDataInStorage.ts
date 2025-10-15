import Validate from "validate.js";
import { SetDataInStorageProps } from "../Types";

export const SetDataInStorage = (props: SetDataInStorageProps): boolean => {
    if (Validate.isEmpty(props.key)) return false;

    let serialized = JSON.stringify(props.selection);
    localStorage.setItem(props.key, serialized);
    return true;
};
