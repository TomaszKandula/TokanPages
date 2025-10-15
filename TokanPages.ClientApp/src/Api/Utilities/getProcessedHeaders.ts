import { GetBaseHeaders } from "../../Api";
import { HeadersProps } from "./Types";

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
