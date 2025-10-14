export const GetProcessedResponse = async (response: Response, isJson?: boolean): Promise<object | string> => {
    if (isJson) {
        return await response.json();
    } else {
        return await response.text();
    }
};
