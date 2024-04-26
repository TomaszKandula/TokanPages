import { useDispatch } from "react-redux";
import { USER_DATA } from "../../Shared/constants";
import { GetDataFromStorage } from "./StorageServices";
import { UserDataStoreAction } from "../../Store/Actions";
import { AuthenticateUserResultDto } from "../../Api/Models";
import Validate from "validate.js";
import base64 from "base-64";
import utf8 from "utf8";

export const UpdateUserData = (): void => {
    const dispatch = useDispatch();
    const encoded = GetDataFromStorage({ key: USER_DATA }) as string;
    if (Validate.isEmpty(encoded)) {
        return;
    }

    const decoded = base64.decode(encoded);
    const text = utf8.decode(decoded);
    const data = JSON.parse(text) as AuthenticateUserResultDto;
    const hasData = Object.entries(data).length !== 0;

    if (hasData) {
        dispatch(UserDataStoreAction.update(data));
    }
};
