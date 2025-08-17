export const TryParse = <T>(value: string): T => {
    try {
        return JSON.parse(value) as T;
    } catch {
        throw new Error("Parsing error.");
    }
}
