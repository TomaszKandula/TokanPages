import { ApplicationState } from "../../../Store/Configuration";

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

export interface ExecuteApiActionProps {
    url: string;
    configuration: ConfigurationProps;
}

export interface ExecuteStoreActionProps extends ExecuteApiActionProps, StoreProps {
    optionalHandle?: string;
    responseType?: string | string[];
}
