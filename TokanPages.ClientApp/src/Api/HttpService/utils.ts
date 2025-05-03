import { ApplicationState } from "../../Store/Configuration";
import { USER_DATA } from "../../Shared/constants";
import { GetDataFromStorage } from "../../Shared/Services/StorageServices";
import { AuthenticateUserResultDto } from "../Models";
import Validate from "validate.js";
import base64 from "base-64";
import utf8 from "utf8";

export interface HeadersProps {
    key: string;
    value: string;
}

export interface ConfigurationProps {
    method: string;
    body?: object;
    form?: FormData;
    headers?: HeadersProps[];
    hasJsonResponse: boolean;
}

export interface StoreProps {
    dispatch: (action: any) => void;
    state: () => ApplicationState;
}

export interface ExecuteApiActionResultProps {
    status?: number;
    content?: string | object;
    error?: string | object | unknown;
}

export interface ExecuteContentActionProps extends StoreProps {
    request: string;
    receive: string;
    url: string;
}

export interface ExecuteApiActionProps {
    url: string;
    configuration: ConfigurationProps;
}

export interface ExecuteStoreActionProps extends ExecuteApiActionProps, StoreProps {
    optionalHandle?: string;
    responseType?: string | string[];
}

export const IsSuccessStatusCode = (statusCode: number): boolean => {
    return statusCode >= 200 && statusCode <= 299;
};

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

export const GetProcessedHeaders = (hasFormData: boolean, configurationHeaders?: HeadersProps[]): Headers => {
    const headers = new Headers();
    const baseHeaders = GetBaseHeaders();
    baseHeaders.forEach(header => {
        if (hasFormData && header.key === "Content-Type") {
            return;
        }

        headers.append(header.key, header.value);
    });

    if (Array.isArray(configurationHeaders)) {
        configurationHeaders.forEach(header => {
            if (header.key === "Content-Type" && headers.has("Content-Type")) {
                headers.delete("Content-Type");
            }

            headers.append(header.key, header.value);
        });
    }

    return headers;
};

export const GetProcessedResponse = async (response: Response, isJson?: boolean): Promise<object | string> => {
    if (isJson) {
        return await response.json();
    } else {
        return await response.text();
    }
};

export const GetProcessedBody = (props: ExecuteApiActionProps): string | FormData | null => {
    const optionalBody = props.configuration.body ? JSON.stringify(props.configuration.body) : null;

    const optionalFormData = props.configuration.form ? props.configuration.form : null;

    return optionalBody !== null ? optionalBody : optionalFormData !== null ? optionalFormData : null;
};
