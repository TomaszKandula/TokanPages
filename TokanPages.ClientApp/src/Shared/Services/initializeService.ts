import { Dispatch } from "redux";
import { UserDataStoreAction } from "../../Store/Actions";
import { AuthenticateUserResultDto } from "../../Api/Models";
import { GetDataFromStorage } from "./StorageServices";
import { HasSnapshotMode } from "./SpaCaching";
import { USER_DATA } from "../constants";
import Validate from "validate.js";
import base64 from "base-64";
import utf8 from "utf8";
import AOS from "aos";
import "aos/dist/aos.css";

export const EnsureUserData = (dispatch: Dispatch<any>): void => {
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

export const InitializeAnimations = (): NodeJS.Timer => {
    const hasSnapshotMode = HasSnapshotMode();
    AOS.init({ once: !hasSnapshotMode, disable: hasSnapshotMode });
    return setInterval(() => AOS.refresh(), 900);
}
