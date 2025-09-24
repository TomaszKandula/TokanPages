import Validate from "validate.js";

export const ProcessQueryParams = <T>(input: T): string => {
    let params = "";

    for (let property in input) {
        const data = input[property];

        if (Validate.isEmpty(params)) {
            params = `${params}?${property}=${data}`;
        } else if (!Validate.isEmpty(data)) {
            params = `${params}&${property}=${data}`;
        }
    }

    return params;
};
