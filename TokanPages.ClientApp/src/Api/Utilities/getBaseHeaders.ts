import { AuthenticateUserResultDto } from "../../Api/Models";
import { GetDataFromStorage } from "../../Shared/Services/StorageServices";
import { USER_DATA } from "../../Shared/Constants";
import { HeadersProps } from "./Types";
import Validate from "validate.js";
import base64 from "base-64";
import utf8 from "utf8";

export const GetBaseHeaders = (): HeadersProps[] => {
    const timezoneOffset = new Date().getTimezoneOffset();
    const encoded = GetDataFromStorage({ key: USER_DATA }) as string;

    const headers: HeadersProps[] = [];
    headers.push({ key: "UserTimezoneOffset", value: timezoneOffset.toString() });
    headers.push({ key: "Content-Type", value: "application/json" });

    if (Validate.isEmpty(encoded)) {
        return headers;
    }

    const decoded = base64.decode(encoded);
    const text = utf8.decode(decoded);
    const data = JSON.parse(text) as AuthenticateUserResultDto;
    const hasAuthorization = Validate.isObject(data) && !Validate.isEmpty(data.userToken);

    if (hasAuthorization) {
        headers.push({ key: "Authorization", value: `Bearer ${data.userToken}` });
    }

    return headers;
};
